using System;
using System.Web.Http;
using WebAPI_Biblos.Controllers;

namespace WebAPI_Biblos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "DefaultApiLetra",
               routeTemplate: "api/{controller}/{porletra}/{letra}",
               defaults: new { letra = RouteParameter.Optional }
           );
          //  config.Routes.MapHttpRoute(
          //    name: "DefaultApiTemas",
          //    routeTemplate: "api/{controller}/temas",
          //    defaults: null
          //);
        }
    }
}
