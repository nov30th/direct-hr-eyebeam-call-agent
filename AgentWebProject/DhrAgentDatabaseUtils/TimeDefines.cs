using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public static class TimeDefines
    {
        public static bool IsWorkingTime()
        {
            if ((DateTime.Now.Hour == 11 && DateTime.Now.Minute > 30)
                || DateTime.Now.Hour == 12
                || DateTime.Now.Hour < 8
                || DateTime.Now.Hour >= 17)
                return false;
            return true;
        }
    }
}
