using System;
using System.Security.Cryptography;
using System.Text;
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

            // email is optional here — pass empty string until a field is added
            bool success = UserService.RegisterUser(username, password, "Member", "", Sha256Hash);

            Output.Text = success
                ? "Successfully registered member"
                : "Couldn't register member, member with this username already exists";
        }

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