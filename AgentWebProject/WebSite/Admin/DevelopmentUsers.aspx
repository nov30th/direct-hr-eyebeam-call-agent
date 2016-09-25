<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="DevelopmentUsers.aspx.cs" Inherits="Admin_DevelopmentUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Development Users</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>These users will IGNORE all blocking services.</h3>
            <div class="clear">
            </div>
        </div>




        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">
                <form id="form1" runat="server">
                    <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource2" DataKeyNames="DevelopmentUsers_Id" InsertItemPosition="LastItem" OnItemInserting="ListView1_ItemInserting" OnItemUpdating="ListView1_ItemUpdating">
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentUsers_IdLabel" runat="server" Text='<%# Eval("DevelopmentUsers_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentNameLabel" runat="server" Text='<%# Eval("DevelopmentName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentExtLabel" runat="server" Text='<%# Eval("DevelopmentExt") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CreatedAtLabel" runat="server" Text='<%# Eval("CreatedAt") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsIgnoreAllRulesCheckBox" runat="server" Checked='<%# Eval("IsIgnoreAllRules") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsAdminCheckBox" runat="server" Checked='<%# Eval("IsAdmin") %>' Enabled="false" />
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
                                    <asp:Label ID="DevelopmentUsers_IdLabel1" runat="server" Text='<%# Eval("DevelopmentUsers_Id") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="DevelopmentNameTextBox" runat="server" Text='<%# Bind("DevelopmentName") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="DevelopmentExtTextBox" runat="server" Text='<%# Bind("DevelopmentExt") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CreatedAtTextBox" runat="server" Text='<%# Bind("CreatedAt") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsIgnoreAllRulesCheckBox" runat="server" Checked='<%# Bind("IsIgnoreAllRules") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsAdminCheckBox" runat="server" Checked='<%# Bind("IsAdmin") %>' />
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
                                    <asp:TextBox ID="DevelopmentNameTextBox" runat="server" Text='<%# Bind("DevelopmentName") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="DevelopmentExtTextBox" runat="server" Text='<%# Bind("DevelopmentExt") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CreatedAtTextBox" runat="server" Text='<%# Bind("CreatedAt") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsIgnoreAllRulesCheckBox" runat="server" Checked='<%# Bind("IsIgnoreAllRules") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsAdminCheckBox" runat="server" Checked='<%# Bind("IsAdmin") %>' />
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
                                    <asp:Label ID="DevelopmentUsers_IdLabel" runat="server" Text='<%# Eval("DevelopmentUsers_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentNameLabel" runat="server" Text='<%# Eval("DevelopmentName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentExtLabel" runat="server" Text='<%# Eval("DevelopmentExt") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CreatedAtLabel" runat="server" Text='<%# Eval("CreatedAt") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsIgnoreAllRulesCheckBox" runat="server" Checked='<%# Eval("IsIgnoreAllRules") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsAdminCheckBox" runat="server" Checked='<%# Eval("IsAdmin") %>' Enabled="false" />
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
                                                <th runat="server">DevelopmentUsers_Id</th>
                                                <th runat="server">DevelopmentName</th>
                                                <th runat="server">DevelopmentExt</th>
                                                <th runat="server">CreatedAt</th>
                                                <th runat="server">IsIgnoreAllRules</th>
                                                <th runat="server">IsAdmin</th>
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
                                    <asp:Label ID="DevelopmentUsers_IdLabel" runat="server" Text='<%# Eval("DevelopmentUsers_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentNameLabel" runat="server" Text='<%# Eval("DevelopmentName") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="DevelopmentExtLabel" runat="server" Text='<%# Eval("DevelopmentExt") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CreatedAtLabel" runat="server" Text='<%# Eval("CreatedAt") %>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsIgnoreAllRulesCheckBox" runat="server" Checked='<%# Eval("IsIgnoreAllRules") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsAdminCheckBox" runat="server" Checked='<%# Eval("IsAdmin") %>' Enabled="false" />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="DhrAgentDatabaseUtils.DBConn" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="DevelopmentUsers">
                    </asp:LinqDataSource>
                </form>
            </div>
        </div>
    </div>

</asp:Content>

