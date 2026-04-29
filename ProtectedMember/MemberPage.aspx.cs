using System;
using System.Web.Security;
using System.Web.UI;

namespace WebApplication1_Assignment5
{
    public partial class MemberPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.GetSessionRole(Session) != "Member")
                Response.Redirect("~/Login.aspx");

            
        }

        protected void account_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccountManagement.aspx");
        }

        protected void criminal_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Criminal_database.aspx");
        }

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            Global.ClearUserSession(Session);
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}