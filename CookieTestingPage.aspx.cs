using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1_Assignment5
{
    public partial class CookieTestingPageaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["GoToPage"] = "CookieTestingPage.aspx"; // store page to go to
            Session["UserType"] = "CookieTester"; // store user type 
            Object u = Session["CookieTesterusername"];


            // if null, return
            if (Session["CookieTesterusername"] == null || Session["CookieTesterpassword"] == null)
            {
                return;
            }


            String username = Session["CookieTesterusername"].ToString(); // get username
            String password = Session["CookieTesterpassword"].ToString(); // get password
            if (username == "" || password == "") // check if username or password is empty
            {
                LabelUsername.Text = "Welcome, new user"; //display welcome message

            }
            else
            {
                LabelUsername.Text = "Welcome, " + username; // read session content for name and display on label
                LabelPassword.Text = "We have your password " + password; // read session content for password and display on label
            }
        }
    }
}