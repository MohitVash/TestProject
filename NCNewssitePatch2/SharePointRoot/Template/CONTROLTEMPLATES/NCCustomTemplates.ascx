<%@ Control Language="C#" AutoEventWireup="false" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="ApplicationPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.ApplicationPages.WebControls" %>
<%@ Register TagPrefix="SPHttpUtility" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.Utilities" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" Src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" Src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<SharePoint:RenderingTemplate ID="NCArticleListForm" runat="server">
    <Template>
        <SharePoint:InformationBar ID="InformationBar1" runat="server" />
        <SharePoint:FormToolBar ID="FormToolBar1" runat="server" />
        <SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage1" runat="server" />
        <div class="span-1">
            &nbsp;
        </div>
        <div class="span-3">
            <h2 class="articlePageHeader">
                <SharePoint:FormField runat="server" ID="TitleField" ControlMode="Display" FieldName="Title" />
            </h2>
            <div class="articleImage">
                <SharePoint:FormField runat="server" ID="ArticleTopImageField" ControlMode="Display"
                    FieldName="ArticleTopImage" />
                <br />
                <SharePoint:FormField runat="server" ID="ArticleTopImageTextField" ControlMode="Display"
                    FieldName="ArticleTopImageText" />
            </div>
            <div class="leadText">
                <SharePoint:FormField runat="server" ID="ArticleHeaderField" ControlMode="Display"
                    FieldName="ArticleHeader" />
            </div>
            <div class="bodyText">
                <SharePoint:FormField runat="server" ID="ArticleBodyTextField" ControlMode="Display"
                    FieldName="ArticleBodyText" />
            </div>
            <br />
            <div>
                By:
                <%=Author(SPContext.Current.ListItem)%>
                <br />
                <%="Published: " + ((DateTime)SPContext.Current.ListItem["PublishingStart"]).ToString("MM.dd.yyyy hh:mm")%>
                &nbsp;
                <%=(DateTime)SPContext.Current.ListItem["Modified"] != (DateTime)SPContext.Current.ListItem["Created"] ? "Updated: " + ((DateTime)SPContext.Current.ListItem["Modified"]).ToString("MM.dd.yyyy hh:mm") : ""%>
            </div>
        </div>
        <div class="articleRightColumn span-1 last">
            <div id="nc-articledisplayform-links">
                <div class="header last">
                    Related links
                </div>
                <div class="content last">
                    <SharePoint:FormField runat="server" ID="RightColumnLinksField" ControlMode="Display"
                        FieldName="RightColumnLinks" />
                </div>
            </div>
            <div id="nc-articledisplayform-news">
                <div class="header last">
                    Related information
                </div>
                <div class="content last">
                    <SharePoint:FormField runat="server" ID="RightColumnFactsField" ControlMode="Display"
                        FieldName="RightColumnFacts" />
                </div>
            </div>
        </div>
        <script runat="server">
            private string Author(SPListItem listItem)
            {
                if (listItem["Author_x0020_From_x0020_AD"] != null)
                {
                    SPFieldUser adUserField = (SPFieldUser)listItem.Fields.GetFieldByInternalName("Author_x0020_From_x0020_AD");
                    SPFieldUserValue adUser = (SPFieldUserValue)adUserField.GetFieldValue(listItem["Author_x0020_From_x0020_AD"].ToString());
                    return adUser.User.Name;
                }
                if (listItem["Author_x0020_From_x0020_List"] != null)
                {
                    SPFieldLookup listUserField = (SPFieldLookup)listItem.Fields.GetFieldByInternalName("Author_x0020_From_x0020_List");
                    SPFieldLookupValue listUser = (SPFieldLookupValue)listUserField.GetFieldValue(listItem["Author_x0020_From_x0020_List"].ToString());
                    return listUser.LookupValue;
                }
                return listItem["ArticleAuthor"].ToString();
            }
        </script>
        <script type="text/javascript">

            var linksContent = '<%=Convert.ToString(SPContext.Current.ListItem["RightColumnLinks"]).Replace(Environment.NewLine, string.Empty).Trim()%>';
            var newsContent = '<%=Convert.ToString(SPContext.Current.ListItem["RightColumnFacts"]).Replace(Environment.NewLine, string.Empty).Trim()%>';

            function HtmlToText(html) {
                return html.replace(/<.*?>/g, "").replace(/&nbsp;/g, "").replace(/[^a-zA-Z 0-9]+/g, "").trim();
            }

            $(document).ready(function () {
                if (HtmlToText(linksContent) == "")
                    $("#nc-articledisplayform-links").css("display", "none");

                if (HtmlToText(newsContent) == "")
                    $("#nc-articledisplayform-news").css("display", "none");
            });
            
        </script>
    </Template>
</SharePoint:RenderingTemplate>
<SharePoint:RenderingTemplate ID="NCArticleListEditForm" runat="server">
    <Template>
        <span id="nc-articleeditform">
            <SharePoint:InformationBar ID="InformationBar1" runat="server" />
            <div id="listFormToolBarTop_renamed">
                <!-- renamed to make in visible always -->
                <wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbltop" RightButtonSeparator="&amp;#160;"
                    runat="server">
                    <template_rightbuttons>
						<%--<SharePoint:NextPageButton runat="server"/>--%>
						<SharePoint:SaveButton runat="server"/>
						<SharePoint:GoBackButton runat="server"/>
					</template_rightbuttons>
                </wssuc:ToolBar>
            </div>
            <SharePoint:FormToolBar ID="FormToolBar1" runat="server" />
            <SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage1" runat="server" />
            <table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
                width="100%">
                <SharePoint:ChangeContentType ID="ChangeContentType1" runat="server" />
                <SharePoint:FolderFormFields ID="FolderFormFields1" runat="server" />
                <tr>
                    <SharePoint:CompositeField ID="TitleField" runat="server" FieldName="Title" />
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <span class="nc-articleedit-sublabel">(from AD)</span>
                            <SharePoint:FieldLabel ID="AuthorFieldLabel" runat="server" FieldName="ArticleAuthor" />
                            <span title="This is a required field." class="ms-formvalidation"> *</span>
                            &#160;
                        </h3>
                    </td>
                    <td valign="top" class="ms-formbody" id="nc-articleeditform-authorad">
                        <SharePoint:FormField ID="AuthorADField" runat="server" FieldName="Author_x0020_From_x0020_AD" />
                        <SharePoint:FieldDescription ID="AuthorADFieldDescription" runat="server" FieldName="ArticleAuthor" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <span class="nc-articleedit-sublabel">(or from internal list)</span>
                            &#160;
                        </h3>
                    </td>
                    <td id="nc-articleeditform-authorlist" valign="top" class="ms-formbody">
                        <SharePoint:FormField ID="AuthorListField" runat="server" FieldName="Author_x0020_From_x0020_List" />
                        <SharePoint:FieldDescription ID="AuthorListFieldDescription" runat="server" FieldName="Author_x0020_From_x0020_List" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <span class="nc-articleedit-sublabel">(or plain text)</span>
                            &#160;
                        </h3>
                    </td>
                    <td valign="top" class="ms-formbody" id="nc-articleeditform-authortext">
                        <SharePoint:FormField ID="AuthorField" runat="server" FieldName="ArticleAuthor" />                        
                        <SharePoint:FieldDescription ID="AuthorFieldDescription" runat="server" FieldName="ArticleAuthor" />
                        <span id="nc-articleedit-required" style="display:none;" class="ms-formvalidation">
                            <span role="alert">You must specify a value for this required field.</span>
                        </span>
                    </td>
                </tr>
                <tr>
                    <SharePoint:CompositeField ID="PublishingStartField" runat="server" FieldName="PublishingStart" />
                </tr>
                <tr>
                    <SharePoint:CompositeField ID="PublishingEndField" runat="server" FieldName="PublishingEnd" />
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="DanishUrlFieldLabel" runat="server" FieldName="DanishUrl" />
                            &#160;
                        </h3>
                    </td>
                   <td valign="top" class="ms-formbody" id="nc-articleeditform-danishurl">
                        <SharePoint:FormField ID="DanishUrlFormField" runat="server" FieldName="DanishUrl" />
                        <SharePoint:FieldDescription ID="DanishUrlFieldDescription" runat="server" FieldName="DanishUrl" />
                        <input type="button" value="Search" id="btnDanishUrl" class="nc-articleedit-browse" ></input>
                    </td>
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="NorwegianUrlFieldLabel" runat="server" FieldName="NorwegianUrl" />
                            &#160;
                        </h3>
                    </td>
                     <td valign="top" class="ms-formbody" id="nc-articleeditform-norwegianurl">
                        <SharePoint:FormField ID="NorwegianUrlFormField" runat="server" FieldName="NorwegianUrl" />
                        <SharePoint:FieldDescription ID="NorwegianUrlFieldDescription" runat="server" FieldName="NorwegianUrl" />
                        <input type="button" value="Search" id="btnNorwegianUrl" class="nc-articleedit-browse" ></input>
                    </td>
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="EnglishUrlFieldLabel" runat="server" FieldName="EnglishUrl" />
                            &#160;
                        </h3>
                    </td>
                   <td valign="top" class="ms-formbody" id="nc-articleeditform-englishurl">
                        <SharePoint:FormField ID="EnglishUrlFormField" runat="server" FieldName="EnglishUrl" />
                        <SharePoint:FieldDescription ID="EnglishUrlFieldDescription" runat="server" FieldName="EnglishUrl" />
                        <input type="button" value="Search" id="btnEnglishUrl" class="nc-articleedit-browse" ></input>
                    </td>
                </tr>
                <tr>
                    <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="PublishAtFieldLabel" runat="server" FieldName="Organization_x0020_Unit" />
                            &#160;
                        </h3>
                    </td>
                    <td valign="top" class="ms-formbody">
                        <SharePoint:MultipleLookupField ID="PublishAtField" runat="server" FieldName="Organization_x0020_Unit" />
                        <SharePoint:FieldDescription ID="PublishAtFieldDescription" runat="server" FieldName="Organization_x0020_Unit" />
                    </td>
                </tr>
                <tr id="nc-articleeditform-frontpageheader1">
                    <SharePoint:CompositeField ID="FrontPageHeader1Field" runat="server" FieldName="FrontpageHeader1" />
                </tr>
                <tr>
                <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="FrontpageTopnewsImageFieldLabel" runat="server" FieldName="FrontpageTopnewsImage" />
                            <br />(width 552px)
                            &#160;
                        </h3>
                    </td>
                    <td valign="top" class="ms-formbody" id="nc-articleeditform-topnewsimage">
                        <SharePoint:FormField ID="FrontpageTopnewsImageField" runat="server" FieldName="FrontpageTopnewsImage" />
                        <SharePoint:FieldDescription ID="FrontpageTopnewsImageFieldDescription" runat="server" FieldName="FrontpageTopnewsImage" />
                    </td>
                </tr>
                <tr id="nc-articleeditform-frontpageheader2">
                    <SharePoint:CompositeField ID="FrontPageHeader2Field" runat="server" FieldName="FrontPageHeader2" />
                </tr>
                <tr>
                <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="FrontpageProfilenewsImageFieldLabel" runat="server" FieldName="FrontpageProfilenewsImage" />
                            <br />(width 108 x height 128px)<br /> (or width 352px)
                            &#160;
                        </h3>
                    </td>
                    <td valign="top" class="ms-formbody" id="nc-articleeditform-profilenewsimage">
                        <SharePoint:FormField ID="FrontpageProfilenewsImageField" runat="server" FieldName="FrontpageProfilenewsImage" />
                        <SharePoint:FieldDescription ID="FrontpageProfilenewsImageFieldDescription" runat="server" FieldName="FrontpageProfilenewsImage" />
                    </td>
                </tr>
                <tr id="nc-articleeditform-header">
                    <SharePoint:CompositeField ID="ArticleHeaderFormField" runat="server" FieldName="ArticleHeader" />
                </tr>
                <tr>
                <td nowrap="true" valign="top" class="ms-formlabel">
                        <h3 class="ms-standardheader">
                            <SharePoint:FieldLabel ID="ArticleTopImageFieldLabel" runat="server" FieldName="ArticleTopImage" />
                            <br />(width 552px)
                            &#160;
                        </h3>
                    </td>
                    <td valign="top" class="ms-formbody" id="nc-articleeditform-topimage">
                        <SharePoint:FormField ID="ArticleTopImageField" runat="server" FieldName="ArticleTopImage" />
                        <SharePoint:FieldDescription ID="ArticleTopImageFieldDescription" runat="server" FieldName="ArticleTopImage" />
                    </td>
                </tr>
                <tr id="nc-articleeditform-topimagetext">
                    <SharePoint:CompositeField ID="ArticleTopImageTextField" runat="server" FieldName="ArticleTopImageText" />
                </tr>
                <tr id="nc-articleeditform-bodytext">
                    <SharePoint:CompositeField ID="ArticleBodyTextField" runat="server" FieldName="ArticleBodyText" />
                </tr>
                <tr id="nc-articleeditform-links">
                    <SharePoint:CompositeField ID="RightColumnLinksField" runat="server" FieldName="RightColumnLinks" />
                </tr>
                <tr id="nc-articleeditform-facts">
                    <SharePoint:CompositeField ID="RightColumnFactsField" runat="server" FieldName="RightColumnFacts" />
                </tr>
                <SharePoint:ApprovalStatus ID="ApprovalStatus1" runat="server" />
                <SharePoint:FormComponent ID="FormComponent1" TemplateName="AttachmentRows" runat="server" />
            </table>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="ms-formline">
                        <img src="/_layouts/images/blank.gif" width='1' height='1' alt="" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
                <tr>
                    <td width="100%">
                        <SharePoint:ItemHiddenVersion ID="ItemHiddenVersion1" runat="server" />
                        <SharePoint:ParentInformationField ID="ParentInformationField1" runat="server" />
                        <SharePoint:InitContentType ID="InitContentType1" runat="server" />
                        <wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbl" RightButtonSeparator="&amp;#160;"
                            runat="server">
                            <template_buttons>
						<SharePoint:CreatedModifiedInfo runat="server"/>
					</template_buttons>
                            <template_rightbuttons>
						<SharePoint:SaveButton runat="server"/>
						<SharePoint:GoBackButton runat="server"/>
					</template_rightbuttons>
                        </wssuc:ToolBar>
                    </td>
                </tr>
            </table>
        </span>
        <SharePoint:AttachmentUpload ID="AttachmentUpload1" runat="server" />
        <script type="text/javascript">

            var headertext;
            var header = $("textarea", "#nc-articleeditform-header");
            var header1 = $("textarea", "#nc-articleeditform-frontpageheader1");
            var header2 = $("textarea", "#nc-articleeditform-frontpageheader2");

            $(document).ready(function () {

                $(".s4-wpTopTable").css("width", "0");
                $("#nc-articleeditform-authorlist span select").css("width", "388px");

                $("img", "#nc-articleeditform-topimage").css("max-width", "552px");
                $("img", "#nc-articleeditform-topnewsimage").css("max-width", "552px");
                $("img", "#nc-articleeditform-profilenewsimage").css("max-width", "352px");
                $(".ms-standardheader").css("line-height", "16px");

                $(".ms-rtefield:eq(0)", "#nc-articleeditform-bodytext").css("width", "548px");
                $(".ms-rtefield:eq(0)", "#nc-articleeditform-links").css("min-width", "162px").css("width", "162px");
                $(".ms-rtefield:eq(0)", "#nc-articleeditform-facts").css("min-width", "162px").css("width", "162px");

                $("textarea", "#nc-articleeditform-topimagetext")
                .addClass("nc-articleedit-header")
                .css("width", "548px")
                .css("height", "36px");

                header
                .addClass("nc-articleedit-header")
                .css("width", "548px")
                .css("height", "120px")
                .focus(function () {
                    headertext = header.val();
                })
                .blur(function () {
                    if (header1.val() == "" || header1.val() == headertext)
                        header1.val(header.val());
                    if (header2.val() == "" || header2.val() == headertext)
                        header2.val(header.val());
                });

                header1
                .addClass("nc-articleedit-header")
                .css("width", "548px")
                .css("height", "120px")
                .blur(function () {
                    if (header1.val() == "")
                        header1.val(header.val());
                });

                header2
                .addClass("nc-articleedit-header")
                .css("width", "354px")
                .css("height", "120px")
                .blur(function () {
                    if (header2.val() == "")
                        header2.val(header.val());
                });

                $("#btnDanishUrl, #btnNorwegianUrl, #btnEnglishUrl").click(function () {
                    OpenDialog('/_layouts/NCNewssiteApplicationPages/ArticleSelectDialog.aspx', this.id);
                });

                $("#nc-articleeditform-danishurl span:eq(0) br").remove();
                $("#nc-articleeditform-englishurl span:eq(0) br").remove();
                $("#nc-articleeditform-norwegianurl span:eq(0) br").remove();
            });

            function PreSaveAction() {
                var authorAD = $(".ms-inputuserfield span span", "#nc-articleeditform-authorad").html();
                var authorList = $("#nc-articleeditform-authorlist span select option:selected")
                var authorText = $("#nc-articleeditform-authortext span input")

                var valid = !(authorAD == null && authorList.val() == "0" && authorText.val().trim() == "");
                $("#nc-articleedit-required").css("display", valid ? "none" : "block");
                return valid;
            }
            function OpenDialog(dialogUrl, controlId) {
                var options = SP.UI.$create_DialogOptions();
                var url = location.protocol + "//" + window.location.host + window.location.pathname;
                url = url.replace("/Lists/Articles/EditForm.aspx", "");
                url = url.replace("/Lists/Articles/NewForm.aspx", "");

                options.title = "Search for article";
                allowMaximize = false;
                options.url = url + dialogUrl;
                if (controlId == "btnDanishUrl")
                    options.dialogReturnValueCallback = CloseCallbackForDanish;
                else if (controlId == "btnNorwegianUrl")
                    options.dialogReturnValueCallback = CloseCallbackForNorwegian;
                else
                    options.dialogReturnValueCallback = CloseCallbackForEnglish;

                SP.UI.ModalDialog.showModalDialog(options);
            }
            function CloseCallbackForDanish(dialogResult, returnValue) {
                var danishUrltextbox = $("#nc-articleeditform-danishurl > span > input:eq(0)");
                danishUrltextbox.val(calculateUrl(returnValue));
            }

            function CloseCallbackForNorwegian(dialogResult, returnValue) {
                var norwegianURLtextbox = $("#nc-articleeditform-norwegianurl > span > input:eq(0)");
                norwegianURLtextbox.val(calculateUrl(returnValue));
            }
            function CloseCallbackForEnglish(dialogResult, returnValue) {
                var englishUrltextbox = $("#nc-articleeditform-englishurl > span > input:eq(0)");
                englishUrltextbox.val(calculateUrl(returnValue));
            }
            function calculateUrl(value) {
                if (value != null) {
                    var articleArray = value.split('|');
                    var url = window.location.pathname;
                    url = url.replace("/EditForm.aspx", "");
                    url = url.replace("/NewForm.aspx", "");
                    return (url + "/dispform.aspx?id=" + articleArray[0]);
                }
            }
        </script>
    </Template>
</SharePoint:RenderingTemplate>
