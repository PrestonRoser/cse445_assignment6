using System;
using System.Web.Security;
using System.Web.UI;
using WebApplication1_Assignment5;

namespace Police_Forensics_CSE445
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session_start();
        }

        public void Session_start()
        {
            if (Global.GetSessionRole(Session) != "Staff")
                Response.Redirect("~/Login.aspx");

            user_lb.Text = "Welcome Staff!";
        }

        protected void account_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccountManagement.aspx");
        }

        protected void criminalman_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Criminal_database.aspx");
        }

        protected void file_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/File-analysis.aspx");
        }

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            Global.ClearUserSession(Session);
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}