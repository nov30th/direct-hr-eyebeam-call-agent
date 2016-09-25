<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LoginTemplate.master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>User Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="login-wrapper" class="png_bg">
        <div id="login-top">
            <h1>
                Softphone Agent Information Center</h1>
            <!-- Logo (221px width) -->
            <img id="logo" src="/resources/images/logo.png" alt="Simpla Admin logo" />
        </div>
        <!-- End #logn-top -->
        <div id="login-content">
            <form id="form1" method="post">
            <div class="notification information png_bg">
                <div>
                    Please enter the password and click Sign in.
                </div>
            </div>
            <p>
                <label>
                    Username</label>
                <input class="text-input" type="text" name="username" value = "Chun Liew"/>
            </p>
            <div class="clear">
            </div>
            <p>
                <label>
                    Password</label>
                <input class="text-input" name="password" type="password" />
            </p>
            <div class="clear">
            </div>
            <p id="remember-password">
                <input type="checkbox" />
                Remember me
            </p>
            <div class="clear">
            </div>
            <p>
                <input class="button" type="submit" value="Sign In" />
            </p>
            </form>
        </div>
        <!-- End #login-content -->
    </div>
    <!-- End #login-wrapper -->
</asp:Content>
