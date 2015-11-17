using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProView.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // remove the XML formatter which allows viewing JSON data in Chrome and Firefox (their default is XML).
            // this is not recommended in real production systems.
            GlobalConfiguration.Configuration.Formatters.RemoveAt(1);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register); 
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            // get last exception
            var exception = HttpContext.Current.Server.GetLastError();

            if (exception != null)
                LogException(exception);
        }

        void LogException(Exception exception)
        {
            // try-catch because database itself could be down or Request context is unknown.

            try
            {
            }
            catch { /* do nothing, or send email to webmaster*/}
        }
    }
}
