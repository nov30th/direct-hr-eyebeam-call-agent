<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminTemplate.master" AutoEventWireup="true" CodeFile="BlockWizard.aspx.cs" Inherits="Admin_BlockWizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Website/Domain Block Wizard</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="form1" runat="server">
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Height="480px" OnFinishButtonClick="Wizard1_FinishButtonClick" OnNextButtonClick="Wizard1_NextButtonClick" Width="629px" OnActiveStepChanged="Wizard1_ActiveStepChanged">
            <WizardSteps>
                <asp:WizardStep runat="server" title="Step 1">
                    Domain Name:<asp:TextBox ID="_txtDomainName" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    Please enter the domain, e.g. <strong>www.sina.com.cn</strong> with www<br />If you willing to block a subdomain, please enter the subdomain, e.g. <strong>video.sina.com.cn</strong><br />
                    <br />
                    Enter the domain with www is strong recommend, Network Filter can block all the subdomains of the main domian. (e.g. when you enabled Network Filter for www.sina.com.cn, *.sina.com.cn will be all blocked).<br />if you entered a subdomain (e.g. video.sina.com.cn), please not block it in Network Filter.
                    <br />
                    <br />
                    This wizard is simply to use but only 30% function of whole blocker system.
                </asp:WizardStep>
                <asp:WizardStep runat="server" title="Step 2">
                    Following domain will be blocked:<br />
                    <asp:Label ID="_domainWillBeBlocked" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:CheckBox ID="_chkBlockHosts" runat="server" Text="Block the website(s) in hosts file." />
                    <asp:CheckBox ID="_chkBlockNetFilter" runat="server" Text="Block the website(s) in Network Filter." />

                    <br />
                    <br />
                    <asp:CheckBox ID="_chkBlockWindowTitle" runat="server" Text="Close whole browser if the Site Window Title has been found."/>
                                        <br />
                    Website Window Title:<asp:TextBox ID="_websiteKeywords" runat="server"></asp:TextBox>
                    <br />
                    (You can type the website window title into the textbox above if you selected &quot;Close whole ...&quot;, please notice that you can just type a part of the Window title, but the title must be the website shows in browsers. And DO NOT type common words, otherwise the people who browsing a website which its title contains the words you typed will be close.)
                </asp:WizardStep>
                <asp:WizardStep runat="server" AllowReturn="False" StepType="Finish" Title="Finish">
                    Result:<br />
                    <asp:Label ID="_labResult" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </form>

</asp:Content>

