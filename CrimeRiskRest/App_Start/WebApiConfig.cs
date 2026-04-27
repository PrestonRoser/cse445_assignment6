using System.Web.Http;

namespace CrimeRiskRest
{
    /// <summary>
    /// Registers Web API routes and formatters for the REST service project.
    /// This configuration keeps the route surface small and clear for WebStrar deployment.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers attribute routes and a default api/{controller}/{id} route.
        /// XML formatting is removed so that JSON is preferred during normal testing.
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
