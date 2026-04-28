using System;
using System.Web.Security;
using System.Web.UI;
using CrimeRiskWeb.Services;

namespace WebApplication1_Assignment5
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;

            if (username == "" || password == "")
            {
                Output.Text = "Please enter username and password";
                return;
            }

            if (UserService.AuthenticateUser(username, password))
            {
                string role = UserService.GetRole(username);
                Global.SetUserSession(Session, username, role);

                string userType = Session["UserType"] as string;
                if (userType == "CookieTester")
                {
                    Response.Redirect("~/CookieTestingPage.aspx");
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                }
            }
            else
            {
                Output.Text = "Invalid login";
            }
        }
    }
}