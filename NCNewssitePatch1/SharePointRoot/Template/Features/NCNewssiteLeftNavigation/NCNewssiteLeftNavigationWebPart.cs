using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using System.Text;
using System.Web.Caching;

namespace NCNewssitePatch1.UI.WebControls.WebParts
{
    [Guid("1f872358-a469-4500-9407-a04346272f8e")]
    public class NCNewssiteLeftNavigationWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        private const string NAVPLACEHOLDER = "[$%LeftNavPlaceHolder%$]";
        private const string EXPANDPLACEHOLDER = "[$%LeftNavCSClassPlaceholder%$]";
        private const string PAGES = "Pages";
        private const string SHOWONLEFTMENU = "ShowOnLeftMenu";
        private bool _error = false;
        private string _html = string.Empty;
        private int _level = 0;
        private bool _IsExpanded = false;
        
        SPWeb web;
                 
        public NCNewssiteLeftNavigationWebPart()
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
                    RenderMenu();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
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

        private void RenderMenu()
        {
            _level = 3;
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                web = site.OpenWeb(SPContext.Current.Web.ServerRelativeUrl);
                _html = string.Empty;
                _html= CachedLeftMenu(web);
                _html = "<div class=\"navigation\">" + _html + "</div>"; ;
                this.Controls.Add(new Label() { Text = _html });
            }
        }

        private string CachedLeftMenu(SPWeb web)
        {
            //GetWebs(web);
            //return _html;
            string cacheValue = string.Empty;
            string cacheKey = "NCNewssiteLeftNavigationMenu" + web.ID.ToString();
            string cacheTimeKey = "NCNewssiteLeftNavigationMenuCacheTime" + web.ID.ToString();
            cacheValue = (string)HttpContext.Current.Cache[cacheKey];
            object lastCached = HttpContext.Current.Cache[cacheTimeKey];
            if (cacheValue == null)
            {
                GetWebs(web);

                HttpContext.Current.Cache.Add(cacheKey, _html, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                //add lists current LastModifiedDate in seperate cache to monitor changes on list
                HttpContext.Current.Cache.Add(cacheTimeKey, web.LastItemModifiedDate, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            else if (lastCached != null && (DateTime)lastCached < web.LastItemModifiedDate)
            {
                GetWebs(web);

                //remove and add menu to cache
                HttpContext.Current.Cache.Remove(cacheKey);
                HttpContext.Current.Cache.Add(cacheKey, _html, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                //remove and add list LastModifiedDate to cache
                HttpContext.Current.Cache.Remove(cacheTimeKey);
                HttpContext.Current.Cache.Add(cacheTimeKey, web.LastItemModifiedDate, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            else
                _html = cacheValue;
            return _html;
        }

        private void HandleCache(SPList list, SPListItem root, SPWeb currentWeb)
        {
           // GetChildPages(list, root, _level, currentWeb.Url);
            string cacheKey = "NCNewssiteLeftNavigation" + currentWeb.ID.ToString();
            string cacheValue = string.Empty;
            if (SPContext.Current.File != null)
            {
                cacheKey += SPContext.Current.File.UniqueId;
            }
            string cacheTimeKey = "NCNewssiteLeftNavigationCacheTime" + currentWeb.ID.ToString();

            cacheValue = (string)HttpContext.Current.Cache[cacheKey];
            object lastCached = HttpContext.Current.Cache[cacheTimeKey];

            if (cacheValue == null)
            {
                GetChildPages(list, root, _level, currentWeb.Url);

                HttpContext.Current.Cache.Add(cacheKey, _html, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                //add lists current LastModifiedDate in seperate cache to monitor changes on list
                HttpContext.Current.Cache.Add(cacheTimeKey, list.LastItemModifiedDate, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                _html += cacheValue;
            }
            else if (lastCached != null && (DateTime)lastCached < list.LastItemModifiedDate)
            {
                _html += "<div class=\"navigation\">" + GetChildPages(list, root, _level, currentWeb.Url);

                //remove and add menu to cache
                HttpContext.Current.Cache.Remove(cacheKey);
                HttpContext.Current.Cache.Add(cacheKey, _html, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                //remove and add list LastModifiedDate to cache
                HttpContext.Current.Cache.Remove(cacheTimeKey);
                HttpContext.Current.Cache.Add(cacheTimeKey, list.LastItemModifiedDate, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            else
                _html = cacheValue;
        }

        private string GetChildPages(SPList pagesList, SPListItem parentItem, int level,string url)
        {
            _html += @"<ul class=""navi-level-" + level + @""">";
            SPListItemCollection items = GetPageItemCollection(parentItem, pagesList);
            foreach (SPListItem page in items)
            {
                if (PublishingPage.IsPublishingPage(page))
                {
                    PublishingPage pubPage = PublishingPage.GetPublishingPage(page);

                    if (pubPage.ListItem.File.Level == SPFileLevel.Published)
                    {
                        bool isSelected = (SPContext.Current.File != null && SPContext.Current.File.UniqueId == page.UniqueId);

                        if (GetCountChildPages(pagesList, page) > 0)
                        {
                            string arrowClass = " class='collapsed'";
                            if (isSelected || HasActiveSubPage(pagesList, page))
                            {
                                arrowClass = " class='expanded active'";
                            }

                            string temp = page.DisplayName;
                            temp = temp.Replace("/", "/&shy;");

                            _html += "<li" + arrowClass + "><div class='submenuToggle' href='#'>&nbsp;</div><a style='white-space:normal' href='" + GetSPPageUrl(page, url) + "'>" + WrapText(page.DisplayName) +"</a>";
                            GetChildPages(pagesList, page, level + 1, url);
                            _html += "</li>";
                        }
                        else
                        {
                            string selectedClass = "";
                            if (isSelected)
                            {
                                selectedClass = " class='active'";
                            }
                            _html += "<li" + selectedClass + "><a style='white-space:normal' href='" + GetSPPageUrl(page, url) + "'>" + WrapText(page.DisplayName) + "</a></li>";
                        }
                    }
                }
            }
            _html += "</ul>";

            return _html;
        }

        private string WrapText(string inputString)
        {
            //char[] charactorToWrap = new[] { '/', '-', " " };
            inputString = "<span>" + inputString;
            //inputString = inputString.Replace("/", "/&nbsp;</span><span>");
            inputString = inputString.Replace("-", "-</span><span>");
            inputString = inputString.Replace(" ", " </span><span>");

            return inputString = inputString + "</span>";
        }

        private SPListItemCollection GetPageItemCollection(SPListItem parentItem, SPList pagesList)
        {
            if (pagesList.Fields.ContainsField(SHOWONLEFTMENU))
            {
                SPQuery query = new SPQuery();
                query.Query = @"
                        <Where><And>
                            <Eq>
                                <FieldRef Name=""Parent_x0020_page"" />
                                <Value Type=""Lookup"">" + parentItem.Title + @"</Value>
                            </Eq>
                            <Eq>
                                <FieldRef Name=""ShowOnLeftMenu"" />
                                <Value Type=""Boolean"">1</Value>
                            </Eq>
                        </And></Where>
                        <OrderBy>
                            <FieldRef Name=""Sort_x0020_order"" />
                        </OrderBy>";

                return pagesList.GetItems(query);
            }
            else
                return null;
        }

        private string GetWebs(SPWeb currentWeb)
        {
            string pagesHtml = string.Empty;
            string webTitle = currentWeb.Title;

            if (currentWeb.Properties[SHOWONLEFTMENU] == "1")
            {
                _html += @"<ul class=""navi-level-" + _level + @""">";
                _html += "<li "+EXPANDPLACEHOLDER+ ">"+NAVPLACEHOLDER+"<a style='white-space:normal' href='" + currentWeb.Url + "'>" + WrapText(currentWeb.Title) + "</a>";
                GetChieldWebs(currentWeb);
                _html += "</li></ul>";
            }
            else
            {
                GetChieldWebs(currentWeb);
            }
            return _html;
        }

        private void GetChieldWebs(SPWeb currentWeb)
        {
            string arrowClass = " class='collapsed'";
            bool hasSubSites = HasSubSites(currentWeb);
            bool showArrow = false;
            bool hasActivePages=false;

            if (hasSubSites)
            {
                foreach (SPWeb chieldWeb in currentWeb.Webs)
                {
                    if (chieldWeb.Properties[SHOWONLEFTMENU] == "1")
                    {
                        showArrow = true;
                        if (_html.Contains(NAVPLACEHOLDER))
                            _html=_html.Replace(NAVPLACEHOLDER, "<div class='submenuToggle' href='#'>&nbsp;</div>");
                        _level++;
                        GetWebs(chieldWeb);
                        _level--;
                    }
                }
            }
            if (IsListExists(currentWeb))
            {
                SPList pagesList = currentWeb.Lists[PAGES];
                SPListItem rootItem = pagesList.GetItemById(1);
                hasActivePages = IsPagesExists(pagesList, rootItem);
                if (hasActivePages)
                {
                    _level++;
                    HandleCache(pagesList, rootItem, currentWeb);
                    _level--;
                }
            }
            if (ShouldExpand(currentWeb))
            {
                arrowClass = " class='expanded active'";
            }
            if (_html.Contains("[$%LeftNavCSClassPlaceholder%$]"))
            {
                _html = _html.Replace("[$%LeftNavCSClassPlaceholder%$]", arrowClass);
            }
            
            if(_html.Contains(NAVPLACEHOLDER))
            {
                if (showArrow || hasActivePages)
                {
                    _html = _html.Replace(NAVPLACEHOLDER, "<div class='submenuToggle' href='#'>&nbsp;</div>");
                }
                else
                    _html = _html.Replace(NAVPLACEHOLDER, "");
            }
        }

        private bool ShouldExpand(SPWeb currentWeb)
        {
            if (_IsExpanded) return true;
            string currentUrl = Convert.ToString(HttpContext.Current.Request.Url);
            if (currentUrl.Contains(currentWeb.Url))
            {
                _IsExpanded = true;
                return true;
            }
            else
                return false;
        }

        private bool IsListExists(SPWeb currentWeb)
        {
            try
            {
                SPList list = currentWeb.Lists[PAGES];
                return list != null ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool HasSubSites(SPWeb currentWeb)
        {
            return currentWeb.Webs.Count > 0 ? true : false;
        }

        private string GetSPPageUrl(SPListItem sppage,string webUrl)
        {
            return webUrl + "/" + sppage.Url.ToString();
        }

        private bool HasActiveSubPage(SPList pagesList, SPListItem parentItem)
        {
            SPListItemCollection items= GetPageItemCollection(parentItem, pagesList);
            foreach (SPListItem page in items)
            {
                if (PublishingPage.IsPublishingPage(page))
                {
                    PublishingPage pubPage = PublishingPage.GetPublishingPage(page);

                    if (pubPage.ListItem.File.Level == SPFileLevel.Published)
                    {
                        if (SPContext.Current.File != null && SPContext.Current.File.UniqueId == page.UniqueId)
                        {
                            return true;
                        }
                        else if (HasActiveSubPage(pagesList, page))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private int GetCountChildPages(SPList pagesList, SPListItem parentItem)
        {
            try
            {
                if (pagesList.Fields.ContainsField(SHOWONLEFTMENU))
                {
                    SPQuery query = new SPQuery();
                    query.Query = @"
                        <Where>
                            <And>
                            <Eq>
                                <FieldRef Name=""Parent_x0020_page"" />
                                <Value Type=""Lookup"">" + parentItem.Title + @"</Value>
                            </Eq>
                            <Eq>
                                <FieldRef Name=""ShowOnLeftMenu"" />
                                <Value Type=""Boolean"">1</Value>
                            </Eq>
                        </And>
                        </Where>";

                    SPListItemCollection items = pagesList.GetItems(query);
                    if (items != null)
                        return items.Count;
                    else
                        return 0;
                }
                else
                    return 0;
            }
            catch (Exception)
            {

                return 0;
            }
            
        }

        private bool IsPagesExists(SPList pagesList, SPListItem parentItem)
        {
            return GetCountChildPages(pagesList, parentItem) <= 0 ? false : true;
        }
    }
}
