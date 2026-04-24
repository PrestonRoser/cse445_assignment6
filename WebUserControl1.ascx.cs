using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1_Assignment5
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Login Page loading...");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Clicked button"); 
            string username = TextBox1.Text; // store username in first text box
            //System.Diagnostics.Debug.WriteLine($"Username {username}");
            string password = TextBox2.Text; // store password in next text box
            //System.Diagnostics.Debug.WriteLine($"Password {password}");

            if(username == "" || password == "") // if username or password is empty do nothing
            {
                return;

            }
            
            
            string goToPage = Session["GoToPage"].ToString(); // get page to go to 
            string userType = Session["UserType"].ToString(); // get user type
            Session[userType + "username"] = TextBox1.Text; // store name in session
            Session[userType + "password"] = TextBox2.Text; // add password to cookies object
         
            System.Diagnostics.Debug.WriteLine($"Go to page is: {goToPage}");
            Response.Redirect(goToPage); // redirect to page

        }
    }
}