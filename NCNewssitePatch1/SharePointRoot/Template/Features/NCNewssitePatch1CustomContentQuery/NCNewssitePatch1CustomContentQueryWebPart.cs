using System;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls.WebParts;
using JohnHolliday.Caml.Net;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing.WebControls;
using Microsoft.SharePoint.WebPartPages;

namespace NCNewssitePatch1.UI.WebControls.WebParts
{
    [Guid("c39eb2a7-a360-49f7-9bcf-505a91deca79")]
    public class NCNewssitePatch1CustomContentQueryWebPart : ContentByQueryWebPart
    {
        #region Properties
        /// <summary>
        /// Property that save whether search will be manual or automatic
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Autosearch Articles")]
        [WebDescription("Check box to display latest article automatically")]
        public bool DisplayLatestArticle { get; set; }

        /// <summary>
        /// This property that saves selected Publish at value 
        /// </summary>
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Publish At")]
        [WebDescription("Property that contains selected value of publish at")]
        public String PublishAt { get; set; }
        #endregion

        public NCNewssitePatch1CustomContentQueryWebPart()
        {
            string serverRelativeUrl;
            if (SPContext.Current.Site.RootWeb.ServerRelativeUrl == "/")
                serverRelativeUrl = "";
            else
                serverRelativeUrl = SPContext.Current.Site.RootWeb.ServerRelativeUrl;

            this.ItemXslLink = serverRelativeUrl + "/Style Library/XSL Style Sheets/CustomItemStyle.xsl";

            this.FilterType1 = "Counter";
            this.FilterField1 = "ID";
            this.FilterOperator1 = FilterFieldQueryOperator.Eq;
            this.ListName = "Articles";

            this.UseCopyUtil = true;
            this.ServerTemplate = "100";
            this.WebUrl = "~sitecollection";
        }

        public override ToolPart[] GetToolParts()
        {
            return new ToolPart[] { new NCNewssitePatch1CustomContentQueryToolPart(), new WebPartToolPart() };
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DisplayLatestArticle)
                this.FilterValue1 = GetLatestArticle();

            base.OnDataBinding(e);
        }

        /// <summary>
        /// Gets the latest news from article list based on Publish start and Published at.
        /// </summary>
        /// <returns>ID of latest article</returns>
        private string GetLatestArticle()
        {
            string query = CAML.Where(CAML.And(
                CAML.Eq(CAML.FieldRef("Organization_x0020_Unit"), CAML.Value("LookupMulti", PublishAt)),
                CAML.Leq(CAML.FieldRef("PublishingStart"), CAML.Value("DateTime", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ")))
                )) + CAML.OrderBy(CAML.FieldRef("PublishingStart", CAML.SortType.Descending));

            SPListItemCollection listItems = GetListItems("Articles", new SPQuery() { Query = query });

            if (listItems != null && listItems.Count > 0)
                return Convert.ToString(listItems[0]["ID"]);

            return string.Empty;
        }

        /// <summary>
        /// This helper method used to execute query on list
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="query"></param>
        /// <returns>List of items</returns>
        private static SPListItemCollection GetListItems(string listName, SPQuery query)
        {
            SPWeb rootWeb = SPContext.Current.Site.RootWeb;
            SPList list = rootWeb.Lists.TryGetList(listName);
            return list.GetItems(query);
        }
    }
}
