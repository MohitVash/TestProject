using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using Microsoft.SharePoint;
using NCNewssiteCommon.Utilities;

namespace NCNewssiteCommon.NavigationProviders
{
    public class NCNewssiteTopNavigationProviderLevel2 : StaticSiteMapProvider
    {

        public override SiteMapNode BuildSiteMap()
        {
            SPWeb currentweb = NCTopMenuHelper.GetCurrentWeb();

            //get down to the root web (level 1 root)
            if (currentweb != SPContext.Current.Site.RootWeb)
            {
                while (currentweb.ParentWeb.Url != SPContext.Current.Site.RootWeb.Url)
                {
                    currentweb = currentweb.ParentWeb;
                }
            }

            SiteMapNode menu = GetMenuStructure(currentweb);
            
            return menu;
        }

        public override SiteMapNode CurrentNode
        {
            get
            {
                if (SPContext.Current.Web == SPContext.Current.Site.RootWeb)
                {
                    return null;
                }
                else
                {
                    return ResolveCurrentNode();
                }
            }
        }

        private SiteMapNode ResolveCurrentNode()
        {
            List<SPWeb> webs = new List<SPWeb>();

            SPWeb currentweb = SPContext.Current.Web;
            webs.Add(currentweb);

            //get down to the root web
            while (currentweb.ParentWeb.Url != SPContext.Current.Site.RootWeb.Url)
            {
                currentweb = currentweb.ParentWeb;
                webs.Add(currentweb);
            }

            //the second last element in the list is now the level 2 element and also the current element
            if (webs.Count >= 2)
            {
                currentweb = webs[webs.Count - 2];
                return new SiteMapNode(this, currentweb.ID.ToString(), currentweb.ServerRelativeUrl, currentweb.Title);
            }
            else
            {
                return null;
            }
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return BuildSiteMap();
        }

        /// <summary>
        /// Try to get menu from cache. If not in cache, build menu
        /// </summary>
        /// <param name="currentWeb">The web to get menustructure for</param>
        /// <returns></returns>
        private SiteMapNode GetMenuStructure(SPWeb currentWeb)
        {
            string cacheKeyMaster = "NCTopNavLevel2" + SPContext.Current.Site.RootWeb.ID.ToString();

            SiteMapNode entiremenu = (SiteMapNode)HttpContext.Current.Cache[cacheKeyMaster];

            if (entiremenu == null)
            {
                //cache entire menustructure
                entiremenu = BuildFullMenuStructure(currentWeb);
                AddMenuToCache(cacheKeyMaster, entiremenu);
            }

            if (SPContext.Current.Site.RootWeb.ID.ToString() == currentWeb.ID.ToString())
                return null;

            //return correct menu, exists in own cache
            SiteMapNode menu = (SiteMapNode)HttpContext.Current.Cache[currentWeb.ID.ToString()];

            return menu;
        }

        private SiteMapNode BuildFullMenuStructure(SPWeb currentWeb)
        {
            Clear();

            SiteMapNode root = null;

            SPWeb webSite = SPContext.Current.Site.RootWeb;
            string relativeUrl = webSite.ServerRelativeUrl.ToString();

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                SPSite currentsite = new SPSite(webSite.Site.Url);
                SPWeb rootweb = currentsite.OpenWeb(relativeUrl);

                root = new SiteMapNode(this, rootweb.ID.ToString(), rootweb.ServerRelativeUrl, rootweb.Title);

                if (rootweb == currentWeb)
                {
                    SiteMapNode root2 = new SiteMapNode(this, "Root", "/", null);
                    AddNode(root2);

                    string cacheKey = rootweb.ID.ToString();
                    AddMenuToCache(cacheKey, root2);
                }

                foreach (SPWeb web in rootweb.Webs)
                {
                    SiteMapNode node = BuildNodeTree(web);
                    AddNode(node, root);
                }
            });


            return root;
        }

        private SiteMapNode BuildNodeTree(SPWeb currentWeb)
        {
            string cacheKey = currentWeb.ID.ToString();

            //currentweb is now a level 1 web (children of root)
            SiteMapNode root = new SiteMapNode(this, currentWeb.ID.ToString(), currentWeb.ServerRelativeUrl, currentWeb.Title);
            SiteMapNode node;

            //list all level 2 webs with level 1 web as parent
            List<SPWeb> webs = new List<SPWeb>();

            foreach (SPWeb web in currentWeb.Webs)
            {
                webs.Add(web);
            }

            webs.Sort(new Comparison<SPWeb>(SortWebs));

            foreach (SPWeb web in webs)
            {
                node = new SiteMapNode(this, web.ID.ToString(), web.ServerRelativeUrl, web.Title);
                AddNode(node, root);
            }

            AddMenuToCache(cacheKey, root);

            return root;
        }

        private int SortWebs(SPWeb web1, SPWeb web2)
        {
            int web1SortOrder;
            int web2SortOrder;

            if (!Int32.TryParse((web1.Properties["SortOrder"]), out web1SortOrder))
            {
                web1SortOrder = 99999;
            }

            if (!Int32.TryParse((web2.Properties["SortOrder"]), out web2SortOrder))
            {
                web2SortOrder = 99999;
            }

            return web1SortOrder.CompareTo(web2SortOrder);
        }

        private void AddMenuToCache(string cacheKey, SiteMapNode node)
        {
            HttpContext.Current.Cache.Add(cacheKey, node, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
    }
}