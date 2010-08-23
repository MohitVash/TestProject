<%@ Assembly Name="NCNewssiteApplicationPages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=713ed81dc3ee48c5" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsBlankSite.aspx.cs" Inherits="NCNewssiteApplicationPages.IsBlankSite" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    Is Blank Site:
    <asp:DropDownList id="dropdownList" runat="server">
    <asp:ListItem Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
    </asp:DropDownList>  &nbsp;&nbsp; 
    <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
&nbsp;    
</asp:Content>


