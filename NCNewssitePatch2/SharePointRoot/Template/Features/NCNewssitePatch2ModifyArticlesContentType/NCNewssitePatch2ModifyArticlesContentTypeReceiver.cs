using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch2.EventHandlers.Features
{
    public class NCNewssitePatch2ModifyArticlesContentTypeReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            base.FeatureActivated(properties);

            SPSite site = (SPSite)properties.Feature.Parent;
            SPWeb web = site.RootWeb;
            SPList list = web.Lists["Articles"];

            //SPContentType newContentType = web.AvailableContentTypes["Article2"];
            SPContentType oldContentType = list.ContentTypes["Article"];

            oldContentType.EditFormTemplateName = "NCArticleListEditForm";
            oldContentType.NewFormTemplateName = "NCArticleListEditForm";
                    
            list.ContentTypesEnabled = true;

            //try
            //{
            //    list.ContentTypes.Add(newContentType);
            //}
            //catch { }


            //try
            //{
            //    list.ContentTypes.Delete(oldContentType.Id);
            //}
            //catch { }

            oldContentType.Update();
            list.Update();
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
