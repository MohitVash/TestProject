using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;

namespace NCNewssitePatch1.NCNewssitePatch1PagesEventHandler
{
    public class PagesEventHandler : SPItemEventReceiver
    {
        public override void ItemAdded(SPItemEventProperties properties)
        {
            if (PublishingPage.IsPublishingPage(properties.ListItem))
            {
                PublishingPage newPage;
                newPage = PublishingPage.GetPublishingPage(properties.ListItem);
                newPage.IncludeInCurrentNavigation = false;
                newPage.IncludeInGlobalNavigation = false;
            }
        }
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            //Should not be able to delete root page
            SPListItem item = properties.ListItem;
            if (item.ID == 1)
            {
                string message = ("You're not allowed to delete the root page in this list");
                properties.ErrorMessage = message;
                properties.Cancel = true;
            }
        }
    }
}