<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="ClientsOnlineStatus.aspx.cs" Inherits="Admin_ClientsOnlineStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>Clients Online Status Overview</h3>
            <div class="clear">
            </div>
        </div>




        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">
                <form id="form1" runat="server">
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" OnSelectionChanged="Calendar1_SelectionChanged">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                    <br />
                    The data of OnlineMins below will ONLY shows WorkingTime. (Ends at 17:00)<br />
                    You can click the header to sorting the table.<br />
                    Color <a style="background-color: Chartreuse;">Chartreuse</a> shows the person is in development group and has all exceptions. (including blocker, normal DNS fixer). When you sorting not by Name, the development users will be hidden in the data for you see results much cleaner.<br />
                    <br />
                    <strong>System Online Time:
                        <%=((int)(systemTime / 60)).ToString("d") + "h" + ((int)(systemTime % 60)).ToString("d") + "m" %>

                    </strong>
                    <br />
                    <strong>
                        <asp:Label ID="lab_sortinghidden" runat="server" Text=""></asp:Label>
                    </strong>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnlineMins" SortExpression="OnlineMins">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("OnlineMins") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("OnlineMins") %>' Font-Bold="True"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LoginAt" HeaderText="LoginAt" SortExpression="LoginAt" />
                            <asp:BoundField DataField="LastLoginAt" HeaderText="LastLoginAt" SortExpression="LastLoginAt" />
                            <asp:BoundField DataField="ExtensionNumber" HeaderText="ExtensionNumber" SortExpression="ExtensionNumber" />
                            <asp:BoundField DataField="ComputerName" HeaderText="ComputerName" SortExpression="ComputerName" />
                            <asp:BoundField DataField="Memo" HeaderText="Memo" SortExpression="Memo" />
                            <asp:BoundField DataField="IsOnline" HeaderText="IsOnline" SortExpression="IsOnline" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <br />
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllOnlineStatus" TypeName="DhrAgentDatabaseUtils.LoginRecordMgr">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Calendar1" Name="datetime" PropertyName="SelectedDate" Type="DateTime" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <br />
                </form>
            </div>
        </div>
    </div>

</asp:Content>

