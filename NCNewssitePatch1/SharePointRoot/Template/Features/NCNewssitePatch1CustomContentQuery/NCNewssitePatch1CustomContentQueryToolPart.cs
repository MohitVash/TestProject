using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace NCNewssitePatch1.UI.WebControls.WebParts
{
    public class NCNewssitePatch1CustomContentQueryToolPart : Microsoft.SharePoint.WebPartPages.ToolPart
    {
        private NCNewssitePatch1CustomContentQueryWebPart webpart;
        private Table toolPanelTable;
        private Panel toolPanel;
        private DropDownList dropdown;
        private DropDownList publishAtDropdown;
        private HiddenField articleID;
        private HiddenField articleTitle;
        private HtmlButton dialogButton;
        private LiteralControl displayArticleTitle;
        private RadioButton optPublishAt;
        private RadioButton optManual;

        public NCNewssitePatch1CustomContentQueryToolPart()
        {
            this.Title = "Newco Content Query";
        }

        public override void ApplyChanges()
        {
            EnsureChildControls();
            try
            {
                webpart.FilterValue1 = optPublishAt.Checked ? "-1" : articleID.Value;
                webpart.PublishAt = publishAtDropdown.SelectedValue;
                webpart.DisplayLatestArticle = optPublishAt.Checked;
                webpart.ItemStyle = dropdown.SelectedValue;

                if (dropdown.SelectedValue == "TopStory")
                {
                    webpart.DataMappings = "Description:{98c14c58-a245-4909-8ae8-d23478faf6d4},FrontpageHeader1,Note;|SipAddress:|Title:{fa564e0f-0c70-4ab9-b863-0177e6ddd247},Title,Text;|OpenInNewWindow:|ImageUrl:{940e4513-a310-45c5-b4bf-95422122d31d},FrontpageTopnewsImage,Image;|LinkUrl:|LinkToolTip:|EnglishUrl:{d7d88391-7808-4156-94d6-8d39e712dd0b},EnglishUrl,Text;|NorwegianUrl:{40c73c73-9c4e-45ec-b470-6cfce4c7c038},NorwegianUrl,Text;|DanishUrl:{a416ee3e-30a3-45a3-86a5-27af4c8f6338},DanishUrl,Text;|ArticleAuthor:{66c3b032-1a98-4920-a344-5ff177279042},ArticleAuthor,Text;|";
                    webpart.DataMappingViewFields = "{940e4513-a310-45c5-b4bf-95422122d31d},Image;{fa564e0f-0c70-4ab9-b863-0177e6ddd247},Text;{98c14c58-a245-4909-8ae8-d23478faf6d4},Note;{d7d88391-7808-4156-94d6-8d39e712dd0b},Text;{40c73c73-9c4e-45ec-b470-6cfce4c7c038},Text;{a416ee3e-30a3-45a3-86a5-27af4c8f6338},Text;{66c3b032-1a98-4920-a344-5ff177279042},Text;";
                }
                else if (dropdown.SelectedValue == "ProfileNews")
                {
                    webpart.DataMappings = "Description:{82ffa31b-e665-4363-9070-1aaf35c22bec},FrontpageHeader2,Note;|SipAddress:|Title:{fa564e0f-0c70-4ab9-b863-0177e6ddd247},Title,Text;|OpenInNewWindow:|ImageUrl:{d6dd133f-bf07-42f2-a796-2d72c7486eb1},FrontpageProfilenewsImage,Image;|LinkUrl:|LinkToolTip:|EnglishUrl:{d7d88391-7808-4156-94d6-8d39e712dd0b},EnglishUrl,Text;|NorwegianUrl:{40c73c73-9c4e-45ec-b470-6cfce4c7c038},NorwegianUrl,Text;|DanishUrl:{a416ee3e-30a3-45a3-86a5-27af4c8f6338},DanishUrl,Text;|ArticleAuthor:{66c3b032-1a98-4920-a344-5ff177279042},ArticleAuthor,Text;|";
                    webpart.DataMappingViewFields = "{d6dd133f-bf07-42f2-a796-2d72c7486eb1},Image;{fa564e0f-0c70-4ab9-b863-0177e6ddd247},Text;{82ffa31b-e665-4363-9070-1aaf35c22bec},Note;{d7d88391-7808-4156-94d6-8d39e712dd0b},Text;{40c73c73-9c4e-45ec-b470-6cfce4c7c038},Text;{a416ee3e-30a3-45a3-86a5-27af4c8f6338},Text;{66c3b032-1a98-4920-a344-5ff177279042},Text;";
                }
                else
                {
                    webpart.DataMappings = "Description:{98c14c58-a245-4909-8ae8-d23478faf6d4},FrontpageHeader1,Note;|SipAddress:|Title:{fa564e0f-0c70-4ab9-b863-0177e6ddd247},Title,Text;|OpenInNewWindow:|ImageUrl:{940e4513-a310-45c5-b4bf-95422122d31d},FrontpageTopnewsImage,Image;|LinkUrl:|LinkToolTip:|";
                    webpart.DataMappingViewFields = "{fa564e0f-0c70-4ab9-b863-0177e6ddd247},Text;{98c14c58-a245-4909-8ae8-d23478faf6d4},Note;";
                }

                base.ApplyChanges();
            }
            catch { }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string webLocale = SPContext.Current.Web.Locale.LCID.ToString();
            Page.ClientScript.RegisterClientScriptInclude("PickerTreeDialog", string.Format("/_layouts/{0}/sp.js", webLocale));

            StringBuilder text = new StringBuilder();
            text.Append("<script type=\"text/javascript\">");

            text.Append("function OpenDialog(dialogUrl) {");
            text.Append("var options = SP.UI.$create_DialogOptions();");
            text.Append("options.title = 'Search for article';");
            text.Append("allowMaximize = false,");
            text.Append("options.url = dialogUrl;");
            text.Append("options.dialogReturnValueCallback = CloseCallback;");
            text.Append("SP.UI.ModalDialog.showModalDialog(options);");
            text.Append("}");
            text.Append("</script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dialogscript", text.ToString(), false);
        }

        protected string GetPostBack()
        {
            return Page.ClientScript.GetPostBackEventReference(this, "SelectArticlePostback");
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            webpart = this.WebPartToEdit as NCNewssitePatch1CustomContentQueryWebPart;

            toolPanel = new Panel();
            toolPanel.Controls.Add(GetToolPanel());

            publishAtDropdown.SelectedValue = webpart.PublishAt;
            optPublishAt.Checked = webpart.DisplayLatestArticle;
            optManual.Checked = !webpart.DisplayLatestArticle;

            this.Controls.Add(toolPanel);
        }

        #region Tool Panel Controls

        private Control GetToolPanel()
        {
            toolPanelTable = new Table();

            toolPanelTable.CellPadding = 0;
            toolPanelTable.CellSpacing = 0;
            toolPanelTable.Attributes.Add("width", "100%");

            toolPanelTable.Rows.Add(GetLayoutTemplateRow());
            toolPanelTable.Rows.Add(GetSeperatorRow());
            toolPanelTable.Rows.Add(GetPublishAtRow());
            toolPanelTable.Rows.Add(GetArticleSearchRow());

            return toolPanelTable;
        }

        private TableRow GetLayoutTemplateRow()
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            Panel userControlGroup = new Panel();

            cell.Controls.Add(new LiteralControl("<div class=\"UserSectionHead\"><label>Layout</label></div>"));

            dropdown = new DropDownList();
            dropdown.Items.Add(new ListItem("Top news story", "TopStory"));
            dropdown.Items.Add(new ListItem("Profile news", "ProfileNews"));
            dropdown.Items.Add(new ListItem("News list story", "NewsList"));
            dropdown.SelectedValue = webpart.ItemStyle;

            userControlGroup.CssClass = "UserControlGroup";
            userControlGroup.Controls.Add(dropdown);
            cell.Controls.Add(userControlGroup);

            row.Cells.Add(cell);
            return row;
        }

        private TableRow GetPublishAtRow()
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            Panel userControlGroup = new Panel();
            publishAtDropdown = new DropDownList();

            publishAtDropdown.ID = "ddlPublishAt";
            publishAtDropdown.Items.Insert(0, new ListItem("(None)"));
            SPListItemCollection publishAtItems = GetListItems("Organization Units", new SPQuery());

            if (publishAtItems != null)
            {
                foreach (SPListItem item in publishAtItems)
                {
                    publishAtDropdown.Items.Add(Convert.ToString(item["Title"]));
                }
            }

            optPublishAt = new RadioButton();
            optPublishAt.Text = "Publish at ";
            optPublishAt.GroupName = "nc-contentquery-search";

            userControlGroup.CssClass = "UserControlGroup";
            userControlGroup.Controls.Add(optPublishAt);
            userControlGroup.Controls.Add(publishAtDropdown);
            cell.Controls.Add(userControlGroup);

            row.Cells.Add(cell);
            return row;
        }

        private TableRow GetArticleSearchRow()
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            Panel userControlGroup = new Panel();

            dialogButton = new HtmlButton();

            articleID = new HiddenField();
            articleID.ID = "articleID";
            articleTitle = new HiddenField();
            articleTitle.ID = "articleTitle";
            dialogButton.ID = "btnsearch";
            dialogButton.Attributes.Add("class", "UserButton");

            StringBuilder text = new StringBuilder();
            text.Append("<script type=\"text/javascript\">");
            text.Append("function CloseCallback(dialogResult, returnValue)");
            text.Append("{");
            text.Append("var articleIDControl = document.getElementById('" + base.ClientID + "_" + articleID.ClientID + "');");
            text.Append("var articleTitleControl = document.getElementById('" + base.ClientID + "_" + articleTitle.ClientID + "');");
            text.Append("var articleArray = returnValue.split('|');");
            text.Append("articleIDControl.value = articleArray[0];");
            text.Append("articleTitleControl.value = articleArray[1];");
            text.Append(GetPostBack());
            text.Append("}");
            text.Append("function ToggleSearch(enabled)");
            text.Append("{");
            text.Append("var btnSearchControl = document.getElementById('" + base.ClientID + "_" + dialogButton.ClientID + "');");
            text.Append("btnSearchControl.disabled=enabled;");
            text.Append("var dropdownControl = document.getElementById('" + base.ClientID + "_" + publishAtDropdown.ClientID + "');");
            text.Append("dropdownControl.disabled=!enabled;");
            text.Append("}");
            text.Append("</script>");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "callbackscript", text.ToString(), false);

            articleID.Value = this.webpart.FilterValue1;

            dialogButton.InnerText = "...";

            string serverRelativeUrl;
            if (SPContext.Current.Web.ServerRelativeUrl == "/")
                serverRelativeUrl = "";
            else
                serverRelativeUrl = SPContext.Current.Web.ServerRelativeUrl;

            dialogButton.Attributes.Add("onclick", "javascript: OpenDialog('" + serverRelativeUrl + "/_layouts/NCNewssiteApplicationPages/ArticleSelectDialog.aspx');");
            dialogButton.Attributes.Add("type", "Button");
            userControlGroup.CssClass = "UserControlGroup";
            displayArticleTitle = new LiteralControl("tmp title");
            
            userControlGroup.Controls.Add(articleID);
            userControlGroup.Controls.Add(articleTitle);

            optManual = new RadioButton();
            optManual.Text = "Select article ";
            optManual.GroupName = "nc-contentquery-search";

            userControlGroup.Controls.Add(optManual);
            userControlGroup.Controls.Add(dialogButton);

            cell.Controls.Add(userControlGroup);

            row.Cells.Add(cell);
            return row;
        }

        private TableRow GetSeperatorRow()
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Controls.Add(new LiteralControl("<div style='width:100%' class='UserDottedLine'></div>"));
            row.Cells.Add(cell);
            return row;
        }

        #endregion

        /// <summary>
        /// This helper method used to execute query on the list
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