<%@ Page Language="C#" Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage,Microsoft.SharePoint.Publishing,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePointWebControls" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls"
    Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingNavigation" Namespace="Microsoft.SharePoint.Publishing.Navigation"
    Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="WebParts" Namespace="NCNewssitePatch1.UI.WebControls.WebParts"
    Assembly="NCNewssitePatch1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=191936cb64556eac" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent1" UIVersion="3" runat="server">
		<ContentTemplate>
			<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/pageLayouts.css %>" runat="server"/>
			<PublishingWebControls:editmodepanel runat="server" id="editmodestyles">
				<!-- Styles for edit mode only-->
				<SharePointWebControls:CssRegistration ID="CssRegistration1" name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/zz2_editMode.css %>" runat="server"/>
			</PublishingWebControls:editmodepanel>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent2" UIVersion="4" runat="server">
		<ContentTemplate>
			<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/page-layouts-21.css %>" runat="server"/>
			<PublishingWebControls:EditModePanel ID="EditModePanel1" runat="server">
				<!-- Styles for edit mode only-->
				<SharePointWebControls:CssRegistration ID="CssRegistration2" name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/edit-mode-21.css %>"
					After="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/page-layouts-21.css %>" runat="server"/>
			</PublishingWebControls:EditModePanel>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:CssRegistration ID="CssRegistration3" name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/rca.css %>" runat="server"/>
	<SharePointWebControls:FieldValue id="PageStylesField" FieldName="HeaderStyleDefinitions" runat="server"/>
</asp:Content>
<asp:Content contentplaceholderid="PlaceHolderPageTitle" runat="server">
	<SharePointWebControls:FieldValue id="PageTitle" FieldName="Title" runat="server"/>
</asp:Content>
<asp:Content contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent3" UIVersion="3" runat="server">
		<ContentTemplate>
			<SharePointWebControls:TextField runat="server" id="TitleField" FieldName="Title"/>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent4" UIVersion="4" runat="server">
		<ContentTemplate>
			<SharePointWebControls:FieldValue FieldName="Title" runat="server"/>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
</asp:Content>
<asp:Content contentplaceholderid="PlaceHolderTitleBreadcrumb" runat="server"> 
	<SharePointWebControls:VersionedPlaceHolder ID="VersionedPlaceHolder1" UIVersion="3" runat="server"> <ContentTemplate> <asp:SiteMapPath ID="siteMapPath" runat="server" SiteMapProvider="CurrentNavigation" RenderCurrentNodeAsLink="false" SkipLinkText="" CurrentNodeStyle-CssClass="current" NodeStyle-CssClass="ms-sitemapdirectional"/> </ContentTemplate> </SharePointWebControls:VersionedPlaceHolder> 
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent5" UIVersion="4" runat="server"> <ContentTemplate> <SharePointWebControls:ListSiteMapPath runat="server" SiteMapProviders="CurrentNavigation" RenderCurrentNodeAsLink="false" PathSeparator="" CssClass="s4-breadcrumb" NodeStyle-CssClass="s4-breadcrumbNode" CurrentNodeStyle-CssClass="s4-breadcrumbCurrentNode" RootNodeStyle-CssClass="s4-breadcrumbRootNode" NodeImageOffsetX=0 NodeImageOffsetY=353 NodeImageWidth=16 NodeImageHeight=16 NodeImageUrl="/_layouts/images/fgimg.png" HideInteriorRootNodes="true" SkipLinkText="" /> </ContentTemplate> </SharePointWebControls:UIVersionedContent> </asp:Content>
<asp:Content contentplaceholderid="PlaceHolderMain" runat="server">
 <table cellpadding="4" cellspacing="0" border="0" width="100%">
        <tr>
        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="newsSideColumn newsLeftColumn wpLeftColumn">
                <WebParts:NCNewssiteLeftNavigationWebPart runat="server" AllowEdit="True" AllowConnect="True" ConnectionID="00000000-0000-0000-0000-000000000000" Title="NC Newssite left navigation web part" IsIncluded="True" Dir="Default" IsVisible="True" AllowMinimize="True" ExportControlledProperties="True" ZoneID="LeftColumn" ID="g_496bbb39_3359_4f1e_993b_68328dfbab5d" FrameState="Normal" ExportMode="All" AllowHide="True" SuppressWebPartChrome="False" DetailLink="" HelpLink="" ImportErrorMessage="Cannot import NCNewssiteLeftNavigation Web Part." MissingAssembly="Cannot import NCNewssiteLeftNavigation Web Part." PartImageSmall="" AllowRemove="True" HelpMode="Modeless" FrameType="Default" AllowZoneChange="True" PartOrder="1" Description="Render menu based on page structure in Pages list" PartImageLarge="" IsIncludedFilter="" __MarkupType="vsattributemarkup" __WebPartId="{496BBB39-3359-4F1E-993B-68328DFBAB5D}" WebPart="true" Height="" Width="">
                    </WebParts:NCNewssiteLeftNavigationWebPart>
            </td>
<td>
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent6" UIVersion="3" runat="server">
		<ContentTemplate>
			<table id="MSO_ContentTable" cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<div class="pageContent">
							<PublishingWebControls:RichHtmlField id="content" FieldName="PublishingPageContent" runat="server" />
						</div>
					</td>
				</tr>
			</table>
			<div style="clear:both">&#160;</div>
			<PublishingWebControls:editmodepanel runat="server" id="editmodepanel2">
				<!-- Add field controls here to bind custom metadata viewable and editable in edit mode only.-->
				<table cellpadding="10" cellspacing="0" align="center" class="editModePanel">
					<tr>
						<td>
							<PublishingWebControls:RichImageField id="ContentQueryImage" FieldName="PublishingRollupImage" AllowHyperLinks="false" runat="server"/>
						</td>
						<td width="200">
							<asp:label text="<%$Resources:cms,Article_rollup_image_text%>" runat="server" />
						</td>
					</tr>
				</table>
			</PublishingWebControls:editmodepanel>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:UIVersionedContent ID="UIVersionedContent7" UIVersion="4" runat="server">
		<ContentTemplate>
			<div class="article article-body">
				<PublishingWebControls:EditModePanel ID="EditModePanel3" runat="server" CssClass="edit-mode-panel">
					<SharePointWebControls:TextField ID="TextField1" runat="server" FieldName="Title"/>
				</PublishingWebControls:EditModePanel>
				<div class="article-content">
					<PublishingWebControls:RichHtmlField ID="RichHtmlField1" FieldName="PublishingPageContent" HasInitialFocus="True" MinimumEditHeight="400px" runat="server"/>
				</div>
				<PublishingWebControls:EditModePanel ID="EditModePanel4" runat="server" CssClass="edit-mode-panel roll-up">
					<PublishingWebControls:RichImageField ID="RichImageField1" FieldName="PublishingRollupImage" AllowHyperLinks="false" runat="server" />
					<asp:Label text="<%$Resources:cms,Article_rollup_image_text%>" runat="server" />
				</PublishingWebControls:EditModePanel>
			</div>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	</td></tr>
    </table>
</asp:Content>
