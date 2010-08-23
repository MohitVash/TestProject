<%@ Control Language="C#"   AutoEventWireup="false" %>
<%@Assembly Name="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@Register TagPrefix="SharePoint" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" namespace="Microsoft.SharePoint.WebControls"%>
<%@Register TagPrefix="ApplicationPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" namespace="Microsoft.SharePoint.ApplicationPages.WebControls"%>
<%@Register TagPrefix="SPHttpUtility" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" namespace="Microsoft.SharePoint.Utilities"%>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="~/_controltemplates/ToolBarButton.ascx" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<SharePoint:RenderingTemplate id="NCArticleListForm" runat="server">
	<Template>
        <SharePoint:InformationBar ID="InformationBar1" runat="server"/>
		<SharePoint:FormToolBar ID="FormToolBar1" runat="server"/>
		<SharePoint:ItemValidationFailedMessage ID="ItemValidationFailedMessage1" runat="server"/>
        <div class="span-1">
            &nbsp;
        </div>
        <div class="span-3">
    		<h2 class="articlePageHeader"><SharePoint:FormField runat="server" ID="TitleField" ControlMode="Display" FieldName="Title" /></h2>
            <p class="byline">
                &nbsp;<SharePoint:FormField runat="server" ID="ArticleAuthorField" ControlMode="Display" FieldName="ArticleAuthor" />
            </p>
            <div class="articleImage">
                <SharePoint:FormField runat="server" ID="ArticleTopImageField" ControlMode="Display" FieldName="ArticleTopImage" />
                <SharePoint:FormField runat="server" ID="ArticleTopImageTextField" ControlMode="Display" FieldName="ArticleTopImageText" />
            </div>

            <div class="leadText">
                (<%=((DateTime)SPContext.Current.ListItem["Created"]).ToShortDateString()%>) - 
                <SharePoint:FormField runat="server" ID="ArticleHeaderField" ControlMode="Display" FieldName="ArticleHeader" />
            </div>            
            
            <div class="bodyText">
                <SharePoint:FormField runat="server" ID="ArticleBodyTextField" ControlMode="Display" FieldName="ArticleBodyText" />
            </div>
        </div>
        <div class="articleRightColumn span-1 last">
			<div class="header last">
				Related links
			</div>
			<div class="content last">
                <SharePoint:FormField runat="server" ID="RightColumnLinksField" ControlMode="Display" FieldName="RightColumnLinks" />
			</div>

			<div class="header last">
				Related news
			</div>
			<div class="content last">
                <SharePoint:FormField runat="server" ID="RightColumnFactsField" ControlMode="Display" FieldName="RightColumnFacts" />
			</div>
        </div>
	</Template>
</SharePoint:RenderingTemplate>
