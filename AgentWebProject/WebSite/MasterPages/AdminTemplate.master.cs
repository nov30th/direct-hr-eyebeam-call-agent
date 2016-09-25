using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_AdminTemplate : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session[SessionStrings.Username] == null || string.IsNullOrEmpty(Session[SessionStrings.Username].ToString()))
            Response.Redirect("~/Login.aspx", true);
    }

}
