using System.Collections.Generic;
using Microsoft.SharePoint;

namespace NCNewssitePatch2.EventHandlers.Features
{
    public class NCNewssitePatch2ModifyArticlePageCTReceiver : SPFeatureReceiver
    {
        private const string ShowOnLeftMenuProperty = "ShowOnLeftMenu";
        private const string ShowOnBreadcrumbProperty = "ShowOnBreadcrumb";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb rootWeb=(SPWeb)properties.Feature.Parent;
            ModifyPageContentType(rootWeb);

            foreach (SPWeb web in rootWeb.Webs)
            {
                ModifyPageContentType(web);
            }
        }

        /// <summary>
        /// Thie method will add navigation properties to the pages list
        /// </summary>
        /// <param name="web"></param>
        private void ModifyPageContentType(SPWeb web)
        {
            SPList pageList = web.Lists.TryGetList("Pages");
            if (pageList == null) return;

            if (!pageList.Fields.ContainsField(ShowOnLeftMenuProperty))
            {
                pageList.Fields.Add(ShowOnLeftMenuProperty, SPFieldType.Boolean, true);
            }
            if (!pageList.Fields.ContainsField(ShowOnBreadcrumbProperty))
            {
                pageList.Fields.Add(ShowOnBreadcrumbProperty, SPFieldType.Boolean, true);
            }

            if (web.IsRootWeb)
            {
                SPContentType ct = web.ContentTypes["Article Page"];
                if (ct == null) return;
                SPFieldLinkCollection spFieldLinks = ct.FieldLinks;
                foreach (SPFieldLink spFieldLink in spFieldLinks)
                {
                    string name = spFieldLink.Name;
                    switch (name)
                    {
                        case ShowOnBreadcrumbProperty:
                            spFieldLink.Hidden = false;
                            break;
                        case ShowOnLeftMenuProperty:
                            spFieldLink.Hidden = false;
                            break;
                    }
                }
                ct.Update();
            }

            pageList.Update();
            web.Update();
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWeb rootWeb = (SPWeb)properties.Feature.Parent;
            DeleteFields(rootWeb);

            foreach (SPWeb web in rootWeb.Webs)
            {
                DeleteFields(web);
            }
        }

        /// <summary>
        /// This method will deletes the fields from the "Pages" list
        /// </summary>
        /// <param name="web"></param>
        private void DeleteFields(SPWeb web)
        {
            SPList pageList = web.Lists.TryGetList("Pages");

            if (pageList != null)
            {
                if (pageList.Fields.ContainsField(ShowOnLeftMenuProperty))
                {
                    pageList.Fields.Delete(ShowOnLeftMenuProperty);
                }
                if (pageList.Fields.ContainsField(ShowOnBreadcrumbProperty))
                {
                    pageList.Fields.Delete(ShowOnBreadcrumbProperty);
                }
            }
            pageList.Update();
            web.Update();
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
