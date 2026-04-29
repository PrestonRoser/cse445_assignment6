using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1_Assignment5;

namespace Police_Forensics_CSE445
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session_start();    
        }
        
        public void Session_start()
        {
                       //checking is cookie session is null
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Staff")
            {
                //Might need to change this to the correct name if the page is different
                Response.Redirect("~/Default.aspx");
            }
            user_lb.Text = "Welcome " + "Staff!";
        }
        protected void logout_btn_Click(object sender, EventArgs e)
        {
            //leaving session and redirecting to login page
            Global.ClearUserSession(Session);
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void criminalman_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Criminal_database.aspx");
            
        }

        protected void file_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("File-analysis.aspx");
        }

        
    }
}