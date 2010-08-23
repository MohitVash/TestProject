using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using JohnHolliday.Caml.Net;
using System.Linq;


namespace NCNewssitePatch1.UI.WebControls.WebParts
{
    [Guid("dc70cffd-24c4-4915-83cd-6f362ef3e866")]
    public class NCNewssiteLatestNewsWebPartWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        private bool _error = false;

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("List name")]
        [WebDescription("Name of the list to get items from")]
        public string ListName { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Organization unit")]
        [WebDescription("Organization unit to get items for")]
        public string OrganizationUnit { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Number of items to display")]
        [WebDescription("Limit the number of items")]
        public int RowLimit { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Date field")]
        [WebDescription("Select field for the date")]
        public string DateField { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Show both date and time")]
        [WebDescription("Select if both date and time should be shown")]
        public bool ShowTime { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Filter on Publishing Start/End")]
        [WebDescription("Select if Publishing Start/End filter should be applied")]
        public bool PublishStartEndFilter { get; set; }
        
        
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Title field")]
        [WebDescription("Select field for the title")]
        public string TitleField { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Description field")]
        [WebDescription("Select field for the description")]
        public string DescriptionField { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Url to archive")]
        [WebDescription("Url to archive with all items")]
        public string ArchiveUrl { get; set; }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("NC Newssite")]
        [WebDisplayName("Text for archive url")]
        [WebDescription("Set text to display on archive url")]
        public string ArchiveUrlText { get; set; }

       
        public NCNewssiteLatestNewsWebPartWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        /// <summary>
        /// Create all your controls here for rendering.
        /// Try to avoid using the RenderWebPart() method.
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!_error)
            {
                try
                {

                    base.CreateChildControls();

                    Controls.Add(new LiteralControl(@"<div class=""span-1 last latestNews""><div class=""boxed"">"));

                    // Your code here...
                    foreach (SPListItem item in GetLatestNews())
                    {
                        string dateField = this.DateField;
                        if (dateField == null || dateField == "")
                            dateField = "Modified";

                        string dateFormatted;
                        if (this.ShowTime == false)
                        {
                            dateFormatted = FormatDate((DateTime)item[dateField], false);
                        }
                        else
                        {
                            dateFormatted = FormatDate((DateTime)item[dateField], true);
                        }


                        string titleField = this.TitleField;
                        if (titleField == null || titleField == "")
                            titleField = "Title";

                        string itemUrl = String.Concat(item.Web.Url, "/",
                          item.ParentList.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url, "?id=", item.ID.ToString());

                        LiteralControl dateLiteral = new LiteralControl(dateFormatted + "<br/>");

                        HtmlAnchor linkTitle = new HtmlAnchor();
                        linkTitle.HRef = itemUrl;
                        linkTitle.InnerText = item[titleField].ToString();

                        HtmlGenericControl strongElement = new HtmlGenericControl("strong");
                        strongElement.Controls.Add(linkTitle);

                        HtmlGenericControl paragraphElement = new HtmlGenericControl("p");

                        paragraphElement.Controls.Add(dateLiteral);
                        paragraphElement.Controls.Add(strongElement);

                        Controls.Add(paragraphElement);

                        if (this.DescriptionField != null)
                        {
                            HtmlGenericControl divDesc = new HtmlGenericControl("div");
                            divDesc.InnerHtml = item[this.DescriptionField].ToString();

                            Controls.Add(divDesc);
                        }
                    }

                    if (GetLatestNews().Count > 0)
                    {
                        string archiveUrl = this.ArchiveUrl;
                        if (archiveUrl == null || archiveUrl == "")
                        {
                            archiveUrl = GetListViewUrl();
                        }

                        string archiveUrlText = this.ArchiveUrlText;
                        if (archiveUrlText == null || archiveUrlText == "")
                            archiveUrlText = "Read more news »";

                        HtmlAnchor linkArchive = new HtmlAnchor();
                        linkArchive.HRef = archiveUrl;
                        linkArchive.InnerText = archiveUrlText;

                        HtmlGenericControl divArchiveUrl = new HtmlGenericControl("p");
                        divArchiveUrl.Attributes["class"] = "more";
                        divArchiveUrl.Controls.Add(linkArchive);

                        Controls.Add(divArchiveUrl);
                    }
                    Controls.Add(new LiteralControl(@"</div></div>"));
                    Controls.Add(new LiteralControl(@"<div style=""clear: both;""></div>"));
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }




        private SPListItemCollection GetLatestNews()
        {
            SPWeb web = SPContext.Current.Web;

            string listName = this.ListName;
            if (listName == null || listName == "")
            {
                throw new Exception("No list is set. Set a list name in the tool pane");
            }

            //if list doesn't exists in current web, use rootweb
            if (ListExists(web, listName) == false)
            {
                web = SPContext.Current.Site.RootWeb;
            }

            SPList list = web.Lists[this.ListName];


            string dateField = this.DateField;
            if (dateField == null || dateField == "")
                dateField = "Created";

            string query = ""; // = "<OrderBy><FieldRef Name="+ dateField +" Ascending=\"FALSE\" /></OrderBy>";



            if (this.OrganizationUnit != null && this.OrganizationUnit != "")
            {

                query += CAML.Eq(CAML.FieldRef("Organization_x0020_Unit"), CAML.Value(this.OrganizationUnit));

            }

            if (this.PublishStartEndFilter)
            {
                string queryOnPublishing = CAML.And(CAML.Leq(CAML.FieldRef("PublishingStart"), CAML.Value("DateTime", CAMLDateFormat(DateTime.Now))),
                                            CAML.Geq(CAML.FieldRef("PublishingEnd"), CAML.Value("DateTime", CAMLDateFormat(DateTime.Now))));

                query = string.IsNullOrEmpty(query) ? queryOnPublishing : CAML.And(queryOnPublishing, query);
            }

            query = CAML.Where(query);
            query += "<OrderBy><FieldRef Name=" + dateField + " Ascending=\"FALSE\" /></OrderBy>";
            SPQuery articleQuery = new SPQuery();
            articleQuery.Query = query;
            articleQuery.RowLimit = (uint)this.RowLimit;

            return list.GetItems(articleQuery);
        }

        public static bool ListExists(SPWeb web, string listName)
        {
            return web.Lists.Cast<SPList>().Any(list => string.Equals(list.Title, listName));
        }

        private string GetListViewUrl()
        {
            SPWeb web = SPContext.Current.Web;

            //if list doesn't exists in current web, use rootweb
            if (ListExists(web, this.ListName) == false)
            {
                web = SPContext.Current.Site.RootWeb;
            }

            SPList list = web.Lists[this.ListName];

            return list.DefaultViewUrl;
        }


        /// <summary>
        /// Ensures that the CreateChildControls() is called before events.
        /// Use CreateChildControls() to create your controls.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!_error)
            {
                try
                {
                    base.OnLoad(e);
                    this.EnsureChildControls();

                    // Your code here...
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Clear all child controls and add an error message for display.
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            this._error = true;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl(ex.Message));
        }


        private string CAMLDateFormat(DateTime dt)
        {
            string day = dt.Day.ToString().PadLeft(2, '0');
            string month = dt.Month.ToString().PadLeft(2, '0');
            string year = dt.Year.ToString();
            string hours = dt.Hour.ToString().PadLeft(2, '0');
            string minutes = dt.Minute.ToString().PadLeft(2, '0');
            return year + "-" + month + "-" + day + "T" + hours + ":" + minutes + ":" + "00" + "Z";

        }

        private string FormatDate(DateTime dt, bool showTime)
        {
            string day = dt.Day.ToString().PadLeft(2, '0');
            string month = dt.Month.ToString().PadLeft(2, '0');
            string year = dt.Year.ToString();
            string hours = dt.Hour.ToString().PadLeft(2, '0');
            string minutes = dt.Minute.ToString().PadLeft(2, '0');

            if (showTime)
            {
                return day + "/" + month + "/" + year + " " + hours + ":" + minutes;
            }
            else
            {
                return day + "/" + month + "/" + year;
            }

        }



    }
}
