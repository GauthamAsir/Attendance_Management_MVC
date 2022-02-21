using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PresentationMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{projectid}/{empid}/{managerid}",
                defaults: new
                {
                    controller = "Auth",
                    action = "Login",
                    id = UrlParameter.Optional,
                    projectid = UrlParameter.Optional,
                    empid = UrlParameter.Optional,
                    managerid = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Leave",
                url: "{controller}/{action}/{id}/{Transid}",
                defaults: new
                {
                    controller = "Leave",
                    action = "Index",
                    id = UrlParameter.Optional,
                    Transid = UrlParameter.Optional
                }
            );
        }
    }
}
