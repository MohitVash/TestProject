using System;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Web.UI.HtmlControls;

namespace NCNewssiteMiniCalendar.UI.WebControls.WebParts
{
    public class NCMiniCalendarToolpart : ToolPart
    {
        DropDownList cboList = new DropDownList();
        DropDownList cboField = new DropDownList();
        DropDownList cboEndField = new DropDownList();
        DropDownList cboTooltipField = new DropDownList();

        public NCMiniCalendarToolpart()
        {
            this.Init += new EventHandler(CalendarToolpart_Init);
            cboList.SelectedIndexChanged += new EventHandler(cboList_SelectedIndexChanged);
        }

        void cboList_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                EnsureChildControls();
                cboField.Items.Clear();
                cboEndField.Items.Clear();
                cboTooltipField.Items.Clear();
                foreach (SPField field in SPContext.Current.Web.Lists[cboList.SelectedValue].Fields) {
                    if (field.Type == SPFieldType.DateTime) {
                        cboField.Items.Add(new ListItem(field.Title, field.InternalName));
                        cboEndField.Items.Add(new ListItem(field.Title, field.InternalName));
                    }
                    cboTooltipField.Items.Add(new ListItem(field.Title, field.Id.ToString()));
                }
            } catch (Exception ex) {
                Page.Response.Write(ex.ToString());
            }
        }

        void CalendarToolpart_Init(object sender, EventArgs e) {
            this.Title = "NC Newssite";
        }

        public override void ApplyChanges() {
            try {
                EnsureChildControls();
                NCMiniCalendarWebPart wp = (NCMiniCalendarWebPart)ParentToolPane.SelectedWebPart;
                wp.ListName = cboList.SelectedValue;
                wp.EventDateField = cboField.SelectedValue;
                wp.EndDateField = cboEndField.SelectedValue;
                wp.TitleField = cboTooltipField.SelectedValue;
            } catch (Exception ex) {
                Page.Response.Write(ex.ToString());
            }
        }

        protected override void CreateChildControls() {
            try {
                base.CreateChildControls();
                NCMiniCalendarWebPart wp = (NCMiniCalendarWebPart)ParentToolPane.SelectedWebPart;
                HtmlGenericControl cnt = new HtmlGenericControl();
                cnt.InnerHtml = "Select the list:<br />";
                Controls.Add(cnt);
                Controls.Add(cboList);
                cnt = new HtmlGenericControl();
                cnt.InnerHtml = "<br />Select the date field:<br />";
                Controls.Add(cnt);
                Controls.Add(cboField);
                cnt = new HtmlGenericControl();
                cnt.InnerHtml = "<br />Select the end date field:<br />";
                Controls.Add(cnt);
                Controls.Add(cboEndField);
                cnt = new HtmlGenericControl();
                cnt.InnerHtml = "<br />Select the tooltip field:<br />";
                Controls.Add(cnt);
                Controls.Add(cboTooltipField);
                cboList.AutoPostBack = true;
                foreach (SPList list in SPContext.Current.Web.Lists) {
                    cboList.Items.Add(list.Title);
                }
                cboList.SelectedValue = wp.ListName;
                foreach (SPField field in SPContext.Current.Web.Lists[cboList.SelectedValue].Fields) {
                    if (field.Type == SPFieldType.DateTime) {
                        cboField.Items.Add(new ListItem(field.Title, field.InternalName));
                        cboEndField.Items.Add(new ListItem(field.Title, field.InternalName));
                    }
                    cboTooltipField.Items.Add(new ListItem(field.Title, field.InternalName));
                }
                cboField.SelectedValue = wp.EventDateField;
                cboEndField.SelectedValue = wp.EndDateField;
                cboTooltipField.SelectedValue = wp.TitleField;
            } catch (Exception ex) {
                HtmlGenericControl cnt = new HtmlGenericControl();
                cnt.InnerText = ex.ToString();
                Controls.Add(cnt);
            }
        }
    }
}