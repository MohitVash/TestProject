<%@ Assembly Name="NCNewssiteApplicationPages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=713ed81dc3ee48c5" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleSelectDialog.aspx.cs" Inherits="NCNewssiteApplicationPages.ArticleSelectDialog" DynamicMasterPageFile="~masterurl/default.master" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleSelectDialog.aspx.cs"
    Inherits="NCNewssiteApplicationPages.ArticleSelectDialog" MasterPageFile="~/SharePointRoot/Template/Layouts/site1.master" %>
--%>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table style="width: 900px">
        <tr>
            <td style="width: 98px">
                <asp:Label ID="lblArticleTitle" runat="server" Text="Article Title:"></asp:Label>
            </td>
            <td style="width: 456px">
                <asp:TextBox runat="server" ID="txtTitleSearch" Width="449px"></asp:TextBox>
            </td>
            <td>
                <asp:LinkButton runat="server" ID="btnSearch" OnClick="btnSearch_Click">Search</asp:LinkButton>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 98px">
            </td>
            <td style="width: 456px">
            </td>
            <td>
            </td>
            <td style="text-align: right">
                <asp:Button runat="server" ID="btnCloseTop" OnClick="btnClose_Click" Text="OK" Width="69px" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 900px;">
 
        <tr>
            <td style="margin-left: 80px">
                <div style="height: 500px; overflow: auto">
                    <asp:GridView runat="server" ID="gvResult" AutoGenerateColumns="False" EmptyDataText="No articles found"
                        EnableModelValidation="True" DataKeyNames="ID, Title" CellPadding="10" CssClass="ms-vb2"
                        ForeColor="#333333" GridLines="None" 
                        onrowdatabound="gvResult_RowDataBound" Width="900px" allowsorting="True" OnSorting="gvResult_Sorting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:ButtonField CommandName="Select" HeaderText="" Text=""></asp:ButtonField>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                            
                            </asp:BoundField>
                            <asp:BoundField DataField="Title" HeaderText="Title" sortexpression="Title"></asp:BoundField>
                            <asp:BoundField DataField="ArticleAuthor" HeaderText="Author" sortexpression="ArticleAuthor"></asp:BoundField>
                            <asp:BoundField DataField="Organization_x0020_Unit" HeaderText="Organization Unit" sortexpression="Organization_x0020_Unit">
                            </asp:BoundField>
                            <asp:BoundField DataField="PublishingStart" HeaderText="Publishing Start" sortexpression="PublishingStart"></asp:BoundField>
                            <asp:BoundField DataField="PublishingEnd" HeaderText="Publishing End" sortexpression="PublishingEnd"></asp:BoundField>
                            <asp:BoundField DataField="Created" HeaderText="Created Date" sortexpression="Created"></asp:BoundField>
                            <asp:BoundField DataField="Modified" HeaderText="Modified date" sortexpression="Modified"></asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; margin-left: 80px">
                <asp:Button runat="server" ID="btnCloseBottom" OnClick="btnClose_Click" Text="OK"
                    Width="69px" />
            </td>
        </tr>
    </table>
    <div>
    </div>
</asp:Content>
