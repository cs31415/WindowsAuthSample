using System;
using System.Web;

namespace WindowsAuthSample
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        public string Referrer { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Referrer = Request?.UrlReferrer?.AbsoluteUri ?? "/admin";
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

        }
    }
}