using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

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
            Session["GoToPage"] = "ProtectedStaff/StaffPage.aspx"; // create session object 

            Session["UserType"] = "Staff"; // userType is staff
            Response.Redirect("ProtectedStaff/StaffPage.aspx");
            //Object staffUsername = Session["Staffusername"];// get member username
            //Object staffPassword = Session["Staffpassword"]; // get member password

            //if(staffUsername == null || staffPassword == null) // check if staff username or password is null
            //{
            //    Response.Redirect("Login.aspx");
            //}


            //if (staffUsername.ToString() == "" || staffPassword.ToString() == "") { // check if username or password is empty
            //    Response.Redirect("Login.aspx"); // redirect to Login.aspx 
            //}
            //else
            //{
            //    Response.Redirect("ProtectedStaff/StaffPage.aspx"); // redirect to staff page
            //}

        }

        protected void memberButton_Click(object sender, EventArgs e)
        {
            Session["GoToPage"] = "ProtectedMember/MemberPage.aspx"; // create session object
            Session["UserType"] = "Member"; // userType is Member
            Response.Redirect("ProtectedMember/MemberPage.aspx"); // redirect to member page

            //Object memberUsername = Session["Memberusername"]; // get member username
            //Object memberPassword = Session["Memberpassword"]; // get member password




            //if (memberUsername == null || memberPassword == null) // check if username or password is null
            //{
            //    Response.Redirect("Login.aspx");

            //}


            //if (memberUsername.ToString() == "" || memberPassword.ToString() == "") // check if username or pasword is empty
            //{ 
            //    Response.Redirect("Login.aspx"); // redirect to Login.aspx 
            //}
            //else
            //{
            //    Response.Redirect("ProtectedMember/MemberPage.aspx"); // redirect to member page
            //}

        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            System.Diagnostics.Debug.WriteLine("User signed out");
            //Session.Clear();
            //Session.Abandon();
            //registerMember("Member2", "abc123!");
            Response.Redirect("Default.aspx");
        }


        void registerMember(string username, string password)
        {
            string flocation = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Member.xml"); ;
            if (File.Exists(flocation))  // check if file exists
            {
                    XmlDocument xd = new XmlDocument(); // create new xml document
                    xd.Load(flocation); // loads xml document

                    
                    XmlNode node = xd; // create xml node
                    XmlNodeList children = node.ChildNodes; // get child nodes
                    XmlNode credentialsNode = children[1]; // 

                    XmlNode userNode = xd.CreateElement("user");

                    XmlNode userNameNode = xd.CreateElement("username");
                    userNameNode.InnerText = username;

                    XmlNode passwordNode = xd.CreateElement("password");
                    passwordNode.InnerText = password;

                    userNode.AppendChild(userNameNode);
                    userNode.AppendChild(passwordNode);

                    credentialsNode.AppendChild(userNode);

                    xd.Save(flocation);
                
            }
        }

        protected void RegisterMemberButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterNewMember.aspx"); // redirect to register new member page if register button clicked
        }
    }
}