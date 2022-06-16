<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant.Master" AutoEventWireup="true"
    CodeBehind="ViewReservations.aspx.cs" Inherits="RestaurantManagementSystem.ViewReservations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <h3>
            Your Upcoming Reservations:
        </h3>
    </div>
    <table id="tables" style="margin-left: 20%; margin-right: 20%;">
        <tr>
            <th style="margin-left: auto; margin-right: auto;">
                <asp:Label ID="lblHeader" runat="server" Text="My Reservations"></asp:Label>
            </th>
        </tr>
        <tr>
            <td  >
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <table id="InLine" style="Height: 150px; Width:150px;">
                            <tr>
                                <td>
                                    <asp:Image ID="Table_icon" ImageUrl="~/images/table_icon.png" Height="150px" Width="150px" runat="server" />
                                </td>
                                <td>
                                    Date:&nbsp;
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("ReservationDate", "{0:dd/MM/yyyy}") %>'></asp:Label><br />
                                    Time:&nbsp;

                                    <asp:Label ID="lblTimeIn" runat="server" Text='<%# Eval("Time_in") %>'></asp:Label>
                                    ->
                                    <asp:Label ID="lblTimeOut" runat="server" Text='<%# Eval("Time_out") %>'></asp:Label><br />
                                    Table:&nbsp;
                                    <asp:Label ID="lblTable" runat="server" Text='<%# Eval("NoOfPeople") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('are you sure you want to delete this reservation?')"
                                        Text="Delete" CommandName="delete" Style="border: solid 1px #ddd" Height="40px"
                                        Width="140px" BackColor="#99FFBB" ForeColor="red"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
</asp:Content>
