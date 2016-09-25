using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public class ClientNetworkFilterMgr
    {
        public string GetHeader(string filterFormatVersion4Chars, string filterContentVersion)
        {
            return string.Format("\r\n[DHRNETWORK]\r\n[DHRNF_VERSION:{0}]\r\n[CONTENT:{1}]\r\n",
                filterContentVersion,
                filterFormatVersion4Chars
               );
        }

        public string GetEnd()
        {
            return
                "\r\n[DHRNF_REPLY:XXXXXXXX]\r\n[DHRNF_REPLYVERSION:XXXXXXXX]\r\n[ENDDHRNF_QZJ]\r\n";
        }

        public List<string> LoadCurrentTimePeriodName()
        {
            int currentTime = Convert.ToInt32(DateTime.Now.ToString("HHmm"));
            using (var db = new DBConn())
            {
                return db.TimePeriod.Where(c => c.StartAt <= currentTime
                    && c.EndAt > currentTime).Select(c => c.TimeName).ToList();
            }
        }

        public List<NetworkFilters> GetFilters(string userName, string office, List<string> timeNames)
        {
            userName = userName.ToUpper();
            office = office.ToUpper();

            using (var db = new DBConn())
            {
                var Filters = db.NetworkFilters.Where(c =>
                    (c.OfficeLocation == office || c.OfficeLocation == string.Empty) &&
                    (c.MemberNames.Contains("," + userName + ",") || c.MemberNames == string.Empty) &&
                    (!c.ExceptionUsers.Contains("," + userName + ",") || c.ExceptionUsers == string.Empty) &&
                    (timeNames.Contains(c.TimePeriod.ToUpper()))
                    ).OrderByDescending(c => c.Pri);
                return Filters.ToList();
            }
        }


        public List<NetworkFilters> GetHighPriFiltersONLY(string userName, string office, List<string> timeNames)
        {
            userName = userName.ToUpper();
            office = office.ToUpper();

            using (var db = new DBConn())
            {
                var Filters = db.NetworkFilters.Where(c =>
                    (c.OfficeLocation == office || c.OfficeLocation == string.Empty) &&
                    (c.MemberNames.Contains("," + userName + ",") || c.MemberNames == string.Empty) &&
                    (!c.ExceptionUsers.Contains("," + userName + ",") || c.ExceptionUsers == string.Empty) &&
                    (timeNames.Contains(c.TimePeriod.ToUpper())) &&
                    (c.Pri > 99)
                    ).OrderByDescending(c => c.Pri);
                return Filters.ToList();
            }
        }

        public List<NetworkFilters> GetFilters(string userName, string office, bool isDevelopmentUser)
        {
            if (isDevelopmentUser)
                return GetHighPriFiltersONLY(userName, office, LoadCurrentTimePeriodName());
            else
                return GetFilters(userName, office, LoadCurrentTimePeriodName());
        }

    }
}
