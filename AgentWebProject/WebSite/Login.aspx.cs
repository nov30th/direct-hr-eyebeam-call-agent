using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using DhrAgentDatabaseUtils;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ip = string.IsNullOrEmpty(this.Context.Request.Headers["X-Real-IP"])
                ? this.Context.Request.UserHostAddress
                : this.Context.Request.Headers["X-Real-IP"];
        if (Session[SessionStrings.Username] != null && !string.IsNullOrEmpty(Session[SessionStrings.Username].ToString()))
        {
            Response.Redirect("Admin/");
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.Form["password"]) && !string.IsNullOrEmpty(Request.Form["username"]))
            {
                var account = new LoginMgr();
                var result = account.Login(Request.Form["username"].ToString().ToUpper(), Request.Form["password"].ToString(), ip);
                if (result)
                {
                    Session[SessionStrings.Username] = Request.Form["username"].ToString().ToUpper();
                    Response.Redirect("Admin/");
                }
                else
                {
                    //do nothing
                }

            }

            //    string.IsNullOrEmpty(Request.Form["password"]))
            //    return;
            //Session[SessionStrings.Username] = Request.Form["username"];

        }
    }
}