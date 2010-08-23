<%@ Register TagPrefix="WpNs0" Namespace="NCNewssitePatch1.UI.WebControls.WebParts"
    Assembly="NCNewssitePatch1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=191936cb64556eac" %>
<%-- _lcid="1033" _version="14.0.4762" _dal="1" --%>
<%-- _LocalBinding --%>

<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:ListItemProperty Property="BaseName" MaxLength="40" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <WebPartPages:WebPartZone runat="server" Title="loc:TitleBar" ID="TitleBar" AllowLayoutChange="false"
        AllowPersonalization="false">
        <ZoneTemplate>
            <%-- TODO: Hva er dette? Kan web parten fjernes? --%>
            <WebPartPages:TitleBarWebPart runat="server" AllowEdit="True" AllowConnect="True"
                ConnectionID="00000000-0000-0000-0000-000000000000" Title="Web Part Page Title Bar"
                IsIncluded="True" Dir="Default" IsVisible="True" AllowMinimize="False" ExportControlledProperties="True"
                ZoneID="TitleBar" ID="g_cafee769_c3b8_458c_9439_39a1c2414480" HeaderTitle="news"
                AllowClose="False" FrameState="Normal" ExportMode="All" AllowRemove="False" AllowHide="True"
                SuppressWebPartChrome="False" DetailLink="" ChromeType="None" HelpLink="" MissingAssembly="Cannot import this Web Part."
                PartImageSmall="" HelpMode="Modeless" FrameType="None" AllowZoneChange="True"
                PartOrder="1" Description="" PartImageLarge="" IsIncludedFilter="" __MarkupType="vsattributemarkup"
                __WebPartId="{CAFEE769-C3B8-458C-9439-39A1C2414480}" WebPart="true" Height=""
                Width=""></WebPartPages:TitleBarWebPart>
        </ZoneTemplate>
    </WebPartPages:WebPartZone>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderTitleAreaClass" runat="server">
    <style type="text/css">
        Div.ms-titleareaframe
        {
            height: 100%;
        }
        .ms-pagetitleareaframe table
        {
            background: none;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta name="GENERATOR" content="Microsoft SharePoint" />
    <meta name="ProgId" content="SharePoint.WebPartPage.Document" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="CollaborationServer" content="SharePoint Team Web Site" />
    <script type="text/javascript">
// <![CDATA[
        var navBarHelpOverrideKey = "WSSEndUser";
// ]]>
    </script>
    <SharePoint:UIVersionedContent ID="WebPartPageHideQLStyles" UIVersion="4" runat="server">
        <contenttemplate>
<style type="text/css">
    body #s4-leftpanel
    {
        display: none;
    }
    .s4-ca
    {
        margin-left: 0px;
    }
</style>
		</contenttemplate>
    </SharePoint:UIVersionedContent>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderSearchArea" runat="server">
    <SharePoint:DelegateControl runat="server" ControlId="SmallSearchInputBox" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderLeftActions" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
    <SharePoint:ProjectProperty Property="Description" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderBodyRightMargin" runat="server">
    <div height="100%" class="ms-pagemargin">
        <img src="/_layouts/images/blank.gif" width="10" height="1" alt="" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageImage" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderNavSpacer" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderLeftNavBar" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table cellpadding="4" cellspacing="0" border="0" width="100%">
        <tr>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="newsSideColumn newsLeftColumn wpLeftColumn">
                <WebPartPages:WebPartZone runat="server" Title="loc:LeftColumn" ID="LeftColumn" FrameType="TitleBarOnly">
                    <ZoneTemplate>
                    </ZoneTemplate>
                </WebPartPages:WebPartZone>
            </td>
            <td valign="top" style="padding: 0" class="newsContentColumn">
                <table cellpadding="4" cellspacing="0" border="0" width="100%" height="100%">
                    <tr>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" colspan="2" valign="top" width="100%" class="wpTopRow">
                            <WebPartPages:WebPartZone runat="server" Title="loc:TopRow" ID="TopRow" FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                    </tr>
                    <tr>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="wpCenterLeftColumnRow1">
                            <WebPartPages:WebPartZone runat="server" Title="loc:CenterLeftColumnRow1" ID="CenterLeftColumnRow1"
                                FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="wpCenterRightColumnRow1">
                            <WebPartPages:WebPartZone runat="server" Title="loc:CenterRightColumnRow1" ID="CenterRightColumnRow1"
                                FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                    </tr>
                    <tr>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="wpCenterLeftColumnRow2">
                            <WebPartPages:WebPartZone runat="server" Title="loc:CenterLeftColumnRow2" ID="CenterLeftColumnRow2"
                                FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="wpCenterRightColumnRow2">
                            <WebPartPages:WebPartZone runat="server" Title="loc:CenterRightColumnRow2" ID="CenterRightColumnRow2"
                                FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                    </tr>
                    <tr>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="wpCenterLeftColumnRow3">
                            <WebPartPages:WebPartZone runat="server" Title="loc:CenterLeftColumnRow3" ID="CenterLeftColumnRow3"
                                FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="wpCenterRightColumnRow3">
                            <WebPartPages:WebPartZone runat="server" Title="loc:CenterRightColumnRow3" ID="CenterRightColumnRow3"
                                FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                    </tr>
                    <tr>
                        <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" colspan="2" valign="top" width="100%" class="wpBottomRow">
                            <WebPartPages:WebPartZone runat="server" Title="loc:BottomRow" ID="BottomRow" FrameType="TitleBarOnly">
                                <ZoneTemplate>
                                </ZoneTemplate>
                            </WebPartPages:WebPartZone>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" valign="top" height="100%" class="newsSideColumn newsRightColumn wpRightColumn">
                <WebPartPages:WebPartZone runat="server" Title="loc:RightColumn" ID="RightColumn"
                    FrameType="TitleBarOnly">
                    <ZoneTemplate>
                    </ZoneTemplate>
                </WebPartPages:WebPartZone>
            </td>
        </tr>
        <tr>
            <td id="_invisibleIfEmpty" name="_invisibleIfEmpty" colspan="3" valign="top" width="100%" class="wpFooter">
                <WebPartPages:WebPartZone runat="server" Title="loc:Footer" ID="Footer" FrameType="TitleBarOnly">
                    <ZoneTemplate>
                    </ZoneTemplate>
                </WebPartPages:WebPartZone>
            </td>
        </tr>
        <script type="text/javascript" language="javascript">
            if (typeof (MSOLayout_MakeInvisibleIfEmpty) == "function") {
                MSOLayout_MakeInvisibleIfEmpty();
            }
        </script>
    </table>
</asp:Content>
