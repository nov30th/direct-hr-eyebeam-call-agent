using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class Admin_DevelopmentUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ListView1_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        e.Values["CreatedAt"] = DateTime.Now;
        for (int i = 0; i < e.Values.Count; i++)
        {
            if (e.Values[i] == null)
                e.Values[i] = string.Empty;
        }
    }
    protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i] == null)
                e.NewValues[i] = string.Empty;
        }
    }
}