using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint;

namespace NCNewssiteCommon.Utilities
{
    public class NCTopMenuHelper
    {
        /// <summary>
        /// Used to get current web. Because of anonymous access we cannot use SPContext.Current.Web directly
        /// </summary>
        /// <returns>SPWeb object</returns>
        public static SPWeb GetCurrentWeb()
        {
            SPWeb currentWeb = null;

            if (SPContext.Current.Web.CurrentUser != null)
            {
                currentWeb = SPContext.Current.Web;
            }
            else
            {
                SPWeb webSite = SPContext.Current.Site.RootWeb;

                string currentUrl = GetCurrentWebRelativeUrl();

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPSite site = new SPSite(webSite.Site.Url);

                    if (currentUrl == null || currentUrl == "")
                        currentWeb = webSite;
                    else
                    {
                        if (IsLayoutPage())
                            currentWeb = site.OpenWeb(currentUrl);
                        else
                            currentWeb = site.OpenWeb(currentUrl, false);

                        if (currentWeb.Exists == false || currentWeb == null)
                            currentWeb = webSite;
                    }
                });
            }
            return currentWeb;
        }

        /// <summary>
        /// Used to get current root web. Because of anonymous access we cannot use SPContext.Current directly
        /// </summary>
        /// <returns>SPWeb object</returns>
        public static SPWeb GetCurrentRootWeb()
        {
            SPWeb currentRootWeb = null;

            if (SPContext.Current.Web.CurrentUser != null)
            {
                currentRootWeb = SPContext.Current.Site.RootWeb;
            }
            else
            {
                SPWeb webSite = SPContext.Current.Site.RootWeb;
                string relativeUrl = webSite.ServerRelativeUrl.ToString();

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPSite currentsite = new SPSite(webSite.Site.Url);
                    currentRootWeb = currentsite.OpenWeb(relativeUrl);
                });
            }
            return currentRootWeb;
        }

        /// <summary>
        /// Helper method to get the current webs relative url. Needed when anonymous access is used
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentWebRelativeUrl()
        {
            string newUrl = null;

            try
            {
                if (SPContext.Current.Web.IsRootWeb)
                    return null;

                string fullUrl = HttpContext.Current.Request.Url.ToString();
                string rootWebUrl = SPContext.Current.Site.Url;

                newUrl = fullUrl.Replace(rootWebUrl + "/", "");

                if (newUrl.IndexOf("/") < 0)
                {
                    return null;
                }

                if (newUrl.IndexOf("/_") >= 0) //this is a layout page, needs to be handled different
                    newUrl = newUrl.Remove(newUrl.IndexOf("/_"));
                else
                    newUrl = newUrl.Remove(newUrl.LastIndexOf("/"));
            }
            catch
            {
                return null;
            }

            return newUrl;
        }

        /// <summary>
        /// Helper method to determine if this a layout page.
        /// </summary>
        /// <returns></returns>
        public static bool IsLayoutPage()
        {
            string fullUrl = HttpContext.Current.Request.Url.ToString();
            if (fullUrl.IndexOf("/_") < 0)
                return false;

            return true;
        }
    }
}