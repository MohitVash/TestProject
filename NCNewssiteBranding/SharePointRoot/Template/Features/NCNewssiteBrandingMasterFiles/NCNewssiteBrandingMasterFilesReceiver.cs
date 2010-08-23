using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssiteBranding.EventHandlers.Features
{
    public class NCNewssiteBrandingMasterFilesReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            web.AllProperties["oldMaster"] = web.MasterUrl;
            if (web.MasterUrl.EndsWith("/v4.master") || web.MasterUrl.Equals("v4.master"))
            {
                web.MasterUrl = GetMasterPageWithPath(web, "ncnewssite.master");
            }
            web.AllProperties["oldCustomMaster"] = web.CustomMasterUrl;
            if (web.CustomMasterUrl.EndsWith("/v4.master") || web.CustomMasterUrl.Equals("v4.master"))
            {
                web.CustomMasterUrl = GetMasterPageWithPath(web, "ncnewssite.master");
            }

            web.Update();
        }

        private string GetMasterPageWithPath(SPWeb web, string abbMasterFile)
        {
            if (web.ServerRelativeUrl.Equals("/"))
            {
                return "/_catalogs/masterpage/" + abbMasterFile;
            }
            else
            {
                return web.ServerRelativeUrl + "/_catalogs/masterpage/" + abbMasterFile;
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            if (web.AllProperties["oldMaster"] != null)
            {
                web.MasterUrl = web.AllProperties["oldMaster"].ToString();
            }
            if (web.AllProperties["oldCustomMaster"] != null)
            {
                web.CustomMasterUrl = web.AllProperties["oldCustomMaster"].ToString();
            }

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
