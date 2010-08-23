using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1PortalSetupReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            //Create root site
            SPWeb rootWeb = (SPWeb)properties.Feature.Parent;

            rootWeb.ApplyWebTemplate("NCNewssite#1");

            //Create Business units and subsites
            SPWeb businessUnits = rootWeb.Webs.Add("bu", "Business Units", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb financialAcquring = businessUnits.Webs.Add("financial", "Financial Acquring", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb merchantSolutions = businessUnits.Webs.Add("merchant", "Merchant Solutions", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb cards = businessUnits.Webs.Add("cards", "Cards", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb eSecurity = businessUnits.Webs.Add("esecurity", "eSecurity", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb payment = businessUnits.Webs.Add("payment", "Payment & Information Services", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb itSolutions = businessUnits.Webs.Add("itsolutions", "IT Solutions", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb operations = businessUnits.Webs.Add("operations", "Operations", "Desc", 1033, "NCNewssite#1", false, false);

            //Create Group units and subsites
            SPWeb groupUnits = rootWeb.Webs.Add("gu", "Group Units", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb hr = groupUnits.Webs.Add("hr", "HR", "Desc", 1033, "NCNewssite#1", false, false);            
            SPWeb finance = groupUnits.Webs.Add("finance", "Finance", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb corporateCentre = groupUnits.Webs.Add("corporatecenter", "Corporate Centre", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb corporateDevelopment = groupUnits.Webs.Add("corporatedevelopment", "Corporate Development", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb kam = groupUnits.Webs.Add("kam", "KAM", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb security = groupUnits.Webs.Add("security", "Security", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb integration = groupUnits.Webs.Add("integration", "Integration", "Desc", 1033, "NCNewssite#1", false, false);

            //News archive and subsites
            SPWeb newsArchive = rootWeb.Webs.Add("newsarchive", "News archive", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb newsArticles = newsArchive.Webs.Add("newsarticles", "News articles", "Desc", 1033, "NCNewssite#1", false, false);
	        SPWeb pressCuttings = newsArchive.Webs.Add("presscuttings", "Press cuttings", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb serviceAnnouncements = newsArchive.Webs.Add("serviceannouncements", "Service announcements", "Desc", 1033, "NCNewssite#1", false, false);
          
            //About Newco and subsites
            SPWeb about = rootWeb.Webs.Add("about", "About Newco", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb values = about.Webs.Add("values", "Values", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb strategy = about.Webs.Add("strategy", "Strategy", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb objectives = about.Webs.Add("objectives", "Objectives & KPIs", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb vision = about.Webs.Add("vision", "Vision", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb terms = about.Webs.Add("termes", "Terms", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb ideaStorming = about.Webs.Add("ideastorming", "Idea storming", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb events = about.Webs.Add("events", "Events", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb merger = about.Webs.Add("merger", "Merger", "Desc", 1033, "NCNewssite#1", false, false);
            SPWeb inFocus = about.Webs.Add("infocus", "In focus", "Desc", 1033, "NCNewssite#1", false, false);
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
