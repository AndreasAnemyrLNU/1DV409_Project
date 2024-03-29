﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WeatherApp.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Forecast",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forecast", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
