using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Ajax.Utilities;

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


            if (username == "" || password == "") // if username or password is empty do nothing
            {
                Output.Text = "Please enter username and password";
                return ;

            }
           
            string goToPage = Session["GoToPage"].ToString(); // get page to go to 
            string userType = Session["UserType"].ToString(); // get user type
            Session[userType + "username"] = TextBox1.Text; // store name in session
            Session[userType + "password"] = TextBox2.Text; // add password to cookies object

            if (myAuthenticate(username, password) == true) // if authentication is successful
            {
                System.Diagnostics.Debug.WriteLine($"Go to page is: {goToPage}");
                if(userType == "CookieTester")
                {
                    Response.Redirect("CookieTestingPage.aspx");
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                }
                    
                //Response.Redirect(goToPage); // redirect to page
            }
            else
            {
                Output.Text = "Invalid login";
            }
        }

        bool myAuthenticate(string username, string password)
        {
            string flocation = "";
            string userType = Session["UserType"].ToString(); // get user type 
            if (userType == "Staff" || userType == "CookieTester") // check if userType is staff
            {
                flocation = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Staff.xml"); // find file location
            }
            else if(userType == "Member") 
            {
               flocation = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Member.xml"); // find file location
            }
            if(File.Exists(flocation))  // check if file exists
            {
                

                    XmlDocument xd = new XmlDocument(); // create new xml document
                    xd.Load(flocation); // loads xml document
                    XmlNode node = xd; // create xml node
                    XmlNodeList children = node.ChildNodes; // get child nodes
                    XmlNode credentials = children[1]; // 


                    foreach (XmlNode child in credentials)
                    {
                        System.Diagnostics.Debug.WriteLine($"Child node name is {child.Name}");
                        if (child.Name == "user") // check for child node 
                        {
                            XmlNode user = child.ChildNodes[0]; // get username
                            XmlNode pwd = child.ChildNodes[1]; // get password
                           //System.Diagnostics.Debug.WriteLine($"Username is {user.InnerText}, Password is {pwd.InnerText}"); 
                            if (username == user.InnerText && password == pwd.InnerText) // check if entered credentials match any in the XML file
                            {
                                return true; // found user
                            }


                        }
                    }

            }
                
               
            return false; // did not find user

        }
    }
}