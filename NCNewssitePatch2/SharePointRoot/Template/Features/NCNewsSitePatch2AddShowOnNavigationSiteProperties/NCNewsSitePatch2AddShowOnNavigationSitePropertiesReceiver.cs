using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch2.EventHandlers.Features
{
    public class NCNewsSitePatch2AddShowOnNavigationSitePropertiesReceiver : SPFeatureReceiver
    {
        private const string ShowOnBreadcrumbProperty = "ShowOnBreadcrumb";
        private const string ShowOnLeftMenuProperty = "ShowOnLeftMenu";
        private int _subSiteLevel = 1;
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite rootSite = (SPSite)properties.Feature.Parent;
            SPWeb rootWeb = rootSite.OpenWeb();


            foreach (SPWeb web in rootWeb.Webs)
            {
                _subSiteLevel = 1;
                ConfigureNavigationFlags(web, "0");
                web.Properties.Update();
                web.Update();
                IterateSubWebs(web);
            }
        }

        /// <summary>
        /// Method that will iterate through all sub webs
        /// </summary>
        /// <param name="web"></param>
        private void IterateSubWebs(SPWeb web)
        {
            foreach (SPWeb subWeb in web.Webs)
            {
                if (_subSiteLevel > 1)
                    ConfigureNavigationFlags(subWeb, "1");
                else
                    ConfigureNavigationFlags(subWeb, "0");
                if (subWeb.Webs.Count > 0)
                {
                    _subSiteLevel++;
                    IterateSubWebs(subWeb);
                    _subSiteLevel--;
                }
                subWeb.Properties.Update();
                subWeb.Update();
            }
        }

        /// <summary>
        /// Method used to create/update the web property
        /// </summary>
        /// <param name="subWeb"></param>
        /// <param name="value"></param>
        private void ConfigureNavigationFlags(SPWeb subWeb, string value)
        {
            subWeb.Properties[ShowOnBreadcrumbProperty] = value;
            subWeb.Properties[ShowOnLeftMenuProperty] = value;
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
