using System;
using System.Web.UI;
using CrimeRiskWeb.Services;

namespace WebApplication1_Assignment5
{
    public partial class RegisterNewMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;

            if (username == "" || password == "")
            {
                Output.Text = "Please enter a username and password";
                return;
            }

            bool success = UserService.RegisterUser(username, password, "Member", "");

            Output.Text = success
                ? "Successfully registered member"
                : "Couldn't register member, member with this username already exists";
        }
    }
}