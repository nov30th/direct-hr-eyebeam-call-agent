<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="TimePeriodSetting.aspx.cs" Inherits="Admin_TimePeriodSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Hosts Time Period Setting</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>Time Period Setting</h3>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">
                <form id="form1" runat="server">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="TimePeriod_Id" DataSourceID="LinqDataSource1" InsertItemPosition="LastItem">
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriod_IdLabel" runat="server" Text='<%# Eval("TimePeriod_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TimeNameLabel" runat="server" Text='<%# Eval("TimeName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="StartAtLabel" runat="server" Text='<%# Eval("StartAt") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="EndAtLabel" runat="server" Text='<%# Eval("EndAt") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriod_IdLabel1" runat="server" Text='<%# Eval("TimePeriod_Id") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="TimeNameTextBox" runat="server" Text='<%# Bind("TimeName") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="StartAtTextBox" runat="server" Text='<%# Bind("StartAt") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="EndAtTextBox" runat="server" Text='<%# Bind("EndAt") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="TimeNameTextBox" runat="server" Text='<%# Bind("TimeName") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="StartAtTextBox" runat="server" Text='<%# Bind("StartAt") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="EndAtTextBox" runat="server" Text='<%# Bind("EndAt") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriod_IdLabel" runat="server" Text='<%# Eval("TimePeriod_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TimeNameLabel" runat="server" Text='<%# Eval("TimeName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="StartAtLabel" runat="server" Text='<%# Eval("StartAt") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="EndAtLabel" runat="server" Text='<%# Eval("EndAt") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="">
                                                <th runat="server"></th>
                                                <th runat="server">TimePeriod_Id</th>
                                                <th runat="server">TimeName</th>
                                                <th runat="server">StartAt</th>
                                                <th runat="server">EndAt</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="">
                                        <asp:DataPager ID="DataPager1" runat="server">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriod_IdLabel" runat="server" Text='<%# Eval("TimePeriod_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TimeNameLabel" runat="server" Text='<%# Eval("TimeName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="StartAtLabel" runat="server" Text='<%# Eval("StartAt") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="EndAtLabel" runat="server" Text='<%# Eval("EndAt") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DhrAgentDatabaseUtils.DBConn" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="TimePeriod">
                    </asp:LinqDataSource>
                    <br />
                    <br />
                </form>
            </div>
        </div>
    </div>
</asp:Content>

