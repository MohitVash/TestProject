using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
namespace NCNewssiteApplicationPages
{
    public partial class SortOrder : LayoutsPageBase {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextBox1.Text = Web.Properties["SortOrder"];
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Web.Properties["SortOrder"] = TextBox1.Text;
            Web.Properties.Update();
            Web.Update();
        }
    }
}