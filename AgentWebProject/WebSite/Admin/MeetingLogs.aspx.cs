using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class Admin_MeetingLogs : System.Web.UI.Page
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
        var meetings = (new RecordMeetings()).GetMeetingLogs(Calendar1.SelectedDate.Date,
                                                                           Calendar2.SelectedDate.AddHours(23).
                                                                               AddMinutes(59).AddSeconds(59)).ToList();
        GridView1.DataSource = meetings;
        GridView1.DataBind();
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        DoDataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].BorderColor = Color.FromName(e.Row.Cells[1].Text);
            e.Row.Cells[1].BorderStyle = BorderStyle.Double;
            e.Row.Cells[1].BorderWidth = 2;
        }


    }
}