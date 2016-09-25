using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DhrAgentDatabaseUtils;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RecordMeetings target = new RecordMeetings(); // TODO: Initialize to an appropriate value
      //  target.RecordMeetingUserStatus();
        target.FlagExpiredUserStatus();
    }
}