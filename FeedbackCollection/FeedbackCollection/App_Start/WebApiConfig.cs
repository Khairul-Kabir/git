using FeedbackCollection.Models.DependencyResolver;
using FeedbackCollection.Models.Rpository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace FeedbackCollection
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //CORS configuration
            config.EnableCors();

            //Configur Unity Container

            var container = new UnityContainer();
            container.RegisterType<IFeedBack, FeedBack>(new TransientLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            //JSON Formater
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
