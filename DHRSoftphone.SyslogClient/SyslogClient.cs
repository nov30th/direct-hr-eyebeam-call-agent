using System;
using System.Net;

namespace DHRSoftphone.SyslogClient
{
    public enum Facility
    {
        Kernel = 0,
        User = 1,
        Mail = 2,
        Daemon = 3,
        Auth = 4,
        Syslog = 5,
        Lpr = 6,
        News = 7,
        UUCP = 8,
        Cron = 9,
        Local0 = 10,
        Local1 = 11,
        Local2 = 12,
        Local3 = 13,
        Local4 = 14,
        Local5 = 15,
        Local6 = 16,
        Local7 = 17,
    }

    public enum Level
    {
        Emergency = 0,
        Alert = 1,
        Critical = 2,
        Error = 3,
        Warning = 4,
        Notice = 5,
        Information = 6,
        Debug = 7,
    }

    public class Client
    {
        private int _port = 514;
        private string _sysLogServerIp = null;
        private IPAddress ipAddress;
        private IPHostEntry ipHostInfo;
        private IPEndPoint ipLocalEndPoint;
        private UdpClientEx udpClient;

        public Client()
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            ipLocalEndPoint = new IPEndPoint(ipAddress, 0);
            udpClient = new UdpClientEx(ipLocalEndPoint);
        }

        public bool IsActive
        {
            get { return udpClient.IsActive; }
        }

        public int Port
        {
            set { _port = value; }
            get { return _port; }
        }

        public string SysLogServerIp
        {
            get { return _sysLogServerIp; }
            set
            {
                if ((_sysLogServerIp == null) && (!IsActive))
                {
                    _sysLogServerIp = value;

                    //udpClient.Connect(_hostIp, _port);
                }
            }
        }

        public void Close()
        {
            if (udpClient.IsActive) udpClient.Close();
        }

        public void Send(Message message)
        {
            if (!udpClient.IsActive)
                udpClient.Connect(_sysLogServerIp, _port);
            if (udpClient.IsActive)
            {
                int priority = message.Facility * 8 + message.Level;
                string msg = System.String.Format("<{0}>{1} {2} {3}",
                                                  priority,
                                                  DateTime.Now.ToString("MMM dd HH:mm:ss"),
                                                  ipLocalEndPoint.Address,
                                                  message.Text);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
                udpClient.Send(bytes, bytes.Length);
            }
            else throw new Exception("Syslog client Socket is not connected. Please set the SysLogServerIp property");
        }
    }

    public class Message
    {
        private int _facility;

        private int _level;

        private string _text;

        public Message() { }

        public Message(int facility, int level, string text)
        {
            _facility = facility;
            _level = level;
            _text = text;
        }

        public int Facility
        {
            get { return _facility; }
            set { _facility = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }

    /// need this helper class to expose the Active propery of UdpClient
    /// (why is it protected, anyway?)
    public class UdpClientEx : System.Net.Sockets.UdpClient
    {
        public UdpClientEx() : base() { }

        public UdpClientEx(IPEndPoint ipe) : base(ipe) { }

        ~UdpClientEx()
        {
            if (this.Active) this.Close();
        }

        public bool IsActive
        {
            get { return this.Active; }
        }
    }
}