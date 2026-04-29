using System;
using System.Web.UI;
using CrimeRiskWeb.Services;

namespace WebApplication1_Assignment5
{
    public partial class AccountManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login if session is missing
            if (!Global.IsLoggedIn(Session))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string username = Session["Username"].ToString();
                UsernameLabel.Text = username;
                RoleLabel.Text = Global.GetSessionRole(Session);
                EmailBox.Text = UserService.GetUserEmail(username);
            }
        }

        protected void SaveEmailBtn_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            string newEmail = EmailBox.Text.Trim();

            if (string.IsNullOrEmpty(newEmail))
            {
                MessageLabel.Text = "Email cannot be empty.";
                return;
            }

            bool success = UserService.UpdateEmail(username, newEmail);
            MessageLabel.Text = success ? "Email updated." : "Failed to update email.";
        }

        protected void SavePasswordBtn_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            string oldPassword = OldPasswordBox.Text;
            string newPassword = NewPasswordBox.Text;
            string confirm = ConfirmPasswordBox.Text;

            if (string.IsNullOrEmpty(oldPassword) ||
                string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(confirm))
            {
                MessageLabel.Text = "All password fields are required.";
                return;
            }

            if (newPassword != confirm)
            {
                MessageLabel.Text = "New passwords do not match.";
                return;
            }

            bool success = UserService.UpdatePassword(username, oldPassword, newPassword);
            MessageLabel.Text = success
                ? "Password updated."
                : "Failed to update password. Check your current password and try again.";
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            string role = Global.GetSessionRole(Session);
            if (role == "Staff")
                Response.Redirect("~/ProtectedStaff/StaffPage.aspx");
            else
                Response.Redirect("~/ProtectedMember/MemberPage.aspx");
        }
    }
}