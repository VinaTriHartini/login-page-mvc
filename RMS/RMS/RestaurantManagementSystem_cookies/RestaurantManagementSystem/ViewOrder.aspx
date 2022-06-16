<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant.Master" AutoEventWireup="true" CodeBehind="ViewOrder.aspx.cs" Inherits="RestaurantManagementSystem.ViewOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<Link rel="stylesheet" type="text/css" href="StyleSheet2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div align="center">
<h3>
    <asp:Label ID="lblMsg" Font-Bold="true" runat="server" Text="Label"></asp:Label>
    <br/><br/>
</h3>
</div>

<table id="tables" align="center" style="margin-left:20%; margin-right: 20%; border: 1px solid #ddd">

<tr>
<td>
    <asp:Repeater ID="rptOrder" runat="server">
    <ItemTemplate>
        <table id="InLine" style="padding: 0; margin: 0; width: 300px; height: 300px">

            <tr>
            <th>
                <b>
                    <%#Eval("Name") %>
                </b>    
            </th>
            </tr>

            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="Image1" ImageUrl='<%# Eval("image") %>' Height="100px" Width="100px" runat="server" />
                                </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text="Name: "></asp:Label>&nbsp;
                                <asp:Label ID="lblDBName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                <br/>
                                <asp:Label ID="lblQuantity" runat="server" Text="Quantity: "></asp:Label>&nbsp;
                                <asp:Label ID="lblDBQuantity" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                                <br/>
                                <asp:Label ID="lblPrice" runat="server" Text="Price: "></asp:Label>&nbsp;
                                <asp:Label ID="lblDBPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                <br/>
                                <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>&nbsp;
                                <asp:Label ID="lblDBDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                     
                </td>
            </tr>
            
        </table>
    </ItemTemplate>
    </asp:Repeater>
</td>
</tr>
<tr style="border: 1px solid #ddd">
    <td align="center">
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Menu.aspx" runat="server">Order More Food </asp:HyperLink>
    </td>
</tr>

</table>
</asp:Content>
