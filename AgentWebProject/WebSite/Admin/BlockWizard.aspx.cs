using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class Admin_BlockWizard : System.Web.UI.Page
{

    static string domain = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        domain = string.Empty;
        _txtDomainName.Text = string.Empty;
        _websiteKeywords.Text = string.Empty;
        _chkBlockWindowTitle.Checked = false;
    }
    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.CurrentStepIndex)
        {
            case 0:
                DoStep1();
                break;
            case 1:
                DoStep2();
                break;
        }


    }

    public void DoStep2()
    {
        var subdomain = domain;
        var mainDomain = domain;
        string outMessage = string.Empty;
        if (string.IsNullOrEmpty(domain))
            return;
        if (domain.IndexOf("www.") == 0)
        {
            mainDomain = domain.Replace("www.", string.Empty);
        }
        DBConn db = new DBConn();

        if (_chkBlockHosts.Checked)
        {
            var hostsContent = db.Hosts.Single(r => r.Hosts_Id == 11);
            if (hostsContent.HostsContent.Contains("\r\n" + mainDomain + " "))
            {
                outMessage = mainDomain + " already in hosts content<br />";
            }
            else
            {
                hostsContent.HostsContent += "\r\n" + mainDomain + " 222.73.42.130";
                outMessage = mainDomain + " adding to hosts content...<br />";
            }

            if (subdomain != mainDomain)
                if (hostsContent.HostsContent.Contains("\r\n" + subdomain + " "))
                {
                    outMessage += subdomain + " already in hosts content<br />";
                }
                else
                {
                    hostsContent.HostsContent += "\r\n" + subdomain + " 222.73.42.130";
                    outMessage += subdomain + " adding to hosts content...<br />";
                }

            db.SubmitChanges();
        }

        if (_chkBlockNetFilter.Checked)
        {
            var filterContent = db.NetworkFilters.Single(r => r.Filter_Id == 3);
            if (filterContent.FilterContent.Contains("|" + mainDomain + "|"))
            {
                outMessage += mainDomain + " already in Network Filter content<br />";
            }
            else
            {
                filterContent.FilterContent += "\r\n" + "[DHRNF_QZJ|funBlocking|ALL|CONT|HTTP|" + mainDomain + "|\\0|]";
                outMessage += subdomain + " adding to network filter content...<br />";
            }
            db.SubmitChanges();
        }

        if (_chkBlockWindowTitle.Checked && _websiteKeywords.Text != string.Empty)
        {
            var websiteKeyword = _websiteKeywords.Text;
            var isExisted = db.ProgramBlackLists.Count(r => r.WindowName.Contains(websiteKeyword));
            if (isExisted > 0)
            {
                outMessage += websiteKeyword + " already in Window Title Blocker<br />";
            }
            else
            {
                outMessage += websiteKeyword + " adding to Window Title Blocker<br />";
                var newBlocker = new ProgramBlackList()
                {
                    ApplicationName = websiteKeyword,
                    ExceptionUsers = ";",
                    FileName = websiteKeyword,
                    IsMustBeSame = false,
                    MatchFileName = false,
                    MatchWindowName = true,
                    Memo = "added from webside blocker wizard",
                    WindowName = websiteKeyword
                };
                db.ProgramBlackLists.InsertOnSubmit(newBlocker);
                db.SubmitChanges();
            }
        }
        _labResult.Text = outMessage;
    }

    public void DoStep1()
    {
        _chkBlockHosts.Checked = true;
        domain = _txtDomainName.Text.ToLower();
        domain = domain.Replace("http://", string.Empty).Replace("https://", string.Empty)
            .Replace("/", string.Empty);
        _domainWillBeBlocked.Text = domain;
        if (domain.IndexOf("www.") == 0)
        {
            _chkBlockNetFilter.Checked = true;
            _domainWillBeBlocked.Text += " and " + domain.Replace("www.", string.Empty);
        }
    }

    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStepIndex >= 1 && string.IsNullOrEmpty(domain))
            Wizard1.ActiveStepIndex = 0;
    }
}