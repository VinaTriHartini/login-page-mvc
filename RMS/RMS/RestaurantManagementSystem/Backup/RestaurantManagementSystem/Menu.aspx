<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant.Master" AutoEventWireup="true"
    CodeBehind="Menu.aspx.cs" Inherits="RestaurantManagementSystem.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <h3>
            Food Menu&nbsp;&nbsp;&nbsp;
        </h3>
    </div>
    <table id="tables" style="margin-left: 20%; margin-right: 20%">
        <tr>
            <td>
                <div id="menu_discDiv">
                    <p>
                        All burgers are served with lettuce, tomato, red onions and house cut fries. Sweet
                        potato fries or onion rings just R2 extra. Be sure to ask your server about our
                        daily dessert specials and our house made artisan ice cream! All sandwiches are
                        served with fries and coleslaw. Sweet potato fries, quinoa salad, side tossed salad
                        available for R2 each.
                    </p>
                </div>
            </td>
            <td>
                <div id="orderDetailDiv">
                    <table>
                        <tr>
                            <th style="text-align: center">
                                Order Information
                            </th>
                        </tr>
                        <tr>
                            <td align="center">
                                <br />
                                Number of items added:&nbsp;<br/><asp:Label ID="lblNoOfItems" Font-Bold="true" Text="0"
                                    runat="server"></asp:Label>&nbsp; item(s) &nbsp;<br />
                                Total Price: &nbsp; <b>R</b>
                                <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <br />
                                <br />
                                <asp:Button ID="btnViewMyOrder" runat="server" Text="View My Order" 
                                    BackColor="#FF6666" Style="border: solid 1px #ddd"
                                    Height="40px" Width="200px" onclick="btnViewMyOrder_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <br />
                <br />
                <br />
                <div id="menuDiv">
                    <table>
                        <tr>
                            <th style="text-align: center">
                                Our Menu
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="rpt" runat="server">
                                    <ItemTemplate>
                                        <table id="InLine">
                                            <tr>
                                                <td>
                                                    <a href="Item.aspx?id=<%# Eval("MealId") %>">
                                                        <img alt="" height="140px" width="140px" src='<%# Eval("image") %>' /></a>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label><br />
                                                    R<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
