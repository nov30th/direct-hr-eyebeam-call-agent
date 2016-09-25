using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DHRSoftphone.IDHRSPPlugin
{
    public class AgentPluginLogArgs
    {
        public AgentPluginLogEventLevel LogLevel { get; set; }

        public string Messages { get; set; }
    }

    public class AgentPluginConfArgs
    {
        public string Section { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    /// <summary>
    /// Log level
    /// </summary>
    [Flags]
    public enum AgentPluginLogEventLevel
    {
        All,
        Details,
        Info,
        Warnings,
        Errors
    }

    public class PluginObject
    {
        public string PluginName { get; set; }

        public string PluginVersion { get; set; }

        public delegate void WriteLog(object sender, AgentPluginLogArgs e);

        public delegate void ShowMessages(object sender, string e);

        public delegate void WriteConfiguration(object sender, AgentPluginConfArgs e);

        public event ShowMessages ShowMessagesWindow;

        public event WriteLog WriteLogToFile;

        public event WriteConfiguration WriteConfToFile;

        public void OnWriteConfToFile(AgentPluginConfArgs e)
        {
            WriteConfiguration handler = WriteConfToFile;
            if (handler != null) handler(this, e);
        }

        public void Output(string message)
        {
            OnWriteLogToFile(new AgentPluginLogArgs() { LogLevel = AgentPluginLogEventLevel.Info, Messages = message });
        }

        public void OnWriteLogToFile(AgentPluginLogArgs e)
        {
            WriteLog handler = WriteLogToFile;
            if (handler != null) handler(this, e);
        }

        public void OnShowMessagesWindow(string e)
        {
            ShowMessages handler = ShowMessagesWindow;
            if (handler != null) handler(this, e);
        }

        public string Upload(Dictionary<string, string> settings, string UploadURI)
        {
            string fullname;
            string extensionnumber;
            string version;
            string cityname;
            string areacode;
            string responseData = string.Empty;


            bool isFullname = settings.TryGetValue("FULLNAME", out fullname);
            bool isExtensionnumber = settings.TryGetValue("EXTENSIONNUMBER", out extensionnumber);
            bool isVersion = settings.TryGetValue("VERSION", out version);
            bool isCityname = settings.TryGetValue("CITYNAME", out cityname);
            bool isAreacode = settings.TryGetValue("AREACODE", out areacode);

            var postdata = "fullname=" + fullname +
                           "&extensionnumber=" + extensionnumber +
                           "&verison=" + version +
                           "&computername=" + System.Environment.MachineName +
                           "&areacode=" + areacode +
                           "&cityname=" + cityname +
                           "&dhrfilterversion=00000000";



            HttpWebRequest req = WebRequest.Create(UploadURI) as HttpWebRequest;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.UserAgent = "DirectHR Softphone Agent [V" + version + "]";

            //write the post values
            byte[] buffer = System.Text.ASCIIEncoding.UTF8.GetBytes(postdata);
            req.ContentLength = buffer.Length;
            using (Stream writer = req.GetRequestStream())
            {
                writer.Write(buffer, 0, buffer.Length);
            }

            //get the response
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            using (Stream receiveStream = resp.GetResponseStream())
            {
                Encoding encode = Encoding.GetEncoding("utf-8");
                using (StreamReader readStream = new StreamReader(receiveStream, encode))
                {
                    responseData = readStream.ReadToEnd();
                }
            }
            resp.Close();
            return responseData;
        }
    }
}