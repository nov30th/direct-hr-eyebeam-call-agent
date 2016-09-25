using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DhrAgentDatabaseUtils;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for UploadStatus
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UploadStatus : System.Web.Services.WebService
{

    public UploadStatus()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    
    public string EncodePassword(string originalPassword)
    {
        //Declarations
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;

        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
        encodedBytes = md5.ComputeHash(originalBytes);

        //Convert encoded bytes back to a 'readable' string
        return BitConverter.ToString(encodedBytes).Replace("-",string.Empty);
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
        var loginMgr = new LoginRecordMgr();
        loginMgr.Add(uploadstatus);
        if (new DevelopmentMgr().IsIgnoreAllBlocing(uploadstatus.FullName))
            return "#IGNORE FILTER";
        ClientNetworkFilterMgr filterMgr = new ClientNetworkFilterMgr();
        List<NetworkFilters> filters = null;
        filters = filterMgr.GetFilters(uploadstatus.FullName, uploadstatus.AreaCode, new DevelopmentMgr().IsDevelopmentUser(uploadstatus.FullName));
        var sb = new StringBuilder(Environment.NewLine);
        foreach (var filter in filters)
        {
            sb.Append("#FILTER ID FROM REMOTE SERVER:");
            sb.AppendLine(filter.Filter_Id.ToString());
            sb.AppendLine(filter.FilterContent);
        }

        string filterContent = sb.ToString();
        string filterHeader = filterMgr.GetHeader(
            "0001",
            EncodePassword(filterContent)
            );
        string filterEnd = filterMgr.GetEnd();
        return filterHeader + filterContent + filterEnd;



    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

}
