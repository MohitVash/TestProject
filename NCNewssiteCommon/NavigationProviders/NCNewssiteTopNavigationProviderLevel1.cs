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
    public class NCNewssiteTopNavigationProviderLevel1 : StaticSiteMapProvider
    {

        public override SiteMapNode BuildSiteMap()
        {
            SiteMapNode root = GetRootNode();

            return root;
        }

        public override SiteMapNode CurrentNode
        {
            get
            {
                SPWeb currentWeb = NCTopMenuHelper.GetCurrentWeb();

                if (currentWeb == SPContext.Current.Site.RootWeb)
                {
                    return null;
                }
                else
                {
                    if (currentWeb.ParentWeb.ID == SPContext.Current.Site.RootWeb.ID)
                    {
                        return new SiteMapNode(this, currentWeb.ParentWeb.ID.ToString(), currentWeb.ParentWeb.Webs[currentWeb.ID].ServerRelativeUrl, currentWeb.ParentWeb.Title);
                    }

                    //return new SiteMapNode(this, currentsite.ParentWeb.ID.ToString(), currentsite.ParentWeb.Webs[0].ServerRelativeUrl, currentsite.ParentWeb.Title);
                    return new SiteMapNode(this, currentWeb.ParentWeb.ID.ToString(), getSubsiteUrl(currentWeb.ParentWeb), currentWeb.ParentWeb.Title);
                }                
            }
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return BuildSiteMap();
        }

        /// <summary>
        /// Try to get menu from cache. If not in cache, build menu
        /// </summary>
        /// <returns></returns>
        private SiteMapNode GetRootNode()
        {
            string cacheKey = SPContext.Current.Site.RootWeb.ID.ToString();

            SiteMapNode rootNode = (SiteMapNode)HttpContext.Current.Cache[cacheKey];

            if (rootNode == null)
            {
                rootNode = BuildNodeTree();
                HttpContext.Current.Cache.Add(cacheKey, rootNode, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return rootNode;
        }

        private SiteMapNode BuildNodeTree()
        {
            Clear();

            SiteMapNode root = new SiteMapNode(this, "Root", "/", null);
            AddNode(root);

            SPWeb currentsiteRootweb = NCTopMenuHelper.GetCurrentRootWeb();

            SiteMapNode node;

            List<SPWeb> webs = new List<SPWeb>();

            foreach (SPWeb web in currentsiteRootweb.Webs)
            {
                webs.Add(web);
            }

            webs.Sort(new Comparison<SPWeb>(SortWebs));

            foreach (SPWeb web in webs)
            {
                string serverRelativeUrl = "";

                if (web.Webs.Count == 0)
                    serverRelativeUrl = web.ServerRelativeUrl;
                else
                    serverRelativeUrl = getSubsiteUrl(web);

                node = new SiteMapNode(this, web.ID.ToString(), serverRelativeUrl, web.Title);
                AddNode(node, root);
            }
            
            return root;
        }

        private string getSubsiteUrl(SPWeb web)
        {
            List<SPWeb> webs = new List<SPWeb>();

            foreach (SPWeb subWeb in web.Webs)
            {
                webs.Add(subWeb);
            }

            webs.Sort(new Comparison<SPWeb>(SortWebs));

            return webs[0].ServerRelativeUrl;
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
    }
}