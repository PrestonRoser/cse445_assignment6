using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Police_Forensics_CSE445
{
   
    public partial class WebForm2 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
                Criminaldb_lb.DataBind();
            }
            DateTime session_start = (DateTime)Application["WebsiteStartTime"];
            session_lb.Text = "Session Start Time: " + session_start.ToString("MM/dd hh:mm tt");
            
        }
        protected void selected_criminal(object sender, EventArgs e)
        {

            string select_criminal = Criminaldb_lb.SelectedValue;
            List<Criminal_Scum> criminals = CriminalDataBase.GetCriminals(Session);
            Criminal_Scum criminal_record = criminals.Find(criminal => criminal.criminal_name == select_criminal);

            if(criminal_record != null)
            {
                Criminalinfo_label.Text = "Criminal Name: " + criminal_record.criminal_name + "<br/>" +
                    " Crime commited: " + criminal_record.crime + "<br/>" +
                    " Original state: " + criminal_record.crime_location + "<br/>" +
                    " Time booked: " + criminal_record.processed_time.ToString("MM/dd/yyyy hh:mm tt"); 
            }
            
        }
        
        public void ClearText()
        {
            Criminalinfo_label.Text = "";
            name_tb.Text = "";
            crime_tb.Text = "";
            state_tb.Text = "";
        }
        public void RefreshList()
        {
            Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
            Criminaldb_lb.DataBind();
        }
        protected void addcriminal_btn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(name_tb.Text) && !string.IsNullOrEmpty(crime_tb.Text) && !string.IsNullOrEmpty(state_tb.Text))
            {
                CriminalDataBase.AddCriminal(Session, new Criminal_Scum(name_tb.Text, crime_tb.Text, state_tb.Text));
                ClearText();
                Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
                Criminaldb_lb.DataBind();

                Application["TotalCriminalsAdded"] = (int)Application["TotalCriminalsAdded"] + 1;
                count_lb.Text = "Total Criminals Booked: " + Application["TotalCriminalsAdded"].ToString();
            }
        }

        protected void release_btn_Click(object sender, EventArgs e)
        {
            string select_criminal = Criminaldb_lb.SelectedValue;
            if (!string.IsNullOrEmpty(select_criminal))
            {
                CriminalDataBase.ExecutedCriminal(Session, select_criminal);
                ClearText();
                //Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
                //Criminaldb_lb.DataBind();
                RefreshList();
            }
        }

        protected void home_btm_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void file_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("File-analysis.aspx");
        }
    }
}