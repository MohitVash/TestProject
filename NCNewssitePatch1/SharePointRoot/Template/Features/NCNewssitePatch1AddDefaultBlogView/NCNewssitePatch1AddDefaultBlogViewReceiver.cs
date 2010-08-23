using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1AddDefaultBlogViewReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            SPList list = web.Lists["Blog"];
            SPViewCollection collection = list.Views;

            StringCollection viewfields = new StringCollection();

            viewfields.Add("LinkTitle");
            viewfields.Add("Author");
            viewfields.Add("Modified");

            collection.Add("DefaultView", viewfields, null, 5000, true, true);
            web.Update();

        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            this.FeatureDeactivating(properties);
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
