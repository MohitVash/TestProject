using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1ArticleContentTypeReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPSite site = (SPSite)properties.Feature.Parent;

            SPWeb rootweb = site.RootWeb;
            SPContentType ct = rootweb.ContentTypes["Article"];

            SPList list = rootweb.Lists["Organization Units"];

            String internalname = rootweb.Fields.AddLookup("Organization Unit", list.ID, rootweb.ID, true);
            
            SPFieldLookup lookup = (SPFieldLookup)rootweb.Fields.GetFieldByInternalName(internalname);
            lookup.AllowMultipleValues = true;
            lookup.LookupField = "Title";
            lookup.Group = "NCNewssite";
            lookup.Update(true);
            lookup.StaticName = "OrganizationUnit";
            
            SPFieldLink fieldLink = new SPFieldLink(lookup);
            
            ct.FieldLinks.Add(fieldLink);
            //Reorder fields
            List<string> fieldNames = new List<string>(){"Title", "ArticleAuthor", "PublishingStart", "PublishingEnd", "DanishUrl", 
                "NorwegianUrl", "EnglishUrl", internalname, "FrontpageHeader1", "FrontpageTopnewsImage", "FrontPageHeader2", 
                "FrontpageProfilenewsImage", "ArticleHeader", "ArticleTopImage", "ArticleTopImageText", "ArticleBodyText", 
                "RightColumnLinks", "RightColumnFacts"};

            ct.FieldLinks.Reorder(fieldNames.ToArray());
            ct.Update(true);

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
