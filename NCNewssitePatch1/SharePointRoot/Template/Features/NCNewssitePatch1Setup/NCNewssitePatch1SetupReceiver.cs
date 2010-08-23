using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Globalization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace NCNewssitePatch1.EventHandlers.Features
{
    public class NCNewssitePatch1SetupReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;

            //only activate these features on root web
            if (web.IsRootWeb)
            {
                // NCNewssitePatch1PictureLibrary
                if (web.Features[new Guid("ef44e083-37a7-4427-9dce-9951222b4add")] == null)
                {
                    web.Features.Add(new Guid("ef44e083-37a7-4427-9dce-9951222b4add"));
                }
                // NCNewssitePatch1ArticleList
                if (web.Features[new Guid("0bfb6a88-28b4-4b2e-a7e5-fde8df4526e2")] == null)
                {
                    web.Features.Add(new Guid("0bfb6a88-28b4-4b2e-a7e5-fde8df4526e2"));
                }
                // NCNewssitePatch1OrganizationUnitsList
                if (web.Features[new Guid("457ac86d-ef7c-4c33-a90d-856efcd30985")] == null)
                {
                    web.Features.Add(new Guid("457ac86d-ef7c-4c33-a90d-856efcd30985"));
                }
                // NCNewssitePatch1ArticleContentType
                if (web.Site.Features[new Guid("fd8e484a-0e05-4ce7-a3d2-4e581607ed4c")] == null)
                {
                    web.Site.Features.Add(new Guid("fd8e484a-0e05-4ce7-a3d2-4e581607ed4c"));
                }
                // NCNewssitePatch1ApplyArticleCTToArticleList
                if (web.Features[new Guid("10b4ae64-6a8b-409f-bc80-c033e9d76eed")] == null)
                {
                    web.Features.Add(new Guid("10b4ae64-6a8b-409f-bc80-c033e9d76eed"));
                }
                // NCNewssitePatch1AddDefaultArticlesView
                if (web.Features[new Guid("2c407f8e-5ec9-4d39-a9ca-7aea98b65c8e")] == null)
                {
                    web.Features.Add(new Guid("2c407f8e-5ec9-4d39-a9ca-7aea98b65c8e"));
                }

                // NCNewssitePatch1ApplyDefaultMenuSortOrder
                if (web.Features[new Guid("012e96e1-0c98-414b-801b-ee7c942f1f62")] == null)
                {
                    web.Features.Add(new Guid("012e96e1-0c98-414b-801b-ee7c942f1f62"));
                }


            }

            // NCNewssitePatch1ServiceAnnouncementList
            if (web.Features[new Guid("9ace56bf-b6eb-4525-8fea-4baaf0adf771")] == null)
            {
                web.Features.Add(new Guid("9ace56bf-b6eb-4525-8fea-4baaf0adf771"));
            }
            // NCNewssitePatch1ServiceAnnouncementContentType
            if (web.Site.Features[new Guid("4fada487-f21f-4a8b-b4b3-929e1315b587")] == null)
            {
                web.Site.Features.Add(new Guid("4fada487-f21f-4a8b-b4b3-929e1315b587"));
            }
            // NCNewssitePatch1ApplyServiceAnnouncementCTToList
            if (web.Features[new Guid("7686febe-906c-4c00-b2eb-f28b04799d02")] == null)
            {
                web.Features.Add(new Guid("7686febe-906c-4c00-b2eb-f28b04799d02"));
            }

            // Activate SharePoint Server Publishing Infrastructure
            if (web.Site.Features[new Guid("f6924d36-2fa8-4f0b-b16d-06b7250180fa")] == null)
            {
                web.Site.Features.Add(new Guid("f6924d36-2fa8-4f0b-b16d-06b7250180fa"));
            }
            // Activate SharePoint Server Publishing
            if (web.Features[new Guid("94c94ca6-b32f-4da9-a9e3-1f3d343d7ecb")] == null)
            {
                web.Features.Add(new Guid("94c94ca6-b32f-4da9-a9e3-1f3d343d7ecb"));
            }

            // NCNewssitePatch1ModifyArticlePageCT
            if (web.Features[new Guid("987dbd53-3584-419a-a29a-7a854187423a")] == null)
            {
                web.Features.Add(new Guid("987dbd53-3584-419a-a29a-7a854187423a"));
            }

            // NCNewssitePatch1AddRootPageToPageList
            if (web.Features[new Guid("8e573401-2b06-4414-84b1-152aefbd1e4d")] == null)
            {
                web.Features.Add(new Guid("8e573401-2b06-4414-84b1-152aefbd1e4d"));
            }

            // NCNewssitePatch1SetAvailablePageTemplates
            if (web.Features[new Guid("38f9b048-398c-4299-ae9a-648765b4fb51")] == null)
            {
                web.Features.Add(new Guid("38f9b048-398c-4299-ae9a-648765b4fb51"));
            }

            // NCNewssitePatch1AddDefaultPagesView
            if (web.Features[new Guid("2e5a1454-53b1-48f2-84d5-919c0e708e31")] == null)
            {
                web.Features.Add(new Guid("2e5a1454-53b1-48f2-84d5-919c0e708e31"));
            }

            // NCNewssitePatch1PagesEventHandler
            if (web.Features[new Guid("ef64d6e6-c8bf-4a42-bf9f-6adc3f5727cb")] == null)
            {
                web.Features.Add(new Guid("ef64d6e6-c8bf-4a42-bf9f-6adc3f5727cb"));
            }

            // NCNewssiteLeftNavigation Web part
            if (web.Features[new Guid("a7d6e985-c7ad-40b0-a0f9-8b01832089de")] == null)
            {
                web.Features.Add(new Guid("a7d6e985-c7ad-40b0-a0f9-8b01832089de"));
            }

            //NCNewssiteCustomContentQuery Web part
            if (web.Features[new Guid("fa2838ed-a58b-4c1c-9af1-1e61a594b281")] == null)
            {
                web.Features.Add(new Guid("fa2838ed-a58b-4c1c-9af1-1e61a594b281"));
            }

            // NCNewssiteLatestNewsWebPart
            if (web.Features[new Guid("7fcd2525-7704-465e-a97e-98f8faa38864")] == null)
            {
                web.Features.Add(new Guid("7fcd2525-7704-465e-a97e-98f8faa38864"));
            }

            //NCNewssiteLinksList
            if (web.Features[new Guid("d7bbfe91-3aa7-4450-9a43-499111d49d7f")] == null)
            {
                web.Features.Add(new Guid("d7bbfe91-3aa7-4450-9a43-499111d49d7f"));
            }

            //Add built-in content type 'Link'
            if (web.Features[new Guid("5ef56c08-a526-4a61-ac28-9026a20e6538")] == null)
            {
                web.Features.Add(new Guid("5ef56c08-a526-4a61-ac28-9026a20e6538"));
            }

            //Add default view for Link
            if (web.Features[new Guid("dcf7bf0b-d0b2-49e5-9685-1627c924531a")] == null)
            {
                web.Features.Add(new Guid("dcf7bf0b-d0b2-49e5-9685-1627c924531a"));
            }

            //NCNewssitePatch1BlogList
            if (web.Features[new Guid("959376c3-595d-4fb6-8626-6d7f191314d7")] == null)
            {
                web.Features.Add(new Guid("959376c3-595d-4fb6-8626-6d7f191314d7"));
            }

            //NCNewssitePatch1BlogContentType
            if (web.Site.Features[new Guid("cced7d95-2c3e-4008-a213-047aa4ada75b")] == null)
            {
                web.Site.Features.Add(new Guid("cced7d95-2c3e-4008-a213-047aa4ada75b"));
            }

            //NCNewssitePatch1ApplyBlogCTToBlogList
            if (web.Features[new Guid("ef12c49a-7960-49c6-b753-a4280db8ab7c")] == null)
            {
                web.Features.Add(new Guid("ef12c49a-7960-49c6-b753-a4280db8ab7c"));
            }

            //NCNewssitePatch1AddDefaultBlogView
            if (web.Features[new Guid("7b08d7e9-4233-4774-99c4-e455e124fb78")] == null)
            {
                web.Features.Add(new Guid("7b08d7e9-4233-4774-99c4-e455e124fb78"));
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
            // if (properties.Feature.Parent is SPWeb)
            // {
            // SPWeb web = (SPWeb)properties.Feature.Parent;
            // SPSite site = web.Site;
            // 
            // }

        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
            base.FeatureInstalled(properties);
        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            base.FeatureUninstalling(properties);
        }

        public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, IDictionary<string, string> parameters)
        {
            base.FeatureUpgrading(properties, upgradeActionName, parameters);

        }
    }
}
