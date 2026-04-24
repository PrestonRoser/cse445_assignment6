using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1_Assignment5
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Login button clicked");
            Response.Redirect("Login.aspx");
        }

        protected void CookieTestingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CookieTestingPage.aspx");

        }

        protected void staffbutton_Click(object sender, EventArgs e)
        {
            Session["GoToPage"] = "StaffPage.aspx"; // create session object 

            Session["UserType"] = "Staff"; // userType is staff
            Object staffUsername = Session["Staffusername"];// get member username
            Object staffPassword = Session["Staffpassword"]; // get member password

            if(staffUsername == null || staffPassword == null) // check if staff username or password is null
            {
                Response.Redirect("Login.aspx");
            }


            if (staffUsername.ToString() == "" || staffPassword.ToString() == "") { // check if username or password is empty
                Response.Redirect("Login.aspx"); // redirect to Login.aspx 
            }
            else
            {
                Response.Redirect("StaffPage.aspx"); // redirect to staff page
            }

        }

        protected void memberButton_Click(object sender, EventArgs e)
        {
            Session["GoToPage"] = "MemberPage.aspx"; // create session object
            Session["UserType"] = "Member"; // userType is Member

            Object memberUsername = Session["Memberusername"]; // get member username
            Object memberPassword = Session["Memberpassword"]; // get member password

            if(memberUsername == null || memberPassword == null) // check if username or password is null
            {
                Response.Redirect("Login.aspx");

            }


            if (memberUsername.ToString() == "" || memberPassword.ToString() == "") // check if username or pasword is empty
            { 
                Response.Redirect("Login.aspx"); // redirect to Login.aspx 
            }
            else
            {
                Response.Redirect("MemberPage.aspx"); // redirect to member page
            }

        }
    }
}