<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="ManageBlackList.aspx.cs" Inherits="Admin_ManageBlackList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <!-- Start Content Box -->
        <div class="content-box-header">
            <h3>
                Manage program list</h3>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab" id="tab1">  <form id="form1" runat="server">
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="False" 
    DataKeyNames="BlockApplication_Id" DataSourceID="LinqDataSource1">
    <Columns>
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        <asp:BoundField DataField="BlockApplication_Id" 
            HeaderText="BlockApplication_Id" InsertVisible="False" ReadOnly="True" 
            SortExpression="BlockApplication_Id" />
        <asp:BoundField DataField="ApplicationName" HeaderText="ApplicationName" 
            SortExpression="ApplicationName" />
        <asp:BoundField DataField="WindowName" HeaderText="WindowName" 
            SortExpression="WindowName" />
        <asp:CheckBoxField DataField="IsMustBeSame" HeaderText="IsMustBeSame" 
            SortExpression="IsMustBeSame" />
        <asp:BoundField DataField="FileName" HeaderText="FileName" 
            SortExpression="FileName" />
        <asp:CheckBoxField DataField="MatchFileName" HeaderText="MatchFileName" 
            SortExpression="MatchFileName" />
        <asp:CheckBoxField DataField="MatchWindowName" HeaderText="MatchWindowName" 
            SortExpression="MatchWindowName" />
        <asp:BoundField DataField="ExceptionUsers" HeaderText="ExceptionUsers" 
            SortExpression="ExceptionUsers" />
        <asp:BoundField DataField="Memo" HeaderText="Memo" SortExpression="Memo" />
    </Columns>
</asp:GridView>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" 
    ContextTypeName="DhrAgentDatabaseUtils.DBConn" EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
    TableName="ProgramBlackLists">
</asp:LinqDataSource>
</form>
</div></div></div>
</asp:Content>

