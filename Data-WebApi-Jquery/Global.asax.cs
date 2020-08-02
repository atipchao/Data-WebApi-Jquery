using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Data_WebApi_Jquery
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        // Allow request Origin to consume the API services - URL OF CLIENT Web App
        protected void Application_BeginRequest()
        {
            string[] allowedOrigin = new string[]
            {
                "http://localhost:53985"
            };
            var origin = HttpContext.Current.Request.Headers["Origin"];
            if(origin != null && allowedOrigin.Contains(origin))
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                //Need to add more late as needed
            }
        }
    }
}
