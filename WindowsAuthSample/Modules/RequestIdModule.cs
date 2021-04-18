using System;
using System.Web;
using WindowsAuthSample.Helpers;

namespace WindowsAuthSample.Modules
{
    class RequestIdModule : IHttpModule
    {
        public void Init(HttpApplication applicationContext)
        {
            applicationContext.BeginRequest += ApplicationContext_BeginRequest;
            applicationContext.AuthenticateRequest += ApplicationContext_AuthenticateRequest;
            applicationContext.PostAuthenticateRequest += ApplicationContext_PostAuthenticateRequest;
            applicationContext.PostAcquireRequestState += ApplicationContext_PostAcquireRequestState;
            applicationContext.EndRequest += ApplicationContext_EndRequest;
        }

        private void ApplicationContext_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            Logger.LogDebug($"{context.Request.Url.AbsolutePath}, ApplicationContext_EndRequest: Request.Cookies[ASP.NET_SessionId] = {context.Request.Cookies["ASP.NET_SessionId"]?.Value}");            
        }

        private void ApplicationContext_PostAcquireRequestState(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            Logger.LogDebug($"{context.Request.Url.AbsolutePath}, ApplicationContext_PostAcquireRequestState: Request.Cookies[ASP.NET_SessionId] = {context.Request.Cookies["ASP.NET_SessionId"]?.Value}");            
        }

        private void ApplicationContext_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            Logger.LogDebug($"{context.Request.Url.AbsolutePath}, ApplicationContext_PostAuthenticateRequest: Request.Cookies[ASP.NET_SessionId] = {context.Request.Cookies["ASP.NET_SessionId"]?.Value}");
        }

        private void ApplicationContext_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            Logger.LogDebug($"{context.Request.Url.AbsolutePath}, ApplicationContext_AuthenticateRequest: Request.Cookies[ASP.NET_SessionId] = {context.Request.Cookies["ASP.NET_SessionId"]?.Value}");
        }

        private void ApplicationContext_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            Logger.LogDebug($"{context.Request.Url.AbsolutePath}, ApplicationContext_BeginRequest: Request.Cookies[ASP.NET_SessionId] = {context.Request.Cookies["ASP.NET_SessionId"]?.Value}");
        }

        public void Dispose()
        {
            
        }
    }
}
