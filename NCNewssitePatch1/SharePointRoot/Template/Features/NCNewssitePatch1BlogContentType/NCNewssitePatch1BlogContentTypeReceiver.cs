using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1BlogContentTypeReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            base.FeatureActivated(properties);
            // if (properties.Feature.Parent is SPSite)
            // {
            // SPSite site = (SPSite)properties.Feature.Parent;
            // 
            // }

        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            this.FeatureDeactivating(properties);
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
