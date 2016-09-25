using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public class DevelopmentMgr
    {
        DBConn db = new DBConn();

        public bool IsDevelopmentUser(string username)
        {
            return (db.DevelopmentUsers.Count(u => u.DevelopmentName.ToUpper() == username.ToUpper()) > 0) ? true : false;
        }

        public bool IsDevelopmentExt(string extensionNumber)
        {
            return (db.DevelopmentUsers.Count(u => u.DevelopmentExt == extensionNumber) > 0) ? true : false;
        }

        public bool IsIgnoreAllBlocing(string username)
        {
            return (db.DevelopmentUsers.Count(u => u.DevelopmentName.ToUpper() == username.ToUpper() && u.IsIgnoreAllRules == true) > 0) ? true : false;
        }

        public List<string> GetAllDevelopmentUsersName()
        {
            return db.DevelopmentUsers.Select(u => u.DevelopmentName).ToList();
        }

    }
}
