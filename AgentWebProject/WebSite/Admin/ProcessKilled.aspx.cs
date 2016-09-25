using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class Admin_ProcessKilled : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Calendar1.SelectedDate = DateTime.Today;
            Calendar2.SelectedDate = DateTime.Today;
            DoDataBind();
        }
        if (Request.QueryString["action"] != null && Request.QueryString["action"] == "today")
        {
            Calendar1.SelectedDate = DateTime.Today;
            Calendar2.SelectedDate = DateTime.Today;
            DoDataBind();
            Panel1.Visible = false;
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DoDataBind();
    }

    protected void DoDataBind()
    {
        var data = new ProgramBlackListMgr().GetProcessKillerLog(Calendar1.SelectedDate.Date,
                                                                 Calendar2.SelectedDate.AddHours(23).
                                                                     AddMinutes(59).AddSeconds(59));
        GridView1.DataSource = data;
        GridView1.DataBind();
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        DoDataBind();
    }
}