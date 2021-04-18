using System;

namespace WindowsAuthSample
{
    public partial class WindowsAuthSite : System.Web.UI.MasterPage
    {
        public int SecondsRemaining
        {
            get
            {
                if (Session["ExpirationTime"] == null)
                    return Session.Timeout * 60;
                return (int)((DateTime) Session["ExpirationTime"] - DateTime.Now).TotalSeconds;
            }
        }
        
        public string LogonTime
        {
            get
            {
                string logonTime;
                if (Session["LogonTime"] == null)
                {
                    logonTime = $"{DateTime.Now:MM/dd/yy H:mm:ss tt}";
                    Session["LogonTime"] = logonTime;
                }
                else logonTime = Session["LogonTime"] as string;

                return logonTime;
            }
        }
    }
}