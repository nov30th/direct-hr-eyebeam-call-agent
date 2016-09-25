<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="FilterSetting.aspx.cs" Inherits="Admin_FilterSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <title>Filter Content Setting</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>Filters Content Setting</h3>
            <br />
            <strong>Please notice the PRI value large than 99 (not equal) will also apply for development users (if user not ignore all blocking server option checked)!</strong>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">
                <form id="form1" runat="server">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Filter_Id" DataSourceID="LinqDataSource1" InsertItemPosition="LastItem" OnItemInserting="ListView1_ItemInserting" OnItemUpdating="ListView1_ItemUpdating">
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                </td>
                                <td>
                                    <asp:Label ID="Filter_IdLabel" runat="server" Text='<%# Eval("Filter_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="OfficeLocationLabel" runat="server" Text='<%# Eval("OfficeLocation") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MemberNamesLabel" runat="server" Text='<%# Eval("MemberNames") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ExceptionUsersLabel" runat="server" Text='<%# Eval("ExceptionUsers") %>' />
                                </td>
                                <td>
                                    <asp:Label TextMode="MultiLine" ID="FilterContentLabel" runat="server" Text='<%# Eval("FilterContent") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriodLabel" runat="server" Text='<%# Eval("TimePeriod") %>' />
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
                                    <asp:Label ID="Filter_IdLabel1" runat="server" Text='<%# Eval("Filter_Id") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="OfficeLocationTextBox" runat="server" Text='<%# Bind("OfficeLocation") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="MemberNamesTextBox" runat="server" Text='<%# Bind("MemberNames") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ExceptionUsersTextBox" runat="server" Text='<%# Bind("ExceptionUsers") %>' />
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="FilterContentTextBox" runat="server" Text='<%# Bind("FilterContent") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="TimePeriodTextBox" runat="server" Text='<%# Bind("TimePeriod") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="PriTextBox" runat="server" Text='<%# Bind("Pri") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table1" runat="server" style="">
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
                                    <asp:TextBox ID="OfficeLocationTextBox" runat="server" Text='<%# Bind("OfficeLocation") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="MemberNamesTextBox" runat="server" Text='<%# Bind("MemberNames") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ExceptionUsersTextBox" runat="server" Text='<%# Bind("ExceptionUsers") %>' />
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="FilterContentTextBox" runat="server" Text='<%# Bind("FilterContent") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="TimePeriodTextBox" runat="server" Text='<%# Bind("TimePeriod") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="PriTextBox" runat="server" Text='<%# Bind("Pri") %>' />
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
                                    <asp:Label ID="Filter_IdLabel" runat="server" Text='<%# Eval("Filter_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="OfficeLocationLabel" runat="server" Text='<%# Eval("OfficeLocation") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MemberNamesLabel" runat="server" Text='<%# Eval("MemberNames") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ExceptionUsersLabel" runat="server" Text='<%# Eval("ExceptionUsers") %>' />
                                </td>
                                <td>
                                    <asp:Label TextMode="MultiLine" ID="FilterContentLabel" runat="server" Text='<%# Eval("FilterContent") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriodLabel" runat="server" Text='<%# Eval("TimePeriod") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PriLabel" runat="server" Text='<%# Eval("Pri") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table id="Table2" runat="server">
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server">
                                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                            <tr id="Tr2" runat="server" style="">
                                                <th id="Th1" runat="server"></th>
                                                <th id="Th2" runat="server">Filter_Id</th>
                                                <th id="Th3" runat="server">OfficeLocation</th>
                                                <th id="Th4" runat="server">MemberNames</th>
                                                <th id="Th5" runat="server">ExceptionUsers</th>
                                                <th id="Th6" runat="server">FilterContent</th>
                                                <th id="Th7" runat="server">TimePeriod</th>
                                                <th id="Th8" runat="server">Pri</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server">
                                    <td id="Td2" runat="server" style="">
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
                                    <asp:Label ID="Filter_IdLabel" runat="server" Text='<%# Eval("Filter_Id") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="OfficeLocationLabel" runat="server" Text='<%# Eval("OfficeLocation") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MemberNamesLabel" runat="server" Text='<%# Eval("MemberNames") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ExceptionUsersLabel" runat="server" Text='<%# Eval("ExceptionUsers") %>' />
                                </td>
                                <td>
                                    <asp:Label TextMode="MultiLine" ID="FilterContentLabel" runat="server" Text='<%# Eval("FilterContent") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TimePeriodLabel" runat="server" Text='<%# Eval("TimePeriod") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="PriLabel" runat="server" Text='<%# Eval("Pri") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="DhrAgentDatabaseUtils.DBConn" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="NetworkFilters">
                    </asp:LinqDataSource>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

