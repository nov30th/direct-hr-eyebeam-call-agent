using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class Admin_OnlineAgents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<DhrAgentDatabaseUtils.LoginStatus> loginstatus = (new LoginRecordMgr()).GetOnlineStatusOrderByFullname().ToList();
        GridView1.DataSource = loginstatus;
        GridView1.DataBind();

        var memberManager = new MemberManager();
        var allmembers = memberManager.GetMembers();
        //var offlineMembers = new List<string>();
        var offlineMembersDir = new Dictionary<string, string>();
        //var errorMemebers = new List<string>();
        foreach (var dhrMember in allmembers)
        {
            var inLoginStatus =
                loginstatus.FirstOrDefault(l => l.FullName.ToUpper() == dhrMember.Fullname.ToUpper()
                    || l.ExtensionNumber == dhrMember.ExtensionNumber);
            {
                if (inLoginStatus == null)
                {
                    //not found, offline clients
                    //OfflineBulletedList.Items.Add(new ListItem(
                    //    dhrMember.Fullname, dhrMember.ExtensionNumber
                    //    ));
                    offlineMembersDir.Add(dhrMember.Fullname, dhrMember.ExtensionNumber ?? string.Empty);
                    //ErrorBulletedList.DataBind();
                }
                else if (inLoginStatus.ExtensionNumber == dhrMember.ExtensionNumber && inLoginStatus.FullName == dhrMember.Fullname)
                    continue;//matchl, no problem
                else
                {
                    ErrorBulletedList.Items.Add(new ListItem(
                        "Correct: " + dhrMember.Fullname + " - " + dhrMember.ExtensionNumber +
                        " Current: " + inLoginStatus.FullName + " - " + inLoginStatus.ExtensionNumber, dhrMember.ExtensionNumber
                        ));
                    //ErrorBulletedList.DataBind();
                }
            }
        }
        var offlineMemebers = memberManager.GetOfflineTimeString(offlineMembersDir);
        foreach (var offlineMember in offlineMemebers)
            OfflineBulletedList.Items.Add(new ListItem(offlineMember.Key + " :  " + offlineMember.Value));
    }
}