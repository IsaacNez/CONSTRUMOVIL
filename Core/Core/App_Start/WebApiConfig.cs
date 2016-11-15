﻿using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication1.Models;
using System.Collections.Generic;



namespace Core
{
    public static class WebApiConfig
    {
   
            public static void Register(HttpConfiguration config)
            {
                System.Diagnostics.Debug.WriteLine("Está llegando acá "+config.Routes.VirtualPathRoot.ToString());
                EnableCrossSiteRequests(config);
                AddRoutes(config);
            }

            private static void AddRoutes(HttpConfiguration config)
            {
                config.Routes.MapHttpRoute(
                    name: "complete",
                    routeTemplate: "api/{controller}/{action}/{attribute}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                name: "Post",
                routeTemplate: "api/{controller}/post/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
            config.Routes.MapHttpRoute(
                name: "Update",
                routeTemplate: "api/{controller}/update/{attr}/{avalue}/{clause}/{id}",
                defaults: new { id = RouteParameter.Optional}
                );
            config.Routes.MapHttpRoute(
                name: "check",
                routeTemplate: "api/{controller}/{action}/{attribute}/{id}/{charge}",
                defaults: new {id = RouteParameter.Optional}
                );

            }

            private static void EnableCrossSiteRequests(HttpConfiguration config)
            {
            System.Diagnostics.Debug.WriteLine("Está habilitando los cors");
                var cors = new EnableCorsAttribute(
                    origins: "*",
                    headers: "*",
                    methods: "*");

                config.EnableCors(cors);
            }
        }
}
