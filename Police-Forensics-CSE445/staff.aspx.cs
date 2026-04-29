using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Police_Forensics_CSE445
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session_start();    
        }
        
        public void Session_start()
        {
                       //checking is cookie session is null
            if (Session["Staff"] == null)
            {
                //Might need to change this to the correct name if the page is different
                Response.Redirect("Login.aspx");
            }
            user_lb.Text = "Welcome " + Session["Staff"].ToString() + "!";
        }
        protected void logout_btn_Click(object sender, EventArgs e)
        {
            //leaving session and redirecting to login page
            Session.Abandon();
            Response.Redirect("Login.aspx");
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