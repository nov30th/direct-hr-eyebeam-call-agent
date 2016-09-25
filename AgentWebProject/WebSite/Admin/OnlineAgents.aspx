<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master"
    AutoEventWireup="true" CodeFile="OnlineAgents.aspx.cs" Inherits="Admin_OnlineAgents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>
                Online Agent Clients</h3>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">
                <form id="form1" runat="server">
                <div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="IPAddress" HeaderText="IPAddress" SortExpression="IPAddress" />
                            <asp:BoundField DataField="ComputerName" HeaderText="ComputerName" SortExpression="ComputerName" />
                            <asp:BoundField DataField="CityName" HeaderText="CityName" SortExpression="CityName" />
                            <asp:BoundField DataField="AreaCode" HeaderText="AreaCode" SortExpression="AreaCode" />
                            <asp:BoundField DataField="ExtensionNumber" HeaderText="ExtensionNumber" SortExpression="ExtensionNumber" />
                            <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName" />
                            <asp:BoundField DataField="CreateAt" HeaderText="CreateAt" SortExpression="CreateAt" />
                            <asp:BoundField DataField="LastModifiedAt" HeaderText="LastModifiedAt" SortExpression="LastModifiedAt" />
                            <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version" />
                            <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Data
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <br />
                Offline OR Softphone Agent Not Installed:<br />
                <asp:BulletedList ID="OfflineBulletedList" runat="server">
                </asp:BulletedList>
                <br />
                Extension OR Name Error Member(s): (Compare data with newtools)<br />
                <asp:BulletedList ID="ErrorBulletedList" runat="server">
                </asp:BulletedList>
                <br />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
