using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
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
                        List<string> users = new List<string>();
                        foreach (var u in m.Element("attendees").Descendants("attendee"))
                        {
                            users.Add(u.Element("fullName").Value);
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
                new AgentException("Get Meeting Data Failed!", ex);
                AgentLogs.WriteLog("Get Meeting Data Failed!", AgentLogEventLevel.Errors);
            }
            return null;
        }
    }

    public class Meeting
    {
        public string JoinUrl { get; set; }

        public string Color { get; set; }

        public Brush BrushColor { get; set; }

        public string Title { get; set; }

        public string StartTime { get; set; }

        public string NumOfUsers { get; set; }

        public string UsersString { get; set; }

        public List<string> Users { get; set; }
    }

    //[Serializable, XmlRoot("meetings")]
    //public class Meetings
    //{
    //    public Meeting[] meeting { get; set; }
    //}

    //[Serializable, XmlRoot("meeting")]
    //public class Meeting
    //{
    //    public string returncode { get; set; }

    //    public string meetingName { get; set; }

    //    public string meetingID { get; set; }

    //    public string createTime { get; set; }

    //    public string voiceBridge { get; set; }

    //    public bool running { get; set; }

    //    public bool recording { get; set; }

    //    public bool hasBeenForciblyEnded { get; set; }

    //    public string startTime { get; set; }

    //    public string endTime { get; set; }

    //    public int participantCount { get; set; }

    //    public Attendee[] attendees { get; set; }

    //    public string metadata { get; set; }

    //    public string messageKey { get; set; }

    //    public string message { get; set; }
    //}

    //[Serializable, XmlRoot("attendees")]
    //public class Attendee
    //{
    //    public string userID { get; set; }

    //    public string fullName { get; set; }

    //    public string role { get; set; }
    //}
}