using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.WebPartPages;
using NCNewssitePatch1.UI.WebControls.WebParts;
using Microsoft.SharePoint.Publishing;



namespace NCNewssiteCommon.Utilities
{
    public class NCWebparts
    {
    

        public static void AddContentEditorWebPart()
        {

        }

        public static void AddLinksWP(SPWeb web, string pageUrl, string zoneId, int zoneIndex, string title, PartChromeState chromState, PartChromeType chromeType)
        {
            
        }

        public static void AddCustomContentQueryWebPart(SPWeb web, string pageUrl, string zoneId, int zoneIndex, string title, PartChromeState chromState, PartChromeType chromeType)
        {
            NCNewssitePatch1CustomContentQueryWebPart customContent = new NCNewssitePatch1CustomContentQueryWebPart();
            customContent.ZoneID = zoneId;
            customContent.Title = title;
            customContent.ChromeState = chromState;
            customContent.ChromeType = chromeType;
            AddWebPart(web, customContent, pageUrl, zoneIndex);
            
        }

        public static void AddCEWP(SPWeb web, string pageUrl, string zoneId,int zoneIndex, string title, PartChromeState chromState, PartChromeType chromeType)
        {
            
            ContentEditorWebPart contentEditor = new ContentEditorWebPart();                                    
            contentEditor.ZoneID = zoneId;                                  
            contentEditor.Title = title;
            contentEditor.ChromeState = chromState;
            contentEditor.ChromeType = chromeType;
            AddWebPart(web, contentEditor, pageUrl, zoneIndex);                              
                                    
        }


        public static void AddWebPart(SPWeb web, Microsoft.SharePoint.WebPartPages.WebPart webPart, string pageUrl, int zoneIndex)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                web.AllowUnsafeUpdates = true;
                SPFile file = web.GetFile(pageUrl);
                if (file != null)
                {
                    using (SPLimitedWebPartManager mgr = file.GetLimitedWebPartManager(PersonalizationScope.Shared))
                    {
                        if (mgr != null)
                        {

                            mgr.AddWebPart(webPart, webPart.ZoneID, zoneIndex);

                            web.Update();
                        }
                    }
                }
            });


        }


    }
}