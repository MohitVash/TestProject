using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using NCNewssiteCommon.Utilities;
using System.Web.UI.WebControls.WebParts;


namespace NCNewssitePageSetup.EventHandlers.Features
{
    public class NCWebPartsSetupReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;
            AddWebPartsToAllPages(web.Site);
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


        private void AddWebPartsToAllPages(SPSite site)
        {
            string pageUrl = "/Pages/FrontPage.aspx";
            SPWeb rootWeb = site.RootWeb;
            foreach (SPWeb web in rootWeb.Webs)
            {
                foreach (SPWeb subWeb in web.Webs)
                {
                    
                    AddContentEditorWebParts(subWeb, subWeb.Url + pageUrl);
                }
            }
        }


        private void AddContentEditorWebParts(SPWeb web, string pageUrl)
        {
            
            PartChromeState chromeState = PartChromeState.Normal;
            PartChromeType chromeType = PartChromeType.TitleOnly;

            NCWebparts.AddCEWP(web, pageUrl, "LeftColumn", 0, "CEO Channel", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "LeftColumn", 1, "Quick Links", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "LeftColumn", 2, "Something", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "Footer", 0, "Vision & Strategy", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "Footer", 1, "Focus on", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "RightColumn", 0, "Service announcements", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "RightColumn", 1, "Goals and KPI's", chromeState, chromeType);
            NCWebparts.AddCEWP(web, pageUrl, "RightColumn", 2, "Idea Storm", chromeState, chromeType);
        }


    }
}
