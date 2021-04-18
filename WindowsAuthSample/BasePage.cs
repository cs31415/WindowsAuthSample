using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.UI;
using WindowsAuthSample.Helpers;

namespace WindowsAuthSample
{
    public class BasePage: System.Web.UI.Page
    {
        public string SessionVariables
        {
            get
            {
                var sb = new StringBuilder();
                foreach (string key in Session.Keys)
                {
                    if (key != "Variables" && key != "Sequence")
                    {
                        sb.Append($"{key}={Session[key]}<br/>");
                    }
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Throw a login prompt if required
        /// </summary>
        private void PromptForCredentials()
        {
            // Returning a 401 might throw up a login prompt if the browser has no cached credentials.
            // If the browser has cached credentials, then it might return those without
            // throwing up a login prompt. This code is to force the browser to throw
            // a login prompt to give the user a chance to enter fresh credentials following
            // a session expiration or explicit log out.

            if (Session["Key"] != null && GetSessionKey() != Session["Key"] as string)
            {
                // Possible session hijacking attempt. Return 401.
                Response.Redirect("SessionExpired.aspx");
            }

            var authLen = Request.Headers["Authorization"]?.Length ?? 0;
            Session["Sequence"] = (Session["Sequence"] as string ?? string.Empty) + (Request.Headers["Authorization"] ?? "null").TakeFirst(15) + $"({authLen})" + ", Token = " + (User.Identity as WindowsIdentity)?.Token;
            Session["Variables"] = $"{SessionVariables}";                

            if (Session["LoggedIn"] == null || Session["LoggedOut"] != null)
            {
                var authHeader = Request.Headers["Authorization"];
                var firstPrompt = Session["1stPromptRequested"] != null || authHeader != null;
                var secondPrompt = Session["2ndPromptRequested"] != null;

                Logger.LogDebug($"{Request.Url.AbsolutePath}, BasePage.OnPreInit: Session.SessionID = {Session.SessionID}, Request.Cookies[ASP.NET_SessionId] = {Request.Cookies["ASP.NET_SessionId"]?.Value}");
                var loggedOut = (Session["LoggedOut"] != null && (bool) Session["LoggedOut"])
                                || string.IsNullOrEmpty(Request.Cookies["ASP.NET_SessionId"]?.Value)
                                || authHeader == null
                                || Request.Browser.Browser == "Safari"
                                || Request.Browser.Browser == "InternetExplorer";
                Session["LoggedOut"] = loggedOut;
                
                var freshCredentials = (secondPrompt && !string.IsNullOrEmpty(authHeader));

                if (!loggedOut || freshCredentials)
                {
                    Session["LoggedIn"] = true;
                    Session["Key"] = GetSessionKey();
                    Session["ExpirationTime"] = DateTime.Now.AddMinutes(Session.Timeout);
                    /*Session.Remove("1stPromptRequested");
                    Session.Remove("2ndPromptRequested");
                    Session.Remove("Sequence");
                    Session.Remove("Variables");*/
                    Session.Remove("LoggedOut");
                    Session["Sequence"] += $"<br/>";
                }
                else
                {
                    if (!firstPrompt)
                    {
                        Session["1stPromptRequested"] = true;
                    }
                    else if (!secondPrompt)
                    {
                        Session["2ndPromptRequested"] = true;
                    }

                    Session["Sequence"] += $",401<br/>";
                    
                    // If we don't have a fresh auth header value, force a login prompt
                    Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    Response.AddHeader("cache-control", "no-cache=authorization");
                    Response.End();
                }
            }
        }

        private string GetSessionKey()
        {
            var key = $"{User.Identity.Name}|{Request.UserHostAddress}|{Request.Browser.Type}|{Request.Browser.Version}";
            byte[] bytes = Encoding.ASCII.GetBytes(key);
            return System.Convert.ToBase64String(bytes);
        }

        #region Event Handlers

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            PromptForCredentials();
        }

        /// <summary>
        /// Method called by the framework to render the page
        /// </summary>
        /// <param name="writer">The writer to use to write the content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //Response.Expires = 0;
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.AddHeader("pragma", "no-cache");
            Response.AppendHeader("Refresh", (60*Session.Timeout) + ";url=/sessionexpired.aspx");

            // Then we allow the base class to render the 
            // controls contained in the ASPX file.
            base.Render(writer);
        }
        #endregion
    }
}