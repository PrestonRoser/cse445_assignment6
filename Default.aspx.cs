using System;
using System.Web.Security;
using System.Web.UI;

namespace WebApplication1_Assignment5
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void staffbutton_Click(object sender, EventArgs e)
        {
            Session["GoToPage"] = "ProtectedStaff/StaffPage.aspx";
            Session["UserType"] = "Staff";
            Response.Redirect("ProtectedStaff/StaffPage.aspx");
        }

        protected void memberButton_Click(object sender, EventArgs e)
        {
            Session["GoToPage"] = "ProtectedMember/MemberPage.aspx";
            Session["UserType"] = "Member";
            Response.Redirect("ProtectedMember/MemberPage.aspx");
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Global.ClearUserSession(Session);
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }

        protected void CookieTestingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CookieTestingPage.aspx");
        }

        protected void RegisterMemberButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterNewMember.aspx");
        }
    }
}