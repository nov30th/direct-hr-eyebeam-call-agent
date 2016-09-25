<%@ Application Language="C#" %>
<%@ Import Namespace="System.Timers" %>
<%@ Import Namespace="DhrAgentDatabaseUtils" %>

<script RunAt="server">


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        System.Timers.Timer Timer = new System.Timers.Timer(5000);//5s触发一次
        Timer.Elapsed += new ElapsedEventHandler(Task);
        Timer.Enabled = true;
        Timer.AutoReset = true;
        Timer.Start();

        //Auto online status recording for soft phone agent client
        System.Timers.Timer Timer2 = new System.Timers.Timer(60000);//60s触发一次
        Timer2.Elapsed += new ElapsedEventHandler(Task2);
        Timer2.Enabled = true;
        Timer2.AutoReset = true;
        Timer2.Start();

        //Auto online status recording for soft phone agent client
        System.Timers.Timer Timer3 = new System.Timers.Timer(3600000);//3600s触发一次
        Timer3.Elapsed += new ElapsedEventHandler(TaskChangeLoadingPic);
        Timer3.Enabled = true;
        Timer3.AutoReset = true;
        Timer3.Start();
    }

    void Task2(object sender, ElapsedEventArgs elapsedEventArgs)
    {
        if (TimeDefines.IsWorkingTime())
        {
            LoginRecordMgr target = new LoginRecordMgr();
            target.RefreshOnlineClientStatus();
        }
    }

    void Task(object sender, ElapsedEventArgs elapsedEventArgs)
    {
        RecordMeetings target = new RecordMeetings();
        target.RecordMeetingUserStatus();
        target.FlagExpiredUserStatus();
    }

    void TaskChangeLoadingPic(object sender, ElapsedEventArgs elapsedEventArgs)
    {
        var picFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Loading_Pic/");
        var picPath = System.IO.Path.Combine(picFolder, DateTime.Today.ToString("MMdd") + ".jpg");
        if (System.IO.File.Exists(picPath))
        {
            System.IO.File.Copy(picPath,
                System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "loading.jpg"),
                true);
            //found pic Path
        }
        else
        {
            var chineseDate = new ChineseCalendar(DateTime.Now);
            var nongLiPic = "nl" + chineseDate.CMonth.ToString("d2") + chineseDate.CDay.ToString("d2") + ".jpg";
            if (System.IO.File.Exists(picPath))
            {
                System.IO.File.Copy(nongLiPic,
                    System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "loading.jpg"),
                    true);
            }
            else
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "loading.jpg")))
                {
                    System.IO.File.Delete(System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"), "loading.jpg"));
                }
            }
        }
    }


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
