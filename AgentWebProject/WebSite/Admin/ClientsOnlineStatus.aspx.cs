using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;


public partial class Admin_ClientsOnlineStatus : System.Web.UI.Page
{
    protected long systemTime;

    /// <summary>
    /// SortExpression of GridView
    /// </summary>
    public string GridViewSortExpression
    {
        get
        {
            return ViewState["GridViewSortExpression"] == null ? "Name" : ViewState["GridViewSortExpression"] as string;
        }
        set
        {
            ViewState["GridViewSortExpression"] = value;
        }
    }

    /// <summary>
    /// for Sorting Direction
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Calendar1.SelectedDate > DateTime.MinValue)
        {
            systemTime = new LoginRecordMgr().GetSystemOnlineTime(Calendar1.SelectedDate);
        }
        ViewState["systemTime"] = ((int)(systemTime / 60)).ToString("d") + "h" + ((int)(systemTime % 60)).ToString("d") + "m";
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        const int NameCol = 2;
        const int MinsCol = 3;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //doing data coloring
            var developmentUsers = (new DevelopmentMgr()).GetAllDevelopmentUsersName();
            var label = (Label)e.Row.Cells[MinsCol].Controls[1];
            long rowMins = Convert.ToInt64(label.Text);
            var Name = ((Label)e.Row.Cells[NameCol].Controls[1]).Text;
            label.Text = ((int)(rowMins / 60)).ToString("d") + "h" + ((int)(rowMins % 60)).ToString("d") + "m";
            if (rowMins + 10 >= systemTime)
                rowMins = systemTime;


            if (systemTime > 0)
            {
                if (rowMins == 0)
                {
                    //never online
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }
                else if (rowMins * 1.0 / systemTime > 0.95)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.GreenYellow;
                }
                else if (rowMins * 1.0 / systemTime < 0.5)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }
                else if (rowMins * 1.0 / systemTime < 0.9)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Crimson;
                }
                else
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.DeepPink;
                }
                label.Text += " (" + (rowMins * 100 / systemTime).ToString("d") + "%)";
            }

            if (developmentUsers.Contains(Name))
                e.Row.Cells[0].BackColor = System.Drawing.Color.Chartreuse;
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
        };
        BindGrid();

    }

    protected void BindGrid(bool isHideDevelopmentUsers = false)
    {
        if (GridViewSortExpression != "Name")
            isHideDevelopmentUsers = true;
        var data = new LoginRecordMgr().GetAllOnlineStatus(Calendar1.SelectedDate, isHideDevelopmentUsers);
        if (data != null && data.Count > 0)
        {
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridView1.DataSource = data.OrderBy(x => x.GetType().GetProperty(GridViewSortExpression).GetValue(x, null));
            }
            else
            {
                GridView1.DataSource = data.OrderByDescending(x => x.GetType().GetProperty(GridViewSortExpression).GetValue(x, null));
            };
        }
        else
        {
            GridView1.DataSource = null;
        }
        GridView1.DataBind();
        if (GridViewSortExpression != "Name")
        {
            lab_sortinghidden.Text = ("Sorting by " + ViewState["GridViewSortExpression"].ToString() + ", developoment users were hidden.");
        }
        else
        {
            lab_sortinghidden.Text = string.Empty;
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        systemTime = new LoginRecordMgr().GetSystemOnlineTime(Calendar1.SelectedDate);

        BindGrid();
    }
}