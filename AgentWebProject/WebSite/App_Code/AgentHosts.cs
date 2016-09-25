using DhrAgentDatabaseUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// AgentHosts 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class AgentHosts : System.Web.Services.WebService
{

    public AgentHosts()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Upload(string fullname,
        string extensionnumber,
        string verison,
        string computername,
        string areacode,
        string cityname
        )
    {
        string ip = string.IsNullOrEmpty(this.Context.Request.Headers["X-Real-IP"])
                        ? this.Context.Request.UserHostAddress
                        : this.Context.Request.Headers["X-Real-IP"];

        var uploadstatus = new LoginStatus()
        {
            AreaCode = areacode,
            CityName = cityname,
            ComputerName = computername,
            Count = 0,
            CreateAt = DateTime.Now,
            ExtensionNumber = extensionnumber.Trim(),
            FullName = fullname.Trim(),
            IPAddress = ip,
            LastModifiedAt = DateTime.Now,
            MACAddress = string.Empty,
            Version = verison
        };


        if (new DevelopmentMgr().IsIgnoreAllBlocing(uploadstatus.FullName))
            return "#IGNORE ALL";

        HostsMgr mgr = new HostsMgr();
        List<Hosts> hosts = null;
        hosts = mgr.GetHosts(uploadstatus.FullName, uploadstatus.AreaCode, new DevelopmentMgr().IsDevelopmentUser(uploadstatus.FullName));
        var sb = new StringBuilder(Environment.NewLine);
        foreach (var host in hosts)
        {
            sb.Append("#HOST ID FROM REMOTE SERVER:");
            sb.AppendLine(host.Hosts_Id.ToString());
            sb.AppendLine(host.HostsContent);
        }
        return sb.ToString();
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

}
