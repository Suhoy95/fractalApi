using FractalApi.Infrastructure;
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
            config.Filters.Add(new HandAuthAttribute() );

            config.Routes.MapHttpRoute(
                name: "ApiWithId",
                routeTemplate: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"\d+"}
            );

            config.Routes.MapHttpRoute(
                name: "AuthApi",
                routeTemplate: "api/auth/{action}",
                defaults: new { controller = "Auth"}
            );

            config.Routes.MapHttpRoute(
                name: "ApiWithSlug",
                routeTemplate: "api/{controller}/{slug}",
                defaults: new { slug = RouteParameter.Optional },
                constraints: new { slug=@"[a-zA-Z0-9]+"}
            );

            config.Routes.MapHttpRoute(
                name: "ApiREST",
                routeTemplate: "api/{controller}/"
            );
        }
    }
}
