using System;
using System.Collections.Generic;
using Microsoft.SharePoint;

namespace NCNewssiteBranding.EventHandlers.Features
{
    public class NCNewssiteBrandingRemoveFromExistingSitesReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = (SPSite)properties.Feature.Parent;

            SPWeb web = site.RootWeb;

            RemoveBrandingFromSite(web);

            site.Features.Remove(properties.Feature.Definition.Id);
        }

        private void RemoveBrandingFromSite(SPWeb web)
        {
            // Activate NCNewssiteBrandingSetup
            if (web.Features[new Guid("e3411783-1c22-4d1c-b7f3-8f70c0e1e3f6")] != null)
            {
                web.Features.Remove(new Guid("e3411783-1c22-4d1c-b7f3-8f70c0e1e3f6"));
            }

            // Recurse all subsites and apply.
            foreach (SPWeb subWeb in web.Webs)
            {
                RemoveBrandingFromSite(subWeb);
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
