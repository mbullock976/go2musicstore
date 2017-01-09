using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Go2MusicStore
{
    using System.Net.Http.Formatting;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Custom Formatters - that ensures all json objects are returned as camelCase not PascalCase
            //which is what javascript requires.
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            //my custom route to handle multiple Get signatures 
            //- refer to AlbumsApiController multiple HttpGet methods -to avoid multipe Gets with same method signatures
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
