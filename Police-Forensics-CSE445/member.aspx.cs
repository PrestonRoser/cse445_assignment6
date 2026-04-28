using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Police_Forensics_CSE445
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshList();
            }
            
            

        
        //Session_start();
   
        }
        protected void selected_criminal(object sender, EventArgs e)
        {

            string select_criminal = Criminaldb_lb.SelectedValue;
            List<Criminal_Scum> criminals = CriminalDataBase.GetCriminals(Session);
            Criminal_Scum criminal_record = criminals.Find(criminal => criminal.criminal_name == select_criminal);

            if (criminal_record != null)
            {
                Criminalinfo_label.Text = "Criminal Name: " + criminal_record.criminal_name + "<br/>" +
                    " Crime commited: " + criminal_record.crime + "<br/>" +
                    " Original state: " + criminal_record.crime_location + "<br/>" +
                    " Time booked: " + criminal_record.processed_time.ToString("MM/dd/yyyy hh:mm tt");
            }

        }


        public void Session_start()
        {
            //checking is cookie session is null
            if (Session["Member"] == null)
            {
                //Might need to change this to the correct name if the page is different
                Response.Redirect("Login.aspx");
            }
            user_lb.Text = "Welcome " + Session["Member"].ToString() + "!";
        }
        public void RefreshList()
        {
            Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
            Criminaldb_lb.DataBind();
        }

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}