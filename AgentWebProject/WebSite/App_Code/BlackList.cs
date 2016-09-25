using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Web.Services;
using DhrAgentDatabaseUtils;

/// <summary>
/// Summary description for ProgramBlackList
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class BlackList : System.Web.Services.WebService
{

    public BlackList()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void UploadProcessLog(string computername, string fullname, string keywords, string windowsname, string processname, string processpath)
    {
        var processLog = new ProcessKillerLog()
        {
            ComputerName = computername,
            FullName = fullname,
            KeyWords = keywords,
            KilledAt = DateTime.Now,
            ProcessName = processname,
            ProcessPath = processpath,
            WindowsName = windowsname
        };
        ProgramBlackListMgr mgr = new ProgramBlackListMgr();
        mgr.PostProcessKillerLog(processLog);
    }

    [WebMethod]
    public ProgramBlackList blackList()
    {
        return new ProgramBlackList();
    }


    [WebMethod]
    public ProgramBlackList[] GetBlackList(string fullname)
    {
        if (new DevelopmentMgr().IsDevelopmentUser(fullname))
            return new List<ProgramBlackList>().ToArray();

        if (!TimeDefines.IsWorkingTime())
            return null;
        var blackListMgr = new ProgramBlackListMgr();
        return blackListMgr.GetCachedBlackList(fullname).ToArray();
    }


    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

}
