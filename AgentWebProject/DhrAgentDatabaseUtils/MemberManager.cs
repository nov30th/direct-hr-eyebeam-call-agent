using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace DhrAgentDatabaseUtils
{

    public class DhrMember
    {
        public string Fullname { get; set; }
        public string ExtensionNumber { get; set; }
        public string Cellphone { get; set; }
    }


    public class MemberManager
    {
        private string getHtml(string url)
        {
            WebClient myWebClient = new WebClient();
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            return Encoding.Default.GetString(myDataBuffer);
        }


        public List<DhrMember> GetMembers()
        {
            var httpContent = getHtml("http://newtools.directhr.net/extension");
            using (StringReader sr = new StringReader(httpContent))
            {
                //using (StringReader sr = new StringReader(xml))
                //{

                XDocument doc = XDocument.Load(sr);
                var members = new List<DhrMember>();
                foreach (var m in doc.Descendants("Member"))
                {

                    members.Add(new DhrMember()
                                    {
                                        Fullname = m.Attribute("Name").Value,
                                        Cellphone = m.Attribute("Cellphone").Value,
                                        ExtensionNumber = m.Attribute("Extension").Value
                                    });
                }
                return members;
            }
        }

        public Dictionary<string, string> GetOfflineTimeString(Dictionary<string, string> args)
        {
            DBConn db = new DBConn();
            var retval = new Dictionary<string, string>();
            foreach(var name in args)
            {
                var data = db.LoginStatus.Where(c => name.Key == c.FullName || name.Value == c.ExtensionNumber).OrderByDescending(c=>c.LastModifiedAt).Select(c=>c.LastModifiedAt).FirstOrDefault();
                if (data == DateTime.MinValue)
                    retval.Add(name.Key ,"Never");
                else
                    retval.Add(name.Key ,data.ToString());
            }
            return retval;
        }


    }


}
