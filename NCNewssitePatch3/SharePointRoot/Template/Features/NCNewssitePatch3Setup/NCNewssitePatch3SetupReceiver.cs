using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch3.EventHandlers.Features
{
    public class NCNewssitePatch3SetupReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            //only activate these features on root web
            if (web.IsRootWeb)
            {
                //NCNewssitePatch3AddMetadataColumntoArticleList
                if (web.Site.Features[new Guid("907ce7d6-cab3-4061-8172-c99ca4547ff7")] == null)
                {
                    web.Site.Features.Add(new Guid("907ce7d6-cab3-4061-8172-c99ca4547ff7"));
                }

                //NCNewssitePatch3AddPageLayoutWithLeftManu
                if (web.Features[new Guid("e5a829e6-c026-44db-9886-82efdd5d872a")] == null)
                {
                    web.Features.Add(new Guid("e5a829e6-c026-44db-9886-82efdd5d872a"));
                }
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
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
