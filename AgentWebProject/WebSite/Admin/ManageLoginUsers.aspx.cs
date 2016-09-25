using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class Admin_ManageLoginUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_changepwd_Click(object sender, EventArgs e)
    {
        string ip = string.IsNullOrEmpty(this.Context.Request.Headers["X-Real-IP"])
        ? this.Context.Request.UserHostAddress
        : this.Context.Request.Headers["X-Real-IP"];
        var account = new LoginMgr();
        var result = account.ChangePassword(txt_change_username.Text, txt_change_oldpwd.Text, txt_change_newpwd.Text, ip);
        if (result)
            lab_changepassword.Text = "Success!";
        else
            lab_changepassword.Text = "Password change failed, please verify the info you input";
    }
    protected void btn_createuser_Click(object sender, EventArgs e)
    {
        string ip = string.IsNullOrEmpty(this.Context.Request.Headers["X-Real-IP"])
        ? this.Context.Request.UserHostAddress
        : this.Context.Request.Headers["X-Real-IP"];
        var account = new LoginMgr();
        var result = account.CreateUser(txt_new_username.Text, txt_new_newpwd.Text, ip);
        if (result)
            lab_newuser.Text = "Success!";
        else
            lab_newuser.Text = "Failed!";
    }
}