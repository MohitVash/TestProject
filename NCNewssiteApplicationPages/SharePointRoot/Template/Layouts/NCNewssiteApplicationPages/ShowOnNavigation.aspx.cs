using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;

namespace NCNewssiteApplicationPages
{
    public partial class ShowOnNavigation : LayoutsPageBase
    {
        private const string ShowOnBreadcrumbProperty = "ShowOnBreadcrumb";
        private const string ShowOnLeftMenuProperty = "ShowOnLeftMenu";
        private SPUser currentUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfiguringControls();
        }

        /// <summary>
        /// This method is used to prefill the values in the dropdowns after fetching existing values of properties 
        /// </summary>
        private void ConfiguringControls()
        {
            if (!Page.IsPostBack)
            {
                var propertyValue = Web.Properties[ShowOnBreadcrumbProperty];
                ddBreadcrumb.SelectedValue = string.Equals(propertyValue, "1") ? "1" : "0";

                var leftNavPropertyValue = Web.Properties[ShowOnLeftMenuProperty];
                ddLeftNav.SelectedValue = string.Equals(leftNavPropertyValue, "1") ? "1" : "0";
            }
            ddBreadcrumb.Enabled = false;
            btnUpdate.Enabled = false;
            ddLeftNav.Enabled = false;
            currentUser = Web.CurrentUser;
            if (currentUser.IsSiteAdmin)
            {
                if (IsValidWebLevel())
                {
                    ddBreadcrumb.Enabled = true;
                    btnUpdate.Enabled = true;
                    ddLeftNav.Enabled = true;
                }
            }
        }

        /// <summary>
        /// This Method validates web belongs to level1 or level2 of websites
        /// </summary>
        /// <returns></returns>
        private bool IsValidWebLevel()
        {
            bool flag = true;
            ArrayList levelOneAndTwoSites = new ArrayList();
            levelOneAndTwoSites.Add("NCBBSNews");
            levelOneAndTwoSites.Add("Business Units");
            levelOneAndTwoSites.Add("Group Units");
            levelOneAndTwoSites.Add("News archive");
            levelOneAndTwoSites.Add("About Newco");
            levelOneAndTwoSites.Add("Financial Acquring");
            levelOneAndTwoSites.Add("Merchant Solutions");
            levelOneAndTwoSites.Add("Cards");
            levelOneAndTwoSites.Add("eSecurity");
            levelOneAndTwoSites.Add("Payment & Information Services");
            levelOneAndTwoSites.Add("IT Solutions");
            levelOneAndTwoSites.Add("Operations");
            levelOneAndTwoSites.Add("HR");
            levelOneAndTwoSites.Add("Corporate Centre");
            levelOneAndTwoSites.Add("Corporate Development");
            levelOneAndTwoSites.Add("KAM");
            levelOneAndTwoSites.Add("Security");
            levelOneAndTwoSites.Add("Integration");
            levelOneAndTwoSites.Add("News articles");
            levelOneAndTwoSites.Add("Objectives & KPIs");
            levelOneAndTwoSites.Add("Press cuttings");
            levelOneAndTwoSites.Add("Service announcements");
            levelOneAndTwoSites.Add("Values");
            levelOneAndTwoSites.Add("Strategy");
            levelOneAndTwoSites.Add("Terms");
            levelOneAndTwoSites.Add("Idea storming");
            levelOneAndTwoSites.Add("Vision");
            levelOneAndTwoSites.Add("Events");
            levelOneAndTwoSites.Add("Merger");
            levelOneAndTwoSites.Add("Events");
            levelOneAndTwoSites.Add("In focus");

            if (levelOneAndTwoSites.Contains(Web.Title))
                flag = false;
            else
                flag = true;
           
            return flag;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var currentUser = Web.CurrentUser;
            if (currentUser.IsSiteAdmin)
            {
                Web.Properties[ShowOnLeftMenuProperty] = ddLeftNav.SelectedValue;
                Web.Properties[ShowOnBreadcrumbProperty] = ddBreadcrumb.SelectedValue;
                Web.Properties.Update();
                Web.Update();
            }            
        }
    }
}