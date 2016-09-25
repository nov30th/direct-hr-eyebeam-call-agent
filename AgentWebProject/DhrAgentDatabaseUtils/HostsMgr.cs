using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{

    public class HostsMgr
    {
        public List<string> LoadCurrentTimePeriodName()
        {
            int currentTime = Convert.ToInt32(DateTime.Now.ToString("HHmm"));
            using (var db = new DBConn())
            {
                return db.TimePeriod.Where(c => c.StartAt <= currentTime
                    && c.EndAt > currentTime).Select(c => c.TimeName).ToList();
            }
        }

        public List<Hosts> GetHosts(string userName, string office, List<string> timeNames)
        {
            userName = userName.ToUpper();
            office = office.ToUpper();

            using (var db = new DBConn())
            {
                var hosts = db.Hosts.Where(c =>
                    (c.OfficeLocation == office || c.OfficeLocation == string.Empty) &&
                    (c.MemberNames.Contains("," + userName + ",") || c.MemberNames == string.Empty) &&
                    (!c.ExceptionUsers.Contains("," + userName + ",") || c.ExceptionUsers == string.Empty) &&
                    (timeNames.Contains(c.TimePeriod.ToUpper()))
                    ).OrderByDescending(c => c.Pri);
                return hosts.ToList();
            }
        }


        public List<Hosts> GetHighPriHostsONLY(string userName, string office, List<string> timeNames)
        {
            userName = userName.ToUpper();
            office = office.ToUpper();

            using (var db = new DBConn())
            {
                var hosts = db.Hosts.Where(c =>
                    (c.OfficeLocation == office || c.OfficeLocation == string.Empty) &&
                    (c.MemberNames.Contains("," + userName + ",") || c.MemberNames == string.Empty) &&
                    (!c.ExceptionUsers.Contains("," + userName + ",") || c.ExceptionUsers == string.Empty) &&
                    (timeNames.Contains(c.TimePeriod.ToUpper())) &&
                    (c.Pri > 99)
                    ).OrderByDescending(c => c.Pri);
                return hosts.ToList();
            }
        }

        public List<Hosts> GetHosts(string userName, string office, bool isDevelopmentUser)
        {
            if (isDevelopmentUser)
                return GetHighPriHostsONLY(userName, office, LoadCurrentTimePeriodName());
            else
                return GetHosts(userName, office, LoadCurrentTimePeriodName());
        }





    }
}
