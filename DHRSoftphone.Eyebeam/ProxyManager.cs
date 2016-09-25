using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DHRSoftphone.Eyebeam
{
    public class ProxyManager
    {
        public List<SIPUser> LoadUsersFromXml(string content)
        {
            List<SIPUser> sipusers = new List<SIPUser>();
            using (var sr = new StringReader(content))
            {
                content = content//.Replace(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>", string.Empty)
                    .Replace(@" version=""1.0"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://www.counterpath.com/cps"">",">");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                XmlNode nodes = doc.SelectSingleNode("/settings/domain[@name='proxies']");
                if (nodes == null)
                    return null;

                foreach (XmlNode m in nodes.ChildNodes)
                {
                    if (m.Attributes["name"] != null && m.Attributes["name"].Value == "proxy0")
                    {

                        try
                        {
                            var sipuser = new SIPUser()
                                                              {
                                                                  DisplayName =
                                                                      m.SelectSingleNode(@"setting[@name='display_name']").Attributes[
                                                                          "value"].Value,
                                                                  Domain =
                                                                      m.SelectSingleNode(@"setting[@name='domain']").Attributes["value"].
                                                                      Value,
                                                                  Enabled =
                                                                      m.SelectSingleNode(@"setting[@name='enabled']").Attributes["value"].
                                                                          Value == "1"
                                                                          ? true
                                                                          : false,
                                                                  Extension =
                                                                      m.SelectSingleNode(@"setting[@name='authorization_username']").
                                                                      Attributes["value"].Value,
                                                                  Password =
                                                                      m.SelectSingleNode(@"setting[@name='password']").Attributes["value"].
                                                                      Value,
                                                                  Username =
                                                                      m.SelectSingleNode(@"setting[@name='username']").Attributes["value"].
                                                                      Value
                                                              };
                            sipusers.Add(sipuser);
                        }
                        catch (Exception exception)
                        {
                            //error when reading configuration file.
                        }
                    }
                }
            }
            return sipusers;
        }
    }
}
