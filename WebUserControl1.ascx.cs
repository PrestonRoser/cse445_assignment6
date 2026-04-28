using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
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

            // Authenticate using the service layer
            bool isValid = UserService.AuthenticateUser(username, password, Sha256Hash);

            if (isValid)
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

        // SHA-256 stand-in until the DLL is integrated — must match XmlUserStore's seed hash
        private static string Sha256Hash(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}