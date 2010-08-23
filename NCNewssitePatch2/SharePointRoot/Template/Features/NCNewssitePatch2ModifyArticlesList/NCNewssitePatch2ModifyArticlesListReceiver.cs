using System;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace NCNewssitePatch2.EventHandlers.Features
{
    public class NCNewssitePatch2ModifyArticlesListReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            base.FeatureActivated(properties);
            if (properties.Feature.Parent is SPWeb)
            {
                SPWeb web = (SPWeb)properties.Feature.Parent;
                SPList articles = web.Lists.TryGetList("Articles");

                if (articles != null)
                {
                    ModifyField(articles, "ArticleAuthor", "Author", false, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);

                    AddAuthorsFromAD(web, articles);
                    AddAuthorsFromCustomList(web, articles);

                    ModifyField(articles, "PublishingStart", "Publishing start", true, "[today]",
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "PublishingEnd", "Publishing end", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);

                    ModifyField(articles, "DanishUrl", "Danish url", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "NorwegianUrl", "Norwegian url", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "EnglishUrl", "English url", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);

                    ModifyField(articles, "Organization_x0020_Unit", "Publish at", null, null,  
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);

                    ModifyField(articles, "FrontpageHeader1", "Top news ingress/intro", null, null,
                        SPFieldType.Invalid, false, SPRichTextMode.Compatible);
                    ModifyField(articles, "FrontpageTopnewsImage", "Top news image", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "FrontPageHeader2", "Profiled news ingress/intro", null, null,
                        SPFieldType.Invalid, false, SPRichTextMode.Compatible);
                    ModifyField(articles, "FrontpageProfilenewsImage", "Profiled news image", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "ArticleHeader", "Ingress/intro", null, null,
                        SPFieldType.Invalid, false, SPRichTextMode.Compatible);
                    ModifyField(articles, "ArticleTopImage", "Image", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "ArticleTopImageText", "Image text", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);

                    ModifyField(articles, "ArticleBodyText", "Body text", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "RightColumnLinks", "Related links", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);
                    ModifyField(articles, "RightColumnFacts", "Related information", null, null,
                        SPFieldType.Invalid, null, SPRichTextMode.Compatible);

                    articles.Update();
                    web.Update();
                }
            }
        }

        private void AddAuthorsFromAD(SPWeb web, SPList articles)
        {
            string authorsListName = "Author From AD";
            if (articles.Fields.ContainsField(authorsListName))
                return;

            articles.Fields.Add(authorsListName, SPFieldType.User, false);
        }

        private void AddAuthorsFromCustomList(SPWeb web, SPList articles)
        {
            SPList authorsList = web.Lists.TryGetList("Authors");

            if (authorsList == null)
            {
                // add authors list
                web.Lists.Add("Authors", string.Empty, SPListTemplateType.GenericList);
                authorsList = web.Lists["Authors"];

                SPField titleField =  authorsList.Fields["Title"];
                titleField.Title = "Name";
                titleField.Update();

                authorsList.Update();
            }

            // modify articles list
            string authorFieldName = "Author From List";

            if (articles.Fields.ContainsField(authorFieldName))
                return;

            articles.Fields.AddLookup(authorFieldName, authorsList.ID, false);
            SPFieldLookup authorField = (SPFieldLookup)articles.Fields[authorFieldName];
            //authorField.LookupField = "Name";
            authorField.Group = "NCNewssite";
            authorField.Update(true);
        }

        private void ModifyField(SPList articles, string internalName, string newDisplayName, bool? isRequired, string defaultValue, SPFieldType fieldType, bool? richText, SPRichTextMode richTextMode)
        {
            if (articles.Fields.ContainsField(internalName))
            {
                SPField field = articles.Fields.GetFieldByInternalName(internalName);

                if (!string.IsNullOrEmpty(newDisplayName))
                    field.Title = newDisplayName;

                if (isRequired.HasValue)
                    field.Required = isRequired.Value;

                if (!string.IsNullOrEmpty(defaultValue))
                    field.DefaultValue = defaultValue;

                if (fieldType !=  SPFieldType.Invalid)
                    field.Type = fieldType;

                if (richText.HasValue)
                {
                    (field as SPFieldMultiLineText).RichText = richText.Value;
                    (field as SPFieldMultiLineText).RichTextMode = richTextMode;
                }

                field.Update();
            }
        }

        #region Others

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            base.FeatureDeactivating(properties);
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

        #endregion
    }
}
