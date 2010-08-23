#region Namespaces
using System;
using System.Web;
using System.Web.Caching;
using Microsoft.SharePoint;
#endregion

namespace NCNewssiteCommon.NavigationProviders
{
    /// <summary>
    /// This class creates a custom provider for the breadcrumbs.
    /// </summary>
    public class NCNewssiteSiteMapProvider : StaticSiteMapProvider
    {
        #region Private Members
        private const bool USECACHE = true;
        private const string ISBLANKSITE = "IsBlankSite";
        private const string ARTICLESLISTTITLE = "Articles";

        private SiteMapNode _rootNode = null;
        private SPWeb _rootWeb = null;
        private SPList _articles = null;
        private bool _initialized = false;
        #endregion

        #region Properties

        public virtual bool IsInitialized
        {
            get
            {
                return _initialized;
            }
        }

        public override SiteMapNode RootNode
        {
            get
            {
                if (!USECACHE)
                {
                    return BuildSiteMap();
                }

                if (_rootNode != null)
                    return _rootNode;

                string cacheKey = SPContext.Current.Site.RootWeb.ID.ToString();
                _rootNode = (SiteMapNode)HttpContext.Current.Cache[cacheKey];

                if (_rootNode != null)
                    return _rootNode;

                _rootNode = BuildSiteMap();

                HttpContext.Current.Cache.Add(cacheKey, _rootNode, null,
                    DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration,
                    CacheItemPriority.Normal, null);

                return _rootNode;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// This method returns the relative URL after removing the rootweb url. 
        /// </summary>
        /// <param name="url">URL of the current site</param>
        /// <returns>relative URL</returns>
        private string MakeRelativUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;
            return url.Replace(SPContext.Current.Site.RootWeb.Url, string.Empty);
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Gets complete site map 
        /// </summary>
        /// <returns>Site map node</returns>
        protected override SiteMapNode GetRootNodeCore()
        {
            return RootNode;
        }

        /// <summary>
        /// Used to clear the currnet node
        /// </summary>
        protected override void Clear()
        {
            lock (this)
            {
                _rootNode = null;
                base.Clear();
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            if (IsInitialized)
                return;
            base.Initialize(name, attributes);
            _initialized = true;
        }

        /// <summary>
        /// Method used to create the site map
        /// </summary>
        /// <returns>Site map node</returns>
        public override SiteMapNode BuildSiteMap()
        {
            lock (this)
            {
                if (_rootNode == null)
                {
                    Clear();

                    _rootWeb = SPContext.Current.Site.RootWeb;
                    _rootNode = new SiteMapNode(this, "root", _rootWeb.ServerRelativeUrl, _rootWeb.Title);

                    AddNode(_rootNode);
                    IterateWeb(_rootWeb, _rootNode, 0);
                }
            }
            return _rootNode;
        }

        /// <summary>
        /// Property which determins the current node
        /// </summary>
        public override SiteMapNode CurrentNode
        {
            get
            {
                SiteMapNode node;

                try
                {
                    string url;
                    var currentNavSiteUrl = SiteMap.Providers["CurrentNavSiteMapProviderNoEncode"].CurrentNode.Url;

                    if (SiteMap.Providers["SPContentMapProvider"].CurrentNode == null)
                    {
                        url = currentNavSiteUrl;
                    }
                    else
                    {
                        var spContentMapUrl = SiteMap.Providers["SPContentMapProvider"].CurrentNode.Url;
                        if (spContentMapUrl.StartsWith("/"))
                        {
                            if (spContentMapUrl.IndexOf("?ID=") > 0)
                            {
                                if (spContentMapUrl.IndexOf("&") > 0)
                                    url = spContentMapUrl.Substring(0, spContentMapUrl.IndexOf("&"));
                                else
                                    url = spContentMapUrl;
                            }
                            else if (spContentMapUrl.IndexOf("?") > 0)
                                url = spContentMapUrl.Substring(0, spContentMapUrl.IndexOf("?"));
                            else
                                url = spContentMapUrl;
                        }
                        else
                        {
                            url = currentNavSiteUrl;
                        }
                    }

                    // handle articles site
                    if (url.StartsWith("/Lists/Articles/DispForm.aspx?ID="))
                    {
                        string source = HttpContext.Current.Request.QueryString["source"];

                        if (source == null && HttpContext.Current.Request.UrlReferrer != null)
                            source = HttpContext.Current.Request.UrlReferrer.AbsolutePath;
                        else
                            source = HttpContext.Current.Server.UrlDecode(source);

                        if (source != null)
                        {
                            source = MakeRelativUrl(source);

                            if ((!source.StartsWith("http")) || (!source.StartsWith("https")))
                                if (source.IndexOf('/', 4) != -1)
                                    url = source.Substring(0, source.IndexOf('/', 4)) + url;
                        }
                    }

                    node = this.FindSiteMapNode(url);

                }
                catch
                {
                    node = null;
                }

                if (node != null)
                    return node;
                else
                    return SiteMap.Providers["CurrentNavSiteMapProviderNoEncode"].CurrentNode;
            }
        }

        #endregion

        #region Iterators

        /// <summary>
        /// Method iterates complete site and and their sub sites to create the site map.
        /// </summary>
        /// <param name="currentWeb"></param>
        /// <param name="currentNode"></param>
        /// <param name="level"></param>
        private void IterateWeb(SPWeb currentWeb, SiteMapNode currentNode, int level)
        {
            IterateLists(currentWeb, currentNode, level);

            if (currentWeb.Webs.Count > 0)
            {
                foreach (SPWeb web in currentWeb.Webs)
                {
                    bool isBlankSite = ((web.Properties[ISBLANKSITE] ?? "0") == "1");
                    string serverRelativeUrl = isBlankSite ? string.Empty : web.ServerRelativeUrl;

                    var node = new SiteMapNode(this, web.ID.ToString(), serverRelativeUrl, web.Title);
                    AddNode(node, currentNode);

                    IterateWeb(web, node, level + 1);
                }
            }
        }

        /// <summary>
        /// Method iterates article list to create the site map.
        /// </summary>
        /// <param name="currentWeb"></param>
        /// <param name="currentNode"></param>
        /// <param name="level"></param>
        private void IterateLists(SPWeb currentWeb, SiteMapNode currentNode, int level)
        {
            foreach (SPList list in currentWeb.Lists)
            {
                var listurl = MakeRelativUrl(list.DefaultViewUrl);
                var listnode = new SiteMapNode(this, list.ID.ToString(), listurl, list.Title);
                AddNode(listnode, currentNode);

                var newitemurl = MakeRelativUrl(list.DefaultNewFormUrl);
                var newitemnode = new SiteMapNode(this, list.ID.ToString() + "new", newitemurl, "New " + list.Title);
                AddNode(newitemnode, currentNode);

                IterateListItems(list, currentWeb, currentNode, level);

                if (level == 0 && list.Title == ARTICLESLISTTITLE)
                {
                    _articles = list;
                }
                else if (level == 2 && _articles != null)
                {
                    IterateListItems(_articles, currentWeb, currentNode, level);
                }
            }
        }

        /// <summary>
        /// Method iterates article list items to create the site map.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="currentWeb"></param>
        /// <param name="currentNode"></param>
        /// <param name="level"></param>
        private void IterateListItems(SPList list, SPWeb currentWeb, SiteMapNode currentNode, int level)
        {
            foreach (SPListItem item in list.Items)
            {
                try
                {
                    var title = item.Title;

                    title = title.Length > 20 ? title.Substring(0, 15) + "..." : title;

                    string itemurl;
                    string serverRelativeUrl = currentWeb.ServerRelativeUrl;

                    if (serverRelativeUrl == "/")
                        serverRelativeUrl = string.Empty;

                    if (list.Title == "Pages")
                        itemurl = serverRelativeUrl + "/" + item.Url;
                    else
                        itemurl = serverRelativeUrl + "/" + item.ParentList.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url + "?ID=" + item.ID;

                    var itemnode = new SiteMapNode(this, currentWeb.ServerRelativeUrl + list.ID.ToString() + item.ID.ToString(), itemurl, title);
                    AddNode(itemnode, currentNode);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        #endregion
    }
}