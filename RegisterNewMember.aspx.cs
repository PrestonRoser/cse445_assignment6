using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebApplication1_Assignment5
{
    public partial class RegisterNewMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;
            if(username == "" || password == "")
            {
                Output.Text = "Please enter a username and password";

            }
           bool flag = registerMember(username, password);
            if(flag)
            {
                Output.Text = "Successfully registered member"; 
            }
            else
            {
                Output.Text = "Couldn't register member, member with this username already exists";
            }
            
        }

        bool registerMember(string username, string password)
        {
            string flocation = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Member.xml"); ;
            if (File.Exists(flocation))  // check if file exists
            {
                XmlDocument xd = new XmlDocument(); // create new xml document
                xd.Load(flocation); // loads xml document

               


                XmlNode node = xd; // create xml node
                XmlNodeList children = node.ChildNodes; // get child nodes
                XmlNode credentialsNode = children[1]; //
                string duplicateCheck = $"user[username='{username}']"; // get username to check for duplicates                                 
                XmlNode userNodeExists = credentialsNode.SelectSingleNode(duplicateCheck); 
                if(userNodeExists != null)
                {
                    return false; // if user node exists return false
                }
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
            return true; 
        }
          
    }
}