using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch3.EventHandlers.Features
{
    public class NCNewssitePatch3AddPageLayoutWithLeftManuReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;
            SPSite site = web.Site;

            PublishingWeb publishingWeb = PublishingWeb.GetPublishingWeb(web);
            PublishingSite publishingSite = new PublishingSite(site);

            if (PublishingWeb.IsPublishingWeb(web))
            {
                SPContentType associatedContentType = publishingSite.ContentTypes["Article Page"];
                if (associatedContentType != null)
                {
                    PageLayout[] pageLayouts = publishingWeb.GetAvailablePageLayouts();
                    PageLayout customPageLayout = GetCustomPageLayout(publishingSite, associatedContentType);
                    List<PageLayout> newPageLayoutList = new List<PageLayout> {customPageLayout};
                    newPageLayoutList.AddRange(pageLayouts);

                    if (customPageLayout != null)
                    {
                        publishingWeb.SetAvailablePageLayouts(newPageLayoutList.ToArray(),true);
                        publishingWeb.SetDefaultPageLayout(pageLayouts[0],true);
                        publishingWeb.Update();
                        //Mohit
                    }
                }
            }
        }

        /// <summary>
        /// Method that will return custom added page layout
        /// </summary>
        /// <param name="publishingSite"></param>
        /// <param name="contentType"></param>
        /// <returns>PageLayout</returns>
        private static PageLayout GetCustomPageLayout(PublishingSite publishingSite, SPContentType contentType)
        {
            PageLayoutCollection pageLayouts = publishingSite.GetPageLayouts(contentType, true);
            return pageLayouts.FirstOrDefault(pageLayout => string.Equals(pageLayout.Name, "PageFromDocLayoutWithLeftMenu.aspx"));
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
            // if (properties.Feature.Parent is SPSite)
            // {
            // SPSite site = (SPSite)properties.Feature.Parent;
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
