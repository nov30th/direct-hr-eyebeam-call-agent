using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    static public class UserStatus
    {
        private static List<string> _clickedNumbers = new List<string>();
        private static string _userStatusUri = "http://8.8.8.8?";

        static public void AddClickedNumber(string number)
        {
            _clickedNumbers.Add(number);
        }

        static public void UploadUserStatus()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (_clickedNumbers.Count == 0)
                    continue;
                try
                {
                    string clickedNumber = _clickedNumbers[0];
                    _clickedNumbers.Clear();
                    WebClient wc = new WebClient();
                    Stream stream = wc.OpenRead(string.Format("{0}?t={1}&clickedNumber={2}", _userStatusUri, new Random().NextDouble().ToString(), clickedNumber));
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(stream);
                    XmlNode statusCode = xmlDoc.SelectSingleNode("Status");
                    if (statusCode.InnerText != "200")
                    {
                        //not 200, error happens.
                        AgentLogs.WriteLog("USERSTATUS RESPONSE ERROR： " + statusCode.InnerText, AgentLogEventLevel.Errors);
                    }
                }
                catch (Exception e)
                {
                    new AgentException("USERSTATUS FUNCTION ERROR: ", e);
                }
            }
        }
    }
}