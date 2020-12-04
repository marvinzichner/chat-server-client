using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace csharp_webserver
{
    public class Global : HttpApplication
    {
        public static MessageStoreService messageStoreService
            = new MessageStoreService();

        public static LoggerService logger
            = new LoggerService();

        public static PublicHostAdressService publicHostAdressService =
            new PublicHostAdressService();

        void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if (lastError is HttpRequestValidationException)
            {
                Response.Redirect("~/Views/Chat.aspx?inputError=true");
            }
        }

    }
}