using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Reflection;
using System.Web;
using Microsoft.SharePoint.Utilities;
using System.Web.Caching;
using System.Collections;
using System.Globalization;
using Microsoft.SharePoint.Publishing;

namespace NCNewssiteFlushCache.UI.WebControls.WebParts
{
    [Guid("025477c6-931a-4626-83e8-39d80c5c3b8a")]
    public class NCNewssiteFlushCacheWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        private bool _error = false;

        private bool IsInEditMode
        {
            get
            {
                var wpm = WebPartManager.GetCurrentWebPartManager(Page);
                return wpm.DisplayMode.Name.Equals("design", StringComparison.InvariantCultureIgnoreCase);
                //return SPContext.Current.FormContext.FormMode == SPControlMode.Edit;
            }
        }

        public NCNewssiteFlushCacheWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Title = "NC Newssite Flush Cache";
            this.Description = "Web Part to flush the cache for whole site";
        }

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

        protected override void CreateChildControls()
        {
            if (!_error)
            {
                try
                {
                    if (IsInEditMode)
                    {
                        base.CreateChildControls();

                        // Your code here...
                        var _flushButton = new Button
                                               {
                                                   Text = "Flush Cache",
                                                   //Visible = this.IsInEditMode
                                               };

                        _flushButton.Click += new EventHandler(_flushButton_Click);
                        this.Controls.Add(_flushButton);
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        public void _flushButton_Click(object sender, EventArgs e)
        {
            try
            {
                SPSite site = SPContext.Current.Site;

                SiteCacheSettingsWriter writer = new SiteCacheSettingsWriter(site);
                writer.SetFarmCacheFlushFlag();
                writer.Update();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            this._error = true;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl(ex.Message));
        }
    }
}
