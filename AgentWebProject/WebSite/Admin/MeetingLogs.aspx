<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master"
    AutoEventWireup="true" CodeFile="MeetingLogs.aspx.cs" Inherits="Admin_MeetingLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <!-- End .clear -->
        <div class="content-box">
            <!-- Start Content Box -->
            <div class="content-box-header">
                <h3>
                    Select the date</h3>
                <div class="clear">
                </div>
            </div>
            <!-- End .content-box-header -->
            <div class="content-box-content">
                <div class="tab-content default-tab" id="tab1">
                    <table class="style1">
                        <tr>
                            <td>
                                StartTime:
                            </td>
                            <td>
                                EndTime:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Calendar ID="Calendar1" runat="server" 
                                    OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" 
                                    BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                                    Width="200px">
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                    <NextPrevStyle VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#808080" />
                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" />
                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                </asp:Calendar>
                            </td>
                            <td>
                                <asp:Calendar ID="Calendar2" runat="server" 
                                    OnSelectionChanged="Calendar2_SelectionChanged" BackColor="White" 
                                    BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                                    Width="200px">
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                    <NextPrevStyle VerticalAlign="Bottom" />
                                    <OtherMonthDayStyle ForeColor="#808080" />
                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" />
                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                </asp:Calendar>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
    </asp:Panel>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>
                Meeting Logs</h3>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="div2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="MeetingLog_Id" onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="EventTime" HeaderText="EventTime" SortExpression="EventTime" />
                        <asp:BoundField DataField="RoomName" HeaderText="RoomName" SortExpression="FullName" />
                        <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName" />
                        <asp:TemplateField HeaderText="Log In / Log Out">
                            <ItemTemplate>
                                <%#Eval("Status").ToString().ToUpper() == "1" ? "Log In" : "Log Out"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
