<%@ Assembly Name="NCNewssiteApplicationPages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=713ed81dc3ee48c5" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowOnNavigation.aspx.cs" Inherits="NCNewssiteApplicationPages.ShowOnNavigation" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<table>
    <tr>
    <td>Show on Breadcrumbs:
    </td>
    <td><asp:DropDownList id="ddBreadcrumb" runat="server" >
    <asp:ListItem Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
    <tr>
    <td>Show on Left Navigation:
    </td>
    <td><asp:DropDownList id="ddLeftNav" runat="server">
    <asp:ListItem Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td>
    <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
        </td><td>
            </td></tr>
    </table>
</asp:Content>


