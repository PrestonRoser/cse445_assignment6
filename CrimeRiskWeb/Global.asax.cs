using System;
using System.Web;
using System.Web.Routing;

namespace CrimeRiskWeb
{
    /// <summary>
    /// Global application class for the ASP.NET Web Forms application.
    /// Tracks total visits and total searches as assignment-facing local component behavior.
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// Initializes application-wide counters when the site starts.
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["TotalVisits"] = 0;
            Application["TotalSearches"] = 0;

            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
        }

        /// <summary>
        /// Increments the total visit count whenever a new session begins.
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["TotalVisits"] = Convert.ToInt32(Application["TotalVisits"]) + 1;
            Application.UnLock();
        }

        /// <summary>
        /// Helper method called by the UI when a successful search is performed.
        /// </summary>
        public static void IncrementSearchCount(HttpApplicationState application)
        {
            application.Lock();
            application["TotalSearches"] = Convert.ToInt32(application["TotalSearches"]) + 1;
            application.UnLock();
        }
    }
}
