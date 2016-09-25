using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public class LoginRecordMgr
    {
        protected readonly int OUTOFTIMEMINS = -5; //Time for detect whether the client is offline.


        public long Add(LoginStatus status)
        {
            long id = -1;
            using (var conn = new DBConn())
            {
                var old =
                    conn.LoginStatus.SingleOrDefault(
                        c =>
                        c.ComputerName == status.ComputerName && c.ExtensionNumber == status.ExtensionNumber &&
                        c.FullName == status.FullName && c.Version == status.Version);
                if (old != null)
                {
                    old.FullName = old.FullName.Trim(); // This line remove the bug from client which does not deal with wihte space
                    old.IPAddress = status.IPAddress;
                    old.AreaCode = status.AreaCode;
                    old.CityName = status.CityName;
                    old.LastModifiedAt = DateTime.Now;
                    old.MACAddress = status.MACAddress;
                    old.Count++;
                    conn.SubmitChanges(ConflictMode.ContinueOnConflict);
                    id = old.Login_Id;
                }
                else
                {
                    conn.LoginStatus.InsertOnSubmit(status);
                    conn.SubmitChanges(ConflictMode.ContinueOnConflict);
                    id = status.Login_Id;
                }
            }
            //if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 17)
            SaveOnlineInfo(status);
            return id;
        }

        public List<LoginStatus> GetOnlineStatus()
        {
            List<LoginStatus> loginStatuses;
            using (var conn = new DBConn())
            {
                loginStatuses = conn.LoginStatus.Where(o => o.LastModifiedAt > DateTime.Now.AddMinutes(OUTOFTIMEMINS)).ToList();
            }
            return loginStatuses;
        }

        public List<LoginStatus> GetOnlineStatusOrderByFullname()
        {
            List<LoginStatus> loginStatuses;
            using (var conn = new DBConn())
            {
                loginStatuses = conn.LoginStatus.Where(o => o.LastModifiedAt > DateTime.Now.AddMinutes(OUTOFTIMEMINS)).OrderBy(o => o.FullName).ToList();
            }
            return loginStatuses;
        }


        public List<LoginStatus> GetOnlineTestStatus()
        {
            List<LoginStatus> loginStatuses;
            using (var conn = new DBConn())
            {
                loginStatuses = conn.LoginStatus.Take(4).ToList();
            }
            return loginStatuses;
        }


        /// <summary>
        /// Refresh the status of online clients.
        /// </summary>
        /// <param name="status"></param>
        public void SaveOnlineInfo(LoginStatus status)
        {
            using (var conn = new DBConn())
            {
                var old =
                    conn.ClientOnlineTime.SingleOrDefault(
                    c =>
                    c.ComputerName == status.ComputerName && c.ExtensionNumber == status.ExtensionNumber &&
                    c.Name == status.FullName && c.DayAt == DateTime.Today);
                if (old != null)
                {
                    if (old.LastLoginAt <= DateTime.Now.AddMinutes(OUTOFTIMEMINS))
                    {
                        //offline more than 10 mins.
                        old.Memo += "|Online at " + DateTime.Now.ToString("HH:mm:ss");
                    }
                    old.LastLoginAt = DateTime.Now;
                    old.IsOnline = true;
                }
                else
                {
                    //1st online today
                    var newRecord = new ClientOnlineTime()
                    {
                        ComputerName = status.ComputerName,
                        DayAt = DateTime.Today,
                        ExtensionNumber = status.ExtensionNumber,
                        LoginAt = DateTime.Now,
                        LastLoginAt = DateTime.Now,
                        Memo = "First Login At " + DateTime.Now.ToString("HH:mm:ss"),
                        OnlineMins = 0,
                        Name = status.FullName,
                        IsOnline = false
                    };
                    conn.ClientOnlineTime.InsertOnSubmit(newRecord);
                }
                conn.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }



        /// <summary>
        /// Must be executed 1 min 1 time.
        /// </summary>
        public void RefreshOnlineClientStatus()
        {
            //This function makes out-of-time client to offline status and not add the online time.
            using (var conn = new DBConn())
            {
                var outoftime = conn.ClientOnlineTime.Where(c => c.IsOnline == true && c.LastLoginAt <= DateTime.Now.AddMinutes(OUTOFTIMEMINS));
                if (outoftime.Count() > 0)
                {
                    foreach (var client in outoftime)
                    {
                        client.IsOnline = false;
                        client.Memo += "|Offline at " + client.LastLoginAt.ToString("HH:mm:ss");
                    }
                }
                var onlineClients = conn.ClientOnlineTime.Where(c => c.IsOnline == true);
                foreach (var client in onlineClients)
                {
                    client.OnlineMins++;
                }
                var systemOnlineMins = conn.ClientOnlineTime.SingleOrDefault(c => c.DayAt == DateTime.Today && c.Name == "system" && c.ComputerName == "system" && c.ExtensionNumber == "system");

                conn.SubmitChanges(ConflictMode.ContinueOnConflict);

                if (systemOnlineMins == null)
                {
                    var newSystemOnlineMins = new ClientOnlineTime()
                    {
                        ComputerName = "system",
                        ExtensionNumber = "system",
                        Name = "system",
                        DayAt = DateTime.Today,
                        IsOnline = true,
                        LastLoginAt = DateTime.Now,
                        LoginAt = DateTime.Now,
                        Memo = "System Login",
                        OnlineMins = 0
                    };
                    conn.ClientOnlineTime.InsertOnSubmit(newSystemOnlineMins);
                }
                else
                {
                    if (systemOnlineMins.IsOnline == false)
                    {
                        systemOnlineMins.Memo += "|Online at " + DateTime.Now.ToString("HH:mm:ss");
                        systemOnlineMins.IsOnline = true;
                    }
                    systemOnlineMins.LastLoginAt = DateTime.Now;
                    //systemOnlineMins.OnlineMins++;
                }

                conn.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }

        public long GetSystemOnlineTime(DateTime datetime)
        {
            using (var conn = new DBConn())
            {
                var oneDayStatus = conn.ClientOnlineTime.SingleOrDefault(c => c.DayAt == datetime.Date && c.Name == "system" && c.ExtensionNumber == "system" && c.ComputerName == "system");
                if (oneDayStatus == null)
                    return 0;
                else
                    return oneDayStatus.OnlineMins;
            }
        }


        public List<ClientOnlineTime> GetAllOnlineStatus(DateTime datetime, bool isHideDevelopmentUsers = false)
        {
            if (datetime <= DateTime.MinValue)
                return null;
            var onlineStatus = new List<ClientOnlineTime>();

            using (var conn = new DBConn())
            {
                var oneDayStatus = conn.ClientOnlineTime.Where(c => c.DayAt == datetime.Date).ToList();
                var memberManager = new MemberManager();
                var allmembers = memberManager.GetMembers();
                var developmentUsers = conn.DevelopmentUsers.Select(u => u.DevelopmentName);
                var offlineMembersDir = new Dictionary<string, string>();
                foreach (var dhrMember in allmembers)
                {
                    if (isHideDevelopmentUsers && developmentUsers.Contains(dhrMember.Fullname))
                        continue;
                    var inLoginStatus =
                        oneDayStatus.FirstOrDefault(l => l.Name.ToUpper() == dhrMember.Fullname.ToUpper());
                    {
                        if (inLoginStatus == null)
                        {
                            //not found, offline clients
                            offlineMembersDir.Add(dhrMember.Fullname, dhrMember.ExtensionNumber ?? string.Empty);
                        }
                        else
                            continue;//matchl, no problem
                    }
                }
                var offlineMemebers = memberManager.GetOfflineTimeString(offlineMembersDir);

                foreach (var offlineMember in offlineMemebers)
                {
                    onlineStatus.Add(new ClientOnlineTime()
                    {
                        ComputerName = string.Empty,
                        DayAt = datetime.Date,
                        ExtensionNumber = string.Empty,
                        Memo = "Not Login Today!",
                        IsOnline = false,
                        LastLoginAt = Convert.ToDateTime(offlineMember.Value == "Never" ? DateTime.MinValue.ToString() : offlineMember.Value),
                        LoginAt = DateTime.MinValue,
                        Name = offlineMember.Key,
                        OnlineMins = 0
                    });
                }

                foreach (var member in oneDayStatus)
                {
                    if (isHideDevelopmentUsers && developmentUsers.Contains(member.Name))
                        continue;
                    onlineStatus.Add(member);
                }
            }

            return onlineStatus.Where(c => c.Name != "system").OrderBy(c => c.Name).ToList();

        }

    }
}
