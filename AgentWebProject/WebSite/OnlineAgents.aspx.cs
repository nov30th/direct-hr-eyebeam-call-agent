using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class OnlineAgents : System.Web.UI.Page
{
    protected void Page_PreLoad(object sender, EventArgs e)
    {
        List<DhrAgentDatabaseUtils.LoginStatus> loginstatus = (new LoginRecordMgr()).GetOnlineStatus().ToList();
        GridView1.DataSource = loginstatus;
        GridView1.DataBind();
    }
}