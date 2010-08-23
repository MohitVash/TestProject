using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace NCNewssiteApplicationPages
{
    public partial class IsBlankSite : LayoutsPageBase
    {
        private const string IsBlankSiteProperty = "IsBlankSite";
        private SPUser currentUser=null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigureControls();
        }

        /// <summary>
        /// This method is used to check for the Administrator and then enable/disable the controls accordingly
        /// </summary>
        private void ConfigureControls()
        {
            currentUser = Web.CurrentUser;
            if (!currentUser.IsSiteAdmin)
            {
                dropdownList.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                dropdownList.Enabled = true;
                btnUpdate.Enabled = true;
            }
            if (!Page.IsPostBack)
            {
                var propertyValue = Web.Properties[IsBlankSiteProperty];
                dropdownList.SelectedValue = string.Equals(propertyValue ?? "0", "1") ? "1" : "0";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var currentUser = Web.CurrentUser;
            if (currentUser.IsSiteAdmin)
            {
                Web.Properties[IsBlankSiteProperty] = dropdownList.SelectedValue;
                Web.Properties.Update();
                Web.Update();
            }
        }
    }
}