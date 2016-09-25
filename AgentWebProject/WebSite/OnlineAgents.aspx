<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineAgents.aspx.cs" Inherits="OnlineAgents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Login_Id" HeaderText="Login_Id" InsertVisible="False"
                    SortExpression="Login_Id" />
                <asp:BoundField DataField="IPAddress" HeaderText="IPAddress" SortExpression="IPAddress" />
                <asp:BoundField DataField="ComputerName" HeaderText="ComputerName" SortExpression="ComputerName" />
                <asp:BoundField DataField="CityName" HeaderText="CityName" SortExpression="CityName" />
                <asp:BoundField DataField="AreaCode" HeaderText="AreaCode" SortExpression="AreaCode" />
                <asp:BoundField DataField="ExtensionNumber" HeaderText="ExtensionNumber" SortExpression="ExtensionNumber" />
                <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName" />
                <asp:BoundField DataField="CreateAt" HeaderText="CreateAt" SortExpression="CreateAt" />
                <asp:BoundField DataField="LastModifiedAt" HeaderText="LastModifiedAt" SortExpression="LastModifiedAt" />
                <asp:BoundField DataField="MACAddress" HeaderText="MACAddress" SortExpression="MACAddress" />
                <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
