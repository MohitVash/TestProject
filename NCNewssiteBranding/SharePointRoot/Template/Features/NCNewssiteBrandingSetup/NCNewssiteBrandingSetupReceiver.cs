using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssiteBranding.EventHandlers.Features
{
    public class NCNewssiteBrandingSetupReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            Guid featureId = new Guid();

            // MasterFiles
            featureId = new Guid("155706c3-d44f-43e5-bf11-fe605f370295");
            if (web.Features[featureId] == null)
            {
                web.Features.Add(featureId);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            Guid featureId = new Guid();

            // MasterFiles
            featureId = new Guid("155706c3-d44f-43e5-bf11-fe605f370295");
            if (web.Features[featureId] != null)
            {
                web.Features.Remove(featureId);
            }
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
