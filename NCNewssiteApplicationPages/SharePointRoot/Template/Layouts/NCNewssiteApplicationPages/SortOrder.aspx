<%@ Assembly Name="NCNewssiteApplicationPages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=713ed81dc3ee48c5" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortOrder.aspx.cs" Inherits="NCNewssiteApplicationPages.SortOrder" DynamicMasterPageFile="~masterurl/default.master" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortOrder.aspx.cs"
    Inherits="NCNewssiteApplicationPages.SortOrder" MasterPageFile="~/SharePointRoot/Template/Layouts/site1.master" %>--%>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    Sortorder:
    <br />
    <asp:textbox ID="TextBox1" runat="server"></asp:textbox>    
    <asp:Button ID="Button1" runat="server" Text="Update" onclick="Button1_Click" />
&nbsp;    
</asp:Content>


