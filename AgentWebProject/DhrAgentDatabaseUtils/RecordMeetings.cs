using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public class RecordMeetings
    {
        DBConn db = new DBConn();


        public long RecordMeetingUserStatus()
        {
            long retval = 0;
            var meetingXml = new MeetingXml();
            var meetingStatus = meetingXml.GetMeetingStatus();
            foreach (var meeting in meetingStatus)
            {
                foreach (var user in meeting.Users)
                {
                    retval++;
                    var userInDb = db.OnlineMeetings.FirstOrDefault(u => u.UserID == user.UserId && u.Isquit == false);
                    if (userInDb == null)
                    {
                        //New user in meeting room
                        var newuser = new OnlineMeeting()
                                          {
                                              FullName = user.FullName,
                                              Isquit = false,
                                              OnlineBegins = DateTime.Now,
                                              OnlineEnds = DateTime.Now,
                                              RoomName = meeting.Color,
                                              UserID = user.UserId
                                          };

                        db.OnlineMeetings.InsertOnSubmit(newuser);

                        var newlog = new MeetingLog()
                                         {
                                             EventTime = DateTime.Now,
                                             FullName = user.FullName,
                                             RoomName = meeting.Color,
                                             Status = 1
                                         };

                        db.MeetingLogs.InsertOnSubmit(newlog);
                    }
                    else
                    {
                        //existed user
                        userInDb.OnlineEnds = DateTime.Now;
                    }
                }
            }


            db.SubmitChanges(ConflictMode.ContinueOnConflict);

            return retval;
        }

        public void SetUsersEndingTime()
        {
            var users = db.OnlineMeetings.Where(m => m.Isquit == false);
            if (users.Any())
            {
                foreach (var user in users)
                {
                    user.OnlineEnds = DateTime.Now;
                }
               
            } db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        public IQueryable<MeetingLog> GetMeetingLogs()
        {
            return db.MeetingLogs;
        }

        public IQueryable<MeetingLog> GetMeetingLogs(DateTime startTime)
        {
            return GetMeetingLogs().Where(m => m.EventTime > startTime);
        }

        public IQueryable<MeetingLog> GetMeetingLogs(DateTime startTime, DateTime endTime)
        {
            return GetMeetingLogs().Where(m => m.EventTime >= startTime && m.EventTime <= endTime);
        }

        public long FlagExpiredUserStatus()
        {
            long retval = 0;
            var expiredUsers =
                db.OnlineMeetings.Where(u => u.Isquit == false && u.OnlineEnds < DateTime.Now.AddMinutes(-2));
            if (expiredUsers.Any())
            {
                //has user status needs to be process
                foreach (var user in expiredUsers)
                {
                    retval++;
                    user.Isquit = true;

                    var newlog = new MeetingLog()
                    {
                        EventTime = user.OnlineEnds,
                        FullName = user.FullName,
                        RoomName = user.RoomName,
                        Status = -1
                    };

                    db.MeetingLogs.InsertOnSubmit(newlog);
                }
            }

            db.SubmitChanges(ConflictMode.ContinueOnConflict);
            return retval;
        }
    }
}
