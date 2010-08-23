using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssiteLatestNewsWebPartReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            base.FeatureActivated(properties);
            // if (properties.Feature.Parent is SPWeb)
            // {
            // SPWeb web = (SPWeb)properties.Feature.Parent;
            // SPSite site = web.Site;
            // 
            // }

        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            this.FeatureDeactivating(properties);
            if (properties.Feature.Parent is SPWeb)
            {
                SPWeb web = (SPWeb)properties.Feature.Parent;
                SPSite site = web.Site;

                // Removes the Feature Web Parts from the Web Part Catalog on the site collection. 

                // Find the Web Part names from the Elements collection
                List<string> webparts = new List<string>();
                SPElementDefinitionCollection elementColletion = properties.Definition.GetElementDefinitions(CultureInfo.CurrentCulture);
                foreach (SPElementDefinition element in elementColletion)
                {
                    foreach (XmlElement xmlNode in element.XmlDefinition.ChildNodes)
                    {
                        if (xmlNode.Name.Equals("File"))
                        {
                            webparts.Add(xmlNode.Attributes["Url"].Value);
                        }
                    }
                }

                // Get the Web Part Catalog
                SPList wpGallery = site.RootWeb.GetCatalog(SPListTemplateType.WebPartCatalog);

                // Find the list items that matchs the Feature Web Parts
                List<SPListItem> items = new List<SPListItem>();
                foreach (SPListItem item in wpGallery.Items)
                {
                    if (webparts.Contains(item.File.Name))
                    {
                        items.Add(item);
                    }
                }

                // Remove the Feature Web Parts from the Web Part catalog
                foreach (SPListItem item in items)
                {
                    item.Delete();
                }
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
