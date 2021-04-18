using System;
using System.Net;
using System.Web;

namespace WindowsAuthSample
{
    public partial class Logout : System.Web.UI.Page
    {
        public string Referrer { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Referrer = Request?.UrlReferrer?.AbsoluteUri ?? "/admin";
            if (Session["LoggedIn"] != null)
            {
                Session.Clear();
                Session["LoggedOut"] = true;
                Session.Abandon();
                //Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                //HttpContext.Current.User = null; 
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            }
        }
    }
}