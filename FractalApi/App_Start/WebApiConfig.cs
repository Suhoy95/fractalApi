using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FractalApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ApiWithId",
                routeTemplate: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"\d+"}
            );

            config.Routes.MapHttpRoute(
                name: "ApiWithSlug",
                routeTemplate: "api/{controller}/{slug}",
                defaults: new { slug = RouteParameter.Optional },
                constraints: new { slug=@"[a-zA-Z]+"}
            );

            config.Routes.MapHttpRoute(
                name: "ApiREST",
                routeTemplate: "api/{controller}/"
            );
        }
    }
}
