using System;

using System.IO;
using System.Web;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    [Flags]
    public enum TelephoneCommands
    {
        Dial,
        PreDial,
        Info,
        Command,
        Url,
        Other
    }

    public class TelephoneEventArg
    {
        public TelephoneCommands Commands { get; set; }

        public string TelephoneNumber { get; set; }

        public string Addations { get; set; }
    }

    public class MyHttpServer : HttpServer
    {
        public delegate void InformationFromGetMethod(object sender, TelephoneEventArg e);

        public delegate void InformationFromPostMethod(object sender, TelephoneEventArg e);

        public event InformationFromGetMethod InformationFromGetMethodChanged;

        public event InformationFromPostMethod InformationFromPostMethodChanged;

        public void OnGotInformation(object sender, TelephoneEventArg e)
        {
            if (InformationFromGetMethodChanged != null)
            {
                InformationFromGetMethodChanged(this, e);
            }
        }

        public void OnPostInformation(object sender, TelephoneEventArg e)
        {
            if (InformationFromPostMethodChanged != null)
            {
                InformationFromPostMethodChanged(sender, e);
            }
        }

        public MyHttpServer(int port)
            : base(port)
        {
        }

        public string GetOutputCSS()
        {
            return @"<style type=""text/css"">" + Environment.NewLine + "body,button, input, select, textarea {" + Environment.NewLine +
"font: 12px/1.5 ‘宋体’,tahoma, Srial, helvetica, sans-serif; }" + Environment.NewLine +
"h1, h2, h3, h4, h5, h6 { font-size: 100%; }" + "</style>";
        }

        public override void handleGETRequest(QzjHttpServer p)
        {
            Console.WriteLine("request: {0}", p.http_url);
            p.writeSuccess();
            p.outputStream.WriteLine("<html><head>");
            p.outputStream.WriteLine(@"<meta name=""directhr_telephone_plugin"" content=""[disabled]"" />");
            p.outputStream.WriteLine(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">");
            p.outputStream.WriteLine(GetOutputCSS());
            p.outputStream.WriteLine("</head><body><h1>Direct HR Softphone Agent Web Server</h1><br />");
            p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString() + "<br />");

            //p.outputStream.WriteLine("url : {0}", p.http_url.Substring(1));
            //p.outputStream.WriteLine("<form method=post action=/form>");
            //p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
            //p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
            //p.outputStream.WriteLine("</form>");

            if (p.http_url.Substring(1).IndexOf("/") <= 0)
                return;

            string command = p.http_url.Substring(1).Substring(0, p.http_url.Substring(1).IndexOf("/") + 1);
            string telephone = p.http_url.Substring(1).Substring(p.http_url.Substring(1).IndexOf("/") + 1);
            telephone = HttpUtility.UrlDecode(telephone).Replace("%20", string.Empty);
            telephone = telephone.Replace(" ", string.Empty).Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);

            command = command.Substring(0, command.Length - 1);
            TelephoneCommands telephoneCommands;

            try
            {
                telephoneCommands = (TelephoneCommands)Enum.Parse(typeof(TelephoneCommands), command);
            }
            catch
            {
                p.outputStream.WriteLine("<strong>Command \"" + command + "\" does not found!</strong>");
                return;
            }

            //get telephone number information
            var telephoneNumber = new TelephoneNumber(telephone);
            telephoneNumber.AnalyzeNumber();

            p.outputStream.WriteLine("<strong>Command:</strong>" + command + "<br />");
            p.outputStream.WriteLine("<strong>Telephone Number:</strong>" + telephone + "<br />");
            p.outputStream.WriteLine("<strong>Telephone Number Location:</strong>" + telephoneNumber.CityName + "<br />");
            p.outputStream.WriteLine("<strong>Telephone Number Area Code:</strong>" + telephoneNumber.AreaCode + "<br />");
            p.outputStream.WriteLine("<strong>Telephone Number Type:</strong>" + telephoneNumber.TelephoneType + "<br />");
            p.outputStream.WriteLine("Edit before call:<input type=\"textbox\" name=\"tel\" id=\"tel\" value=\""
                + telephoneNumber.MakeFinallyNumber(AgentDatabaseProvider.CurrentAreaCode)
                + "\" /><br />");
            p.outputStream.WriteLine("<input type=\"button\" id=\"call\" name=\"call\" text=\"Call this number\" value=\"Call this number\" onclick=\"javascript:document.location='http://127.0.0.1:7069/Dial/' + tel.value\" />");

            OnGotInformation(this, new TelephoneEventArg()
            {
                Commands = telephoneCommands,
                TelephoneNumber = telephone
            });
        }

        public override void handlePOSTRequest(QzjHttpServer p, StreamReader inputData)
        {
            Console.WriteLine("POST request: {0}", p.http_url);
            string data = inputData.ReadToEnd();

            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("<a href=/test>return</a><p>");
            p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
        }

        public override void Exit()
        {
            //m_thread.Abort();
            //Here do the exit steps.
        }
    }
}