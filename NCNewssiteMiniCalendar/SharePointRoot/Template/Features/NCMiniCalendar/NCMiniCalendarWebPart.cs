using System;
#region Namespaces
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages; 
#endregion

namespace NCNewssiteMiniCalendar.UI.WebControls.WebParts
{
    [Guid("1bcee1e5-d273-4f80-9489-daf60c415d89")]
    public class NCMiniCalendarWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        #region Scripts to register

        private static string _toolTipScript = @"function CloseTip() {
            $('.tip').css('display', 'none');
        }

        $(document).ready(function () {
            $(document.body).append($('.tip')[0]);

            $('td', $('.MiniCalendar'))
            .hover(function () {
                var event = $('span', this).attr('event');
                if (event == '' || event == undefined) {
                }
                else {
                    $(this).css('cursor', 'hand');
                }
            }, function () {
                $(this).css('cursor', 'default');
            })
            .click(function (e) {
                var event = $('span', this).attr('event');
                $('.tip').html(""<span style='float:right; background-color:red; color:white; padding: 0px 2px 0px 2px; cursor:hand;' onclick=CloseTip();>X</span>"" + event);

                if (event == '' || event == undefined) {
                    $('.tip').css('display', 'none');
                }
                else {
                    $('.tip').css('display', 'block').css('top', e.pageY).css('left', e.pageX);
                }
            });
        });";

        #endregion

        #region Private Members
        private bool _error;
        private Calendar _calendar;
        private HtmlGenericControl _tip;
        private string _listName = "Calendar";
        private string _startDateField = "EventDate";
        private string _endDateField = "EndDate";
        private FirstDayOfWeek _firstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Monday;
        private SPListItemCollection events;
        private string _titleField;
        
        #endregion

        #region Properties

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("General")]
        [WebDisplayName("Calendar list name")]
        public string ListName
        {
            get { return _listName; }
            set { _listName = value; }
        }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("General")]
        [WebDisplayName("Event start-date field name")]
        public string EventDateField
        {
            get { return _startDateField; }
            set { _startDateField = value; }
        }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("General")]
        [WebDisplayName("Event end-date field name")]
        public string EndDateField
        {
            get { return _endDateField; }
            set { _endDateField = value; }
        }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("General")]
        [WebDisplayName("First day of week")]
        public FirstDayOfWeek FirstDayOfWeek
        {
            get { return _firstDayOfWeek; }
            set { _firstDayOfWeek = value; }
        }

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [System.ComponentModel.Category("General")]
        [WebDisplayName("Tooltip field name")]
        public string TitleField
        {
            get { return _titleField; }
            set { _titleField = value; }
        }

        #endregion

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public NCMiniCalendarWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        /// <summary>
        /// Method defines the title and description
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Title = "NC Mini Calendar";
            this.Description = "Mini calendar for NC newssite";
        }

        /// <summary>
        /// Load method of the webpart
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
        /// Method used to define the querries and styling of the calendar
        /// </summary>
        protected override void CreateChildControls()
        {
            if (!_error)
            {
                try
                {
                    base.CreateChildControls();

                    Page.ClientScript.RegisterClientScriptInclude("jQuery", "/_layouts/ncnewssite/jquery-1.4.2.min.js");
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "EventToolTip", _toolTipScript, true);

                    _calendar = new Calendar { UseAccessibleHeader = false, CssClass = "MiniCalendar" };
                    _calendar.FirstDayOfWeek = FirstDayOfWeek;
                    _calendar.DayRender += new DayRenderEventHandler(_calendar_DayRender);
                    _calendar.VisibleMonthChanged += new MonthChangedEventHandler(_calendar_VisibleMonthChanged);
                    _calendar.DataBinding += new EventHandler(_calendar_DataBinding);

                    ApplyCalendarStyles();

                    _tip = new HtmlGenericControl("div");
                    _tip.Attributes.Add("class", "tip");

                    this.Controls.Add(_calendar);
                    this.Controls.Add(_tip);

                    _calendar.DataBind();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Method used to fetch the event from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _calendar_DataBinding(object sender, EventArgs e)
        {
            if (!_error)
            {
                try
                {
                    FetchEvents();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Method performs logic when visible month is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _calendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            if (!_error)
            {
                try
                {
                    FetchEvents();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Method called every time when each day is rendered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (!_error)
            {
                try
                {
                    if (string.IsNullOrEmpty(ListName) || string.IsNullOrEmpty(EventDateField) || string.IsNullOrEmpty(EndDateField))
                        return;

                    e.Cell.Controls.Clear();

                    var label = new Label { Text = e.Day.DayNumberText, ForeColor = e.Day.IsOtherMonth ? Color.Gray : Color.Black };

                    e.Cell.Controls.Add(label);

                    var items = from SPListItem evt in events
                                where ((DateTime)evt[EventDateField]).Date <= e.Day.Date && ((DateTime)evt[EndDateField]).Date >= e.Day.Date
                                select evt;

                    if (items.Count<SPListItem>() > 0)
                    {
                        label.Style["font-weight"] = "bold";

                        string listUrl = SPContext.Current.Web.Url + "/" + SPContext.Current.Web.Lists[ListName].Forms[PAGETYPE.PAGE_DISPLAYFORM].Url;
                        label.Attributes["event"] = string.Empty;

                        foreach (var item in items)
                        {
                            if(string.IsNullOrEmpty( TitleField))
                            {
                                label.Attributes["event"] += 
                                    string.Format("<a href='{0}'>{1}</a>", 
                                    listUrl + "?ID=" + item.ID, item.Title);
                            }
                            else
                            {
                                string description = item[TitleField].ToString();

                                if (description != @"<div></div>")
                                    description = ", " + description;

                                if (description.Length > 100 )
                                    description =  description.Substring(0, 100) + "...";

                                label.Attributes["event"] += string.Format("<a href='{0}'>{1}</a>{2}", 
                                    listUrl + "?ID=" + item.ID, item.Title, description);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        /// <summary>
        /// Method executes query on calendar list and fetch all the items
        /// </summary>
        private void FetchEvents()
        {
            if (!string.IsNullOrEmpty(_listName))
            {
                SPQuery query = new SPQuery();
                query.CalendarDate = _calendar.VisibleDate;
                query.ExpandRecurrence = true;
                query.Query = "<Where>" +
                    "<DateRangesOverlap>" +
                        "<FieldRef Name=\"" + EventDateField + "\"></FieldRef>" +
                        "<FieldRef Name=\"" + EndDateField + "\"></FieldRef>" +
                    //"<FieldRef Name=\"RecurrenceID\"></FieldRef>" +
                        "<Value Type=\"DateTime\"><Month/></Value>" +
                    "</DateRangesOverlap></Where>";
                events = SPContext.Current.Web.Lists[ListName].GetItems(query);
            }
        }

        /// <summary>
        /// Styles are applied to the calendar
        /// </summary>
        private void ApplyCalendarStyles()
        {
            if (_calendar != null)
            {
                _calendar.NextPrevFormat = NextPrevFormat.ShortMonth;
                _calendar.DayStyle.CssClass = "ms-DayStyle";
                _calendar.NextPrevStyle.CssClass = "ms-NextPrevStyle";
                _calendar.TodayDayStyle.CssClass = "ms-TodayDayStyle";
                _calendar.NextPrevStyle.CssClass = "ms-NextPrevStyle";
                _calendar.OtherMonthDayStyle.CssClass = "ms-OtherMonthDayStyle";
                _calendar.WeekendDayStyle.CssClass = "ms-WeekendDayStyle";
            }
        }

        /// <summary>
        /// Method retrievs the toolpart for the calendar
        /// </summary>
        /// <returns></returns>
        public override ToolPart[] GetToolParts()
        {
            ToolPart[] toolparts = new ToolPart[3];
            WebPartToolPart wptp = new WebPartToolPart();
            CustomPropertyToolPart custom = new CustomPropertyToolPart();
            toolparts[0] = custom;
            toolparts[1] = wptp;

            toolparts[2] = new NCMiniCalendarToolpart();

            return toolparts;
        }

        /// <summary>
        /// Method to handle the exception while rendering the logic
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            this._error = true;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl(ex.Message));
        }

    }
}
