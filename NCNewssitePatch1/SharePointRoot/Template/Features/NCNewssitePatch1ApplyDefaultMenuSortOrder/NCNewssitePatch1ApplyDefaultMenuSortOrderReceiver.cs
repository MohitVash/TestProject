using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1ApplyDefaultMenuSortOrderReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb rootWeb = (SPWeb)properties.Feature.Parent;

            foreach (SPWeb web in rootWeb.Webs)
            {
                switch (web.Title)
                {
                    case "Business Units":
                        web.Properties.Add("SortOrder", "1000");
                        break;
                    case "Group Units":
                        web.Properties.Add("SortOrder", "2000");
                        break;
                    case "News archive":
                        web.Properties.Add("SortOrder", "3000");
                        break;
                    case "About Newco":
                        web.Properties.Add("SortOrder", "4000");
                        break;
                    default:
                        web.Properties.Add("SortOrder", "99999");
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
                            subWeb.Properties.Add("SortOrder", "1000");
                            break;
                        case "Merchant Solutions":
                            subWeb.Properties.Add("SortOrder", "2000");
                            break;
                        case "Cards":
                            subWeb.Properties.Add("SortOrder", "3000");
                            break;
                        case "eSecurity":
                            subWeb.Properties.Add("SortOrder", "4000");
                            break;
                        case "Payment & Information Services":
                            subWeb.Properties.Add("SortOrder", "5000");
                            break;
                        case "IT Solutions":
                            subWeb.Properties.Add("SortOrder", "6000");
                            break;
                        case "Operations":
                            subWeb.Properties.Add("SortOrder", "7000");
                            break;
                        //Group units subsites
                        case "HR":
                            subWeb.Properties.Add("SortOrder", "1000");
                            break;
                        case "Finance":
                            subWeb.Properties.Add("SortOrder", "2000");
                            break;
                        case "Corporate Centre":
                            subWeb.Properties.Add("SortOrder", "3000");
                            break;
                        case "Corporate Development":
                            subWeb.Properties.Add("SortOrder", "4000");
                            break;
                        case "KAM":
                            subWeb.Properties.Add("SortOrder", "5000");
                            break;
                        case "Security":
                            subWeb.Properties.Add("SortOrder", "6000");
                            break;
                        case "Integration":
                            subWeb.Properties.Add("SortOrder", "7000");
                            break;
                        //News archive subsites
                        case "News articles":
                            subWeb.Properties.Add("SortOrder", "1000");
                            break;
                        case "Press cuttings":
                            subWeb.Properties.Add("SortOrder", "2000");
                            break;
                        case "Service announcements":
                            subWeb.Properties.Add("SortOrder", "3000");
                            break;
                        //About Newco subsites
                        case "Values":
                            subWeb.Properties.Add("SortOrder", "1000");
                            break;
                        case "Strategy":
                            subWeb.Properties.Add("SortOrder", "2000");
                            break;
                        case "Objectives & KPIs":
                            subWeb.Properties.Add("SortOrder", "3000");
                            break;
                        case "Vision":
                            subWeb.Properties.Add("SortOrder", "4000");
                            break;
                        case "Terms":
                            subWeb.Properties.Add("SortOrder", "5000");
                            break;
                        case "Idea storming":
                            subWeb.Properties.Add("SortOrder", "6000");
                            break;
                        case "Events":
                            subWeb.Properties.Add("SortOrder", "7000");
                            break;
                        case "Merger":
                            subWeb.Properties.Add("SortOrder", "8000");
                            break;
                        case "In focus":
                            subWeb.Properties.Add("SortOrder", "9000");
                            break;
                        default:
                            subWeb.Properties.Add("SortOrder", "99999");
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
