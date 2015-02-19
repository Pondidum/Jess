using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Jess
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{*url}",
                defaults: new { controller = "Hydration", action = "get" }
            );
        }
    }
}
