using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Police_Forensics_CSE445
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["TotalScans"] = 0;
            Application["TotalCriminalsAdded"] = 0;
            Application["WebsiteStartTime"] = DateTime.Now;
        }
    }
}