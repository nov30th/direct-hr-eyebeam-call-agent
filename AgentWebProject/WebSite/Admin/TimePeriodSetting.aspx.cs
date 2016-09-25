using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TimePeriodSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DetailsView1.DefaultMode = DetailsViewMode.Insert;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //DetailsView1.ChangeMode(DetailsViewMode.Insert);
    }
    //protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    //{
    //    for (int i = 0; i < e.Values.Count; i++)
    //    {
    //        if (e.Values[i] == null)
    //            e.Values[i] = string.Empty;
    //    }
    //    if (!e.Values["MemberNames"].ToString().StartsWith(","))
    //        e.Values["MemberNames"] = "," + e.Values["MemberNames"];
    //    if (!e.Values["MemberNames"].ToString().EndsWith(","))
    //        e.Values["MemberNames"] = e.Values["MemberNames"] + ",";
    //    if (!e.Values["ExceptionUsers"].ToString().StartsWith(","))
    //        e.Values["ExceptionUsers"] = "," + e.Values["ExceptionUsers"];
    //    if (!e.Values["ExceptionUsers"].ToString().EndsWith(","))
    //        e.Values["ExceptionUsers"] = e.Values["ExceptionUsers"] + ",";
    //}
}