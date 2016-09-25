using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。   
    /// NameSpace: DhrAgentDatabaseUtils
    /// FullName: DhrAgentDatabaseUtils.ProgramBlackListMgr
    /// Class Created: 2012/8/24 14:29
    /// On Computer: NOV30TH-LAPTOP - Administrator
    /// 
    /// </summary>
    public class ProgramBlackListMgr
    {
        protected DBConn Db;
        protected List<ProgramBlackList> Blacklists;


        public ProgramBlackListMgr()
        {
            if (Db == null)
                Db = new DBConn();
            GetAllBlackList();
        }

        public List<ProgramBlackList> GetProgramBlackListData(string fullname)
        {
            fullname += ",";
            var blacklist = Db.ProgramBlackLists.Where(b => !b.ExceptionUsers.Contains(fullname)).ToList();
            return blacklist;
        }



        public List<ProcessKillerLog> GetBlackList(DateTime startTime)
        {

            var endTime = DateTime.Now.AddDays(31);
            return GetProcessKillerLog(startTime, endTime);
        }

        public List<ProcessKillerLog> GetProcessKillerLog(DateTime startTime, DateTime endTime)
        {
            var result = Db.ProcessKillerLogs.Where(p => p.KilledAt >= startTime && p.KilledAt <= endTime);
            return result.ToList();

        }

        public List<ProgramBlackList> GetAllBlackList()
        {
            Blacklists = Db.ProgramBlackLists.ToList();
            return Blacklists;
        }

        public List<ProgramBlackList> GetCachedBlackList()
        {
            return Blacklists;
        }

        public List<ProgramBlackList> GetCachedBlackList(string fullname)
        {
            fullname += ",";
            var blacklist = Blacklists.Where(b => !b.ExceptionUsers.Contains(fullname)).ToList();
            return blacklist;
        }

        public void PostProcessKillerLog(ProcessKillerLog processLog)
        {
            Db.ProcessKillerLogs.InsertOnSubmit(processLog);
            Db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }


    }
}
