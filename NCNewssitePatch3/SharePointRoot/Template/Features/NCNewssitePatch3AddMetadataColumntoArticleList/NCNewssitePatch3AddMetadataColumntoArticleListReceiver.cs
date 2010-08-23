using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch3.EventHandlers.Features
{
    public class NCNewssitePatch3AddMetadataColumntoArticleListReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            if (properties.Feature.Parent is SPSite)
            {
                SPSite site = properties.Feature.Parent as SPSite;
                SPWeb web = site.RootWeb;
                if (web.IsRootWeb)
                {
                    SPList articleList = web.Lists.TryGetList("Articles");
                    Guid guid = new Guid("{23F27201-BEE3-471e-B2E7-B64FD8B7CA38}");
                    if (!articleList.Fields.Contains(guid))
                    {
                        SPField field = articleList.ParentWeb.AvailableFields[guid];
                        field.Title = "Managed Keywords";
                        field.Description = "";
                        articleList.Fields.Add(field);
                    }
                    articleList.Update();
                }
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
