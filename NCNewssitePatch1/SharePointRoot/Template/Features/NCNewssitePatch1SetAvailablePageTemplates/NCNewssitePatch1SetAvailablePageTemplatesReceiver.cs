using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Publishing;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1SetAvailablePageTemplatesReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            PublishingWeb publishingWeb = null;
            if (PublishingWeb.IsPublishingWeb(web))
            {
                publishingWeb = PublishingWeb.GetPublishingWeb(web);

                PageLayout[] pageLayouts = publishingWeb.GetAvailablePageLayouts();
                PageLayout[] availablePageLayouts = new PageLayout[1];

                availablePageLayouts[0] = pageLayouts[0];

                publishingWeb.SetAvailablePageLayouts(availablePageLayouts, true);
                publishingWeb.SetDefaultPageLayout(availablePageLayouts[0], true);
                publishingWeb.Update();
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
