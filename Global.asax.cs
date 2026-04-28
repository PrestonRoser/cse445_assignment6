using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using CrimeRiskWeb.Services;

namespace WebApplication1_Assignment5
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["TotalVisits"]   = 0;
            Application["TotalSearches"] = 0;

            // Guarantee both XML files exist before any request is served
            XmlUserStore.EnsureFilesExist();
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["TotalVisits"] = Convert.ToInt32(Application["TotalVisits"]) + 1;
            Application.UnLock();
        }

        public static void IncrementSearchCount(HttpApplicationState application)
        {
            application.Lock();
            application["TotalSearches"] = Convert.ToInt32(application["TotalSearches"]) + 1;
            application.UnLock();
        }

        // Session helpers
        public static void SetUserSession(HttpSessionState session, string username, string role)
        {
            session["Username"] = username;
            session["Role"]     = role;
        }

        public static void ClearUserSession(HttpSessionState session)
        {
            session.Remove("Username");
            session.Remove("Role");
        }

        public static bool IsLoggedIn(HttpSessionState session)
        {
            return session["Username"] != null
                && !string.IsNullOrEmpty(session["Username"].ToString());
        }

        public static string GetSessionRole(HttpSessionState session)
        {
            return session["Role"] as string;
        }
    }
}