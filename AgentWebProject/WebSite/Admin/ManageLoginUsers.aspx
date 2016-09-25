<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="ManageLoginUsers.aspx.cs" Inherits="Admin_ManageLoginUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Manage Login Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>Account</h3>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">
                <form id="form1" runat="server">
                    <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1">
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="LastLoginLabel" runat="server" Text='<%# Eval("LastLogin") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="LastLoginIPLabel" runat="server" Text='<%# Eval("LastLoginIP") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PriLabel" runat="server" Text='<%# Eval("Pri") %>' />
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
                                    <asp:TextBox ID="UsernameTextBox" runat="server" Text='<%# Bind("Username") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="LastLoginTextBox" runat="server" Text='<%# Bind("LastLogin") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="LastLoginIPTextBox" runat="server" Text='<%# Bind("LastLoginIP") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="PriTextBox" runat="server" Text='<%# Bind("Pri") %>' />
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
                                <td>
                                    <asp:TextBox ID="UsernameTextBox" runat="server" Text='<%# Bind("Username") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="LastLoginTextBox" runat="server" Text='<%# Bind("LastLogin") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="LastLoginIPTextBox" runat="server" Text='<%# Bind("LastLoginIP") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="PriTextBox" runat="server" Text='<%# Bind("Pri") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="LastLoginLabel" runat="server" Text='<%# Eval("LastLogin") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="LastLoginIPLabel" runat="server" Text='<%# Eval("LastLoginIP") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PriLabel" runat="server" Text='<%# Eval("Pri") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr runat="server" style="">
                                                <th runat="server">Username</th>
                                                <th runat="server">LastLogin</th>
                                                <th runat="server">LastLoginIP</th>
                                                <th runat="server">Pri</th>
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
                                    <asp:Label ID="UsernameLabel" runat="server" Text='<%# Eval("Username") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="LastLoginLabel" runat="server" Text='<%# Eval("LastLogin") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="LastLoginIPLabel" runat="server" Text='<%# Eval("LastLoginIP") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PriLabel" runat="server" Text='<%# Eval("Pri") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <br />
                    Change Password:<br />
                    Username<asp:TextBox ID="txt_change_username" runat="server"></asp:TextBox>
                    <br />
                    Old Password<asp:TextBox ID="txt_change_oldpwd" runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                    New Password<asp:TextBox ID="txt_change_newpwd" runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:Button ID="btn_changepwd" runat="server" OnClick="btn_changepwd_Click" Text="Change Password" />
                    <br />
                    <asp:Label ID="lab_changepassword" runat="server" ForeColor="Red" Text=".."></asp:Label>
                    <br />
                    <br />
                    Create Account:<br />
                    Username<asp:TextBox ID="txt_new_username" runat="server"></asp:TextBox>
                    <br />
                    New Password<asp:TextBox ID="txt_new_newpwd" runat="server" TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:Button ID="btn_createuser" runat="server" Text="Create New User" OnClick="btn_createuser_Click" />
                    <br />
                    <asp:Label ID="lab_newuser" runat="server" ForeColor="Red" Text=".."></asp:Label>
                    <br />
                    <br />
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DhrAgentDatabaseUtils.DBConn" EntityTypeName="" Select="new (Username, LastLogin, LastLoginIP, Pri)" TableName="LoginUsers">
                    </asp:LinqDataSource>
                </form>
            </div>
        </div>
    </div>

</asp:Content>

