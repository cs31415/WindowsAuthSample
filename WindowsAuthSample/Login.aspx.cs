using System;
using System.Net;
using System.Web;

namespace WindowsAuthSample
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var referrer = Request?.UrlReferrer?.AbsoluteUri ?? "/admin";
            Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            Response.Redirect(referrer);
            Response.End();
        }
    }
}