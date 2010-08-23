using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using JohnHolliday.Caml.Net;

namespace NCNewssiteApplicationPages
{
    public partial class BlogSelectDialog : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SPQuery qry = new SPQuery();

     
                
                //SPWeb rootWeb = Site.RootWeb;

                SPList list = Web.Lists["Blog"];
               


                CamlQuery caml = new CamlQuery();

                SPQuery articleQuery = new SPQuery();

                articleQuery.Query = CAML.Where(
                                    CAML.Contains(
                                        CAML.FieldRef("Title"),
                                        CAML.Value(txtTitleSearch.Text)));


                //articleQuery.ViewFields = CAML.ViewFields(CAML.FieldRef("Title"), CAML.FieldRef("Modified By"));
                //articleQuery.ViewFields = string.Concat(
                //                    "<FieldRef Name='Title' />",
                //                   "<FieldRef Name='Modified' />");

                //articleQuery.ViewFieldsOnly = true;
                //articleQuery.ViewFields = "<ViewFields><FieldRef Name=\"Title\"/></ViewFields>";


                SPListItemCollection col;

                if (txtTitleSearch.Text.Length == 0)
                {
                    col = list.Items;
                }
                else
                {
                    col = list.GetItems(articleQuery);
                }
                

                gvResult.DataSource = col.GetDataTable();
                gvResult.DataBind();


     


        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (gvResult.SelectedValue != null)
            {
                string articleId = gvResult.SelectedDataKey.Values[0].ToString() + "|" + gvResult.SelectedDataKey.Values[1].ToString();
                this.Page.Response.Clear();
                this.Page.Response.Write(string.Format("<script type=\"text/javascript\">window.frameElement.commonModalDialogClose(1, '{0}');</script>", articleId));
                this.Page.Response.End();
            }

        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvResult, "Select$" + e.Row.RowIndex);
            }

        }
    }
}