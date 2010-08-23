using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch2.EventHandlers.Features
{
    public class NCNewssitePatch2SetupReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPWeb web = (SPWeb)properties.Feature.Parent;
            

            //only activate these features on root web
            if (web.IsRootWeb)
            {

                //NCNewsSitePatch2AddAppPagesToSiteSettings
                if (web.Site.Features[new Guid("25105fff-4489-4da6-8c37-ad834d7edd62")] == null)
                {
                    web.Site.Features.Add(new Guid("25105fff-4489-4da6-8c37-ad834d7edd62"));
                }

                //NCNewsSitePatch2AddIsBlankSiteProperty
                if (web.Features[new Guid("ffd0c621-f326-48c4-ba39-ad36f8601975")] == null)
                {
                    web.Features.Add(new Guid("ffd0c621-f326-48c4-ba39-ad36f8601975"));
                }

                //NCNewsSitePatch2AddShowOnNavigationSiteProperties
                if (web.Site.Features[new Guid("b46bd994-b77b-4cc6-88e3-0da4ade45abf")] == null)
                {
                    web.Site.Features.Add(new Guid("b46bd994-b77b-4cc6-88e3-0da4ade45abf"));
                }

                //NCNewssitePatch2ModifyArticlePageCT
                if (web.Features[new Guid("0bdb2a5a-8b39-4fab-b94f-0e0eb97db7e2")] == null)
                {
                    web.Features.Add(new Guid("0bdb2a5a-8b39-4fab-b94f-0e0eb97db7e2"));
                }

                //NCNewssitePatch2ModifyArticlesContentType
                if (web.Site.Features[new Guid("2548cae0-94dc-4019-995d-885af54b81fc")] == null)
                {
                    web.Site.Features.Add(new Guid("2548cae0-94dc-4019-995d-885af54b81fc"));
                }


                //NCNewssitePatch2ModifyArticlesList
                if (web.Features[new Guid("7561b874-c423-49e8-8ef5-6330e327ec4b")] == null)
                {
                    web.Features.Add(new Guid("7561b874-c423-49e8-8ef5-6330e327ec4b"));
                }


            }
   
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
            // if (properties.Feature.Parent is SPWeb)
            // {
            // SPWeb web = (SPWeb)properties.Feature.Parent;
            // SPSite site = web.Site;
            // 
            // }

        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
            base.FeatureInstalled(properties);
        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            base.FeatureUninstalling(properties);
        }

        public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, IDictionary<string, string> parameters)
        {
            base.FeatureUpgrading(properties, upgradeActionName, parameters);

        }
    }
}
