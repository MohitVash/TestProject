using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssiteCore.EventHandlers.Features
{
    public class NCNewssiteCoreUpgradeReceiver : SPFeatureReceiver
    {
        protected readonly string solutionId = "a97fef3f-216d-4553-a9ae-e512a2f210d9";
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;
            SPSite site = web.Site;
            SPFarm farm = SPFarm.Local;
            // Create a list for our specialization features
            SortedList<int, SPFeatureDefinition> upgradeFeatures =
                new SortedList<int, SPFeatureDefinition>();
            // Iterate through all farm installed features
            foreach (SPFeatureDefinition featureDef
                in farm.FeatureDefinitions)
            {
                // If the feature is an upgrade to this solution
                if (featureDef.Properties["UpgradeSolutionId"] != null
                    &&
                    featureDef.Properties["UpgradeSolutionId"].Value
                    == solutionId)
                {
                    // Find the priority (to support multiple
                    // specialization solutions)
                    int Priority = 0;
                    if (featureDef.Properties["UpgradePriority"] != null)
                    {
                        // NOTE: This line breaks due to length:
                        int.TryParse(featureDef.Properties["UpgradePriority"].Value.ToString(),
                            out Priority);
                    }
                    try
                    {
                        // Add the feature as an upgrade to our solution
                        upgradeFeatures.Add(Priority, featureDef);
                    }
                    catch (ArgumentException)
                    {
                        // If we cannot add the feature to the collection
                        // it means that we have a duplicate 
                        // UpgradePriority in two features
                        throw new
                            SPException(@"Error in upgrade package, verify 
                              that the UpgradePriority property 
                              is unique for all specialization 
                              features");
                    }
                }
            }
            // Iterate all the specialization features
            foreach (KeyValuePair<int, SPFeatureDefinition> upgradeFeature
                in upgradeFeatures)
            {
                SPFeatureDefinition featureDefinition =
                    upgradeFeature.Value;
                // Activate the feature at the correct scope
                if (featureDefinition.Scope == SPFeatureScope.Web)
                {
                    if (web.Features[featureDefinition.Id] == null)
                    {
                        web.Features.Add(featureDefinition.Id);
                    }
                }
                else if (featureDefinition.Scope == SPFeatureScope.Site)
                {
                    if (site.Features[featureDefinition.Id] == null)
                    {
                        site.Features.Add(featureDefinition.Id);
                    }
                }
            }
            // Finally, deactivate the upgrade feature to deactivate
            // the upgrade feature.
            web.Features.Remove(properties.Feature.Definition.Id);

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
