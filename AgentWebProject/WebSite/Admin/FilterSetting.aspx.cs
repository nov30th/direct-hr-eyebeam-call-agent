using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FilterSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ListView1_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        if (e.Values["OfficeLocation"] == null)
            e.Values["OfficeLocation"] = string.Empty;
        if (e.Values["MemberNames"] == null)
            e.Values["MemberNames"] = string.Empty;
        if (e.Values["ExceptionUsers"] == null)
            e.Values["ExceptionUsers"] = string.Empty;
        if (e.Values["Pri"] == null)
            e.Values["Pri"] = 0;
        e.Values["ExceptionUsers"] = e.Values["ExceptionUsers"].ToString().Replace(";", ",");
        if (!string.IsNullOrEmpty(e.Values["ExceptionUsers"].ToString()))
        {
            if (!e.Values["ExceptionUsers"].ToString().StartsWith(","))
                e.Values["ExceptionUsers"] = "," + e.Values["ExceptionUsers"].ToString();
            if (!e.Values["ExceptionUsers"].ToString().EndsWith(","))
                e.Values["ExceptionUsers"] = e.Values["ExceptionUsers"].ToString() + ",";
        }
    }
    protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        if (e.NewValues["OfficeLocation"] == null)
            e.NewValues["OfficeLocation"] = string.Empty;
        if (e.NewValues["MemberNames"] == null)
            e.NewValues["MemberNames"] = string.Empty;
        if (e.NewValues["ExceptionUsers"] == null)
            e.NewValues["ExceptionUsers"] = string.Empty;
        if (e.NewValues["Pri"] == null)
            e.NewValues["Pri"] = 0;
        e.NewValues["ExceptionUsers"] = e.NewValues["ExceptionUsers"].ToString().Replace(";", ",");
        if (!string.IsNullOrEmpty(e.NewValues["ExceptionUsers"].ToString()))
        {
            if (!e.NewValues["ExceptionUsers"].ToString().StartsWith(","))
                e.NewValues["ExceptionUsers"] = "," + e.NewValues["ExceptionUsers"].ToString();
            if (!e.NewValues["ExceptionUsers"].ToString().EndsWith(","))
                e.NewValues["ExceptionUsers"] = e.NewValues["ExceptionUsers"].ToString() + ",";
        }
    }
}