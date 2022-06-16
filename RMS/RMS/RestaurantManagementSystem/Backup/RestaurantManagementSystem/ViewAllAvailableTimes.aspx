<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant.Master" AutoEventWireup="true"
    CodeBehind="ViewAllAvailableTimes.aspx.cs" Inherits="RestaurantManagementSystem.ViewAllAvailableTimes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <h3>
            Avaiability details for
            <asp:Label ID="lblDate" runat="server"></asp:Label></h3>
    </div>
    <br />
    <div align="center" id="tables">
        <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/calender_icon.png" 
            Height="45px" Width="45px" runat="server" onclick="ImageButton1_Click" />
        <asp:Calendar ID="Calendar1" runat="server" 
            onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
    </div>
    <br/>
    <table id="tables" style="margin-left: auto; margin-right: auto;">
        <tr>
            <td>
                <table>
                    <tr>
                    <th>
                        <asp:Label ID="lblTable1" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblDate1" runat="server"></asp:Label>
                    </th>
                    </tr>

                    <tr>
                        <td>
                            <asp:Repeater ID="rptAvailableTimes1" runat="server" 
                                onitemdatabound="rptAvailableTimes1_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RTimes") %>'></asp:Label>
                                                ->
                                                <asp:Label ID="lblTimeStatus" runat="server" Text='<%# Eval("TStatus") %>'></asp:Label>
                                            
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
            <!-- ******************************************************************************************************************************************** -->

            <td>
                <table>
                    <tr>
                    <th>
                        <asp:Label ID="lblTable2" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblDate2" runat="server"></asp:Label>
                    </th>
                    </tr>

                    <tr>
                        <td>
                            <asp:Repeater ID="rptAvailableTimes2" runat="server" 
                                onitemdatabound="rptAvailableTimes2_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RTimes") %>'></asp:Label>
                                                ->
                                                <asp:Label ID="lblTimeStatus" runat="server" Text='<%# Eval("TStatus") %>'></asp:Label>
                                            
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
            <!-- ****************************************************************************************************************************************************** -->

            <td>
                <table>
                    <tr>
                    <th>
                        <asp:Label ID="lblTable3" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblDate3" runat="server"></asp:Label>
                    </th>
                    </tr>

                    <tr>
                        <td>
                            <asp:Repeater ID="rptAvailableTimes3" runat="server" 
                                onitemdatabound="rptAvailableTimes3_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RTimes") %>'></asp:Label>
                                                ->
                                                <asp:Label ID="lblTimeStatus" runat="server" Text='<%# Eval("TStatus") %>'></asp:Label>
                                           
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
            <!-- ************************************************************************************************************************************************************** -->

            <td>
                <table>
                    <tr>
                    <th>
                        <asp:Label ID="lblTable4" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblDate4" runat="server"></asp:Label>
                    </th>
                    </tr>

                    <tr>
                        <td>
                            <asp:Repeater ID="rptAvailableTimes4" runat="server" 
                                onitemdatabound="rptAvailableTimes4_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RTimes") %>'></asp:Label>
                                                ->
                                                <asp:Label ID="lblTimeStatus" runat="server" Text='<%# Eval("TStatus") %>'></asp:Label>
                                            
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>

            <!-- *********************************************************************************************************************************************************** -->
            <td>
                <table>
                    <tr>
                    <th>
                        <asp:Label ID="lblTable5" runat="server"></asp:Label>&nbsp;
                        <asp:Label ID="lblDate5" runat="server"></asp:Label>
                    </th>
                    </tr>

                    <tr>
                        <td>
                            <asp:Repeater ID="rptAvailableTimes5" runat="server" 
                                onitemdatabound="rptAvailableTimes5_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RTimes") %>'></asp:Label>
                                                ->
                                                <asp:Label ID="lblTimeStatus" runat="server" Text='<%# Eval("TStatus") %>'></asp:Label>
                                            
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>
</asp:Content>
