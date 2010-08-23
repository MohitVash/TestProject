using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch2.EventHandlers.Features
{
    public class NCNewsSitePatch2AddIsBlankSitePropertyReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb rootWeb = (SPWeb)properties.Feature.Parent;

            foreach (SPWeb web in rootWeb.Webs)
            {
                switch (web.Title)
                {
                    case "Business Units":
                        web.Properties["IsBlankSite"]= "1";
                        break;
                    case "Group Units":
                        web.Properties["IsBlankSite"]= "1";
                        break;
                    case "News archive":
                        web.Properties["IsBlankSite"]= "1";
                        break;
                    case "About Newco":
                        web.Properties["IsBlankSite"]= "1";
                        break;
                    default:
                        web.Properties["IsBlankSite"]= "1";
                        break;
                }
                web.Properties.Update();
                web.Update();

                foreach (SPWeb subWeb in web.Webs)
                {
                    switch (subWeb.Title)
                    {
                        //Business Units subsites
                        case "Financial Acquring":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Merchant Solutions":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Cards":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "eSecurity":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Payment & Information Services":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "IT Solutions":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Operations":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        //Group units subsites
                        case "HR":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Finance":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Corporate Centre":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Corporate Development":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "KAM":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Security":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Integration":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        //News archive subsites
                        case "News articles":
                            subWeb.Properties.Add("IsBlankSite", "0");
                            break;
                        case "Press cuttings":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Service announcements":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        //About Newco subsites
                        case "Values":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Strategy":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Objectives & KPIs":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Vision":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Terms":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Idea storming":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Events":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "Merger":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        case "In focus":
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                        default:
                            subWeb.Properties["IsBlankSite"]= "0";
                            break;
                    }
                    subWeb.Properties.Update();
                    subWeb.Update();
                }
            }

        }


        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
        }

        private static bool IsPropertyExists(SPWeb web)
        {
            return web.Properties["IsBlankSite"] != null;
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
