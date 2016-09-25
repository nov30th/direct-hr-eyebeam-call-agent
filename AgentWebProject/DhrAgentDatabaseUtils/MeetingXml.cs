using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using System.Drawing;
using System.Threading;

namespace DhrAgentDatabaseUtils
{
    public class MeetingXml
    {
        public delegate void MeetingRoomData(object sender, List<Meeting> args);

        public event MeetingRoomData MeetingRoomDataUpdated;

        private const string url = "http://meeting.directhr.cn/demo/demo4_helper.jsp";

        //{0}:User name
        //{1}:Room name
        private const string loginUrl = @"http://meeting.directhr.cn/demo/demo2.jsp?username={0}&meetingID={1}&action=create";

        public Meeting FormatColor(Meeting meeting)
        {
            switch (meeting.Title.ToUpper().Replace(" ROOM", string.Empty))
            {
                case "BLACK":
                    meeting.Color = "BLACK";
                    meeting.BrushColor = Brushes.Black;
                    break;
                case "BLUE":
                    meeting.Color = "BLUE";
                    meeting.BrushColor = Brushes.Blue;
                    break;
                case "BROWN":
                    meeting.Color = "BROWN";
                    meeting.BrushColor = Brushes.Brown;
                    break;
                case "GREEN":
                    meeting.Color = "GREEN";
                    meeting.BrushColor = Brushes.Green;
                    break;
                case "GREY":
                    meeting.Color = "GREY";
                    meeting.BrushColor = Brushes.Gray;
                    break;
                case "ORANGE":
                    meeting.Color = "ORANGE";
                    meeting.BrushColor = Brushes.Orange;
                    break;
                case "PINK":
                    meeting.Color = "PINK";
                    meeting.BrushColor = Brushes.Pink;
                    break;
                case "RED":
                    meeting.Color = "RED";
                    meeting.BrushColor = Brushes.Red;
                    break;
                case "WHITE":
                    meeting.Color = "WHITE";
                    meeting.BrushColor = Brushes.White;
                    break;
                case "YELLOW":
                    meeting.Color = "YELLOW";
                    meeting.BrushColor = Brushes.Yellow;
                    break;
                default:
                    meeting.Color = string.Empty;
                    meeting.BrushColor = Brushes.AliceBlue;
                    break;
            }
            return meeting;
        }


        private string getHtml(string url)
        {
            WebClient myWebClient = new WebClient();
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            return Encoding.Default.GetString(myDataBuffer);
        }

        public void OnMeetingRoomDataUpdated(object sender, List<Meeting> args)
        {
            if (MeetingRoomDataUpdated != null)
                MeetingRoomDataUpdated(sender, args);
        }

        public void DoChecking()
        {
            while (true)
            {
                //AgentLogs.WriteLog("Getting data from meeting server...", AgentLogEventLevel.Details);
                var meeting = GetMeetingStatus();
                OnMeetingRoomDataUpdated(this, meeting);
                Thread.Sleep(30000);
            }
        }

        static public string MakeLoginUrl(string username, string roomname)
        {
            return string.Format(loginUrl, username, roomname).Replace(" ", "%20");
        }

        public List<Meeting> GetMeetingStatus()
        {
            //string username = new AgentSettings().GetFullname();
            List<Meeting> meetings = new List<Meeting>();
            try
            {
                var httpContent =
                    getHtml(url + "?" + DateTime.UtcNow.ToString()).Replace("<?xml version=\"1.0\" ?>\r\n", string.Empty)
                        .Replace(
                            "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\n\n\n \r\n\r\n\r\n\r\n\r\n\r\n\r\n",
                            string.Empty);
                //var meetingsContent = XDocument.Load(@"http://meeting.directhr.cn/demo/demo4_helper.jsp");
                //XmlDocument xDom = new XmlDocument();
                //xDom.LoadXml(httpContent);
                // var meetings = from p in meetingsContent.Descendants("meeting")

                using (StringReader sr = new StringReader(httpContent))
                {
                    //using (StringReader sr = new StringReader(xml))
                    //{

                    XDocument doc = XDocument.Load(sr);

                    foreach (var m in doc.Descendants("meeting"))
                    {
                        var users = new List<User>();
                        foreach (var u in m.Element("attendees").Descendants("attendee"))
                        {
                            users.Add(new User()
                                          {
                                              FullName = u.Element("fullName").Value,
                                              Role = u.Element("role").Value,
                                              UserId = u.Element("userID").Value
                                          });
                            //users.Add(u.Element("fullName").Value);
                        }
                        meetings.Add(FormatColor(new Meeting()
                                                     {
                                                         Users = users,
                                                         NumOfUsers = users.Count.ToString("d"),
                                                         StartTime = m.Element("startTime").Value,
                                                         Title = m.Element("meetingName").Value //,
                                                         //JoinUrl = string.Format(loginUrl, username, m.Element("meetingName").Value)

                                                     }));
                    }
                }
                return meetings;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }


    public class Meeting
    {
        public string CreateTime { get; set; }
        public string JoinUrl { get; set; }
        public string Color { get; set; }
        public Brush BrushColor { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; }
        public string NumOfUsers { get; set; }
        public string UsersString { get; set; }
        public List<User> Users { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }

}
