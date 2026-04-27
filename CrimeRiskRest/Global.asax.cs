using System.Web.Http;
using System.Web.Routing;

namespace CrimeRiskRest
{
    /// <summary>
    /// Global application class for the REST project.
    /// The main responsibility here is registering the Web API configuration.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Runs once when the REST application starts.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
        }
    }
}
