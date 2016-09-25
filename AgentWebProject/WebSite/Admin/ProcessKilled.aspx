<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master"
    AutoEventWireup="true" CodeFile="ProcessKilled.aspx.cs" Inherits="Admin_ProcessKilled" %>

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
                                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged">
                                </asp:Calendar>
                            </td>
                            <td>
                                <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged">
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
                Process Killed
            </h3>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="div2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ProcessKillerLogId" HeaderText="ProcessKillerLogId" ReadOnly="True" />
                        <asp:BoundField DataField="KilledAt" HeaderText="KilledAt" ReadOnly="True" SortExpression="KilledAt" />
                        <asp:BoundField DataField="FullName" HeaderText="FullName" ReadOnly="True" SortExpression="FullName" />
                        <asp:BoundField DataField="ComputerName" HeaderText="ComputerName" ReadOnly="True"
                            SortExpression="ComputerName" />
                        <asp:BoundField DataField="KeyWords" HeaderText="KeyWords" ReadOnly="True" SortExpression="KeyWords" />
                        <asp:BoundField DataField="WindowsName" HeaderText="WindowsName" ReadOnly="True"
                            SortExpression="WindowsName" />
                        <asp:TemplateField HeaderText="ProcessName" SortExpression="ProcessName">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" ToolTip='<%# Eval("ProcessPath") %>' runat="server" Text='<%# Eval("ProcessName") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" ToolTip='<%# Eval("ProcessPath") %>' runat="server" Text='<%# Bind("ProcessName") %>'></asp:Label>
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
