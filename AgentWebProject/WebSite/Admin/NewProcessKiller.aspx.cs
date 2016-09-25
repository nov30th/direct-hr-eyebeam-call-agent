using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NewProcessKiller : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DetailsView1.ChangeMode(DetailsViewMode.Insert);
    }
    protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DetailsView1.InsertItem(true);
    }
}