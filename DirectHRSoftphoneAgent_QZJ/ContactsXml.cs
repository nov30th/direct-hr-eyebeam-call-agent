using DHRSoftphone.AgentExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{

    public class UserContact
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Photo { get; set; }
        public string BigPhoto { get; set; }
        public string Office { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string MasterEmail { get; set; }
    }


    public class ContactsXml
    {
        protected const string url = @"http://newtools.directhr.net/extension";

        private string getHtml(string url)
        {
            WebClient myWebClient = new WebClient();
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            return Encoding.Default.GetString(myDataBuffer);
        }

        
        public List<UserContact> GetData()
        {
            //string username = new AgentSettings().GetFullname();
            List<UserContact> userContacts = new List<UserContact>();
            try
            {
                var httpContent =
                    getHtml(url + "?" + DateTime.UtcNow.ToString()).Replace("<?xml version=\"1.0\" ?>\r\n", string.Empty);

                //var meetingsContent = XDocument.Load(@"http://meeting.directhr.cn/demo/demo4_helper.jsp");
                //XmlDocument xDom = new XmlDocument();
                //xDom.LoadXml(httpContent);
                // var meetings = from p in meetingsContent.Descendants("meeting")

                using (StringReader sr = new StringReader(httpContent))
                {
                    //using (StringReader sr = new StringReader(xml))
                    //{
                    XDocument doc = XDocument.Load(sr);

                    foreach (var m in doc.Descendants("Extensions"))
                    {
                        List<string> users = new List<string>();
                        foreach (var u in m.Descendants("Member"))
                            userContacts.Add(new UserContact()
                       {
                           Name = u.Attribute("Name").Value,
                           Extension = u.Attribute("Extension").Value,
                           Office = u.Attribute("Office").Value,
                           Cellphone = u.Attribute("Cellphone").Value,
                           Photo = u.Attribute("Photo").Value,
                           BigPhoto = u.Attribute("BigPhoto").Value,
                           Email = u.Attribute("Email").Value,
                           MasterEmail = u.Attribute("MasterEmail").Value
                       });
                    }
                }
                return userContacts;
            }
            catch (Exception ex)
            {
                new AgentException("Get Meeting Data Failed!", ex);
                AgentLogs.WriteLog("Get Meeting Data Failed!", AgentLogEventLevel.Errors);
            }
            return null;
        }
    }
}
