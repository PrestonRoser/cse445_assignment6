using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Police_Forensics_CSE445
{
    public class Criminal_Scum
    {
        public string criminal_name { get; set; }
        public string crime { get; set; }
        public string crime_location { get; set; }
        public DateTime processed_time { get; set; }
        public Criminal_Scum(string name, string broken_law, string location)
        {
            criminal_name = name;
            crime = broken_law;
            crime_location = location;
            processed_time = DateTime.Now;
        }
    }

    public static class  CriminalDataBase 
    {

        // making a key to better track session so we can add default criminals into the program
        private const string seskey = "AddedCriminal";


        //making a list of criminals to be added into the database, relating it back to the session
        public static List<Criminal_Scum> GetCriminals(HttpSessionState session)
        {

            //if seskey is empty, like it doesn't have AddedCriminal, then we create the list
            if(session[seskey] == null)
            {
                session[seskey] = new List<Criminal_Scum>();
                
            }
            //returning the list of criminals whether it's empty or not
            return (List<Criminal_Scum>)session[seskey];
        }

        public static void AddCriminal(HttpSessionState session, Criminal_Scum cs)
        {
            //grabbing the list of criminals for the session and adding a new criminal to it, then updating the session with new list
            var list = GetCriminals(session);
            //this is where we add the criminal
            list.Add(cs);
            //updating session with new list
            session[seskey] = list;
            

        }
        public static void ExecutedCriminal(HttpSessionState session, string CriminalName)
        {
            //grabbing list of criminals 
            var jail_list = GetCriminals(session);
            for(int j = 0; j < jail_list.Count; j++)
            {
                if (jail_list[j].criminal_name == CriminalName)
                {
                    jail_list.RemoveAt(j);
                    break;
                }
            }
            //jail_list.RemoveAll(criminal => criminal.criminal_name == CriminalName);
            session[seskey] = jail_list;
        }
    }

    public partial class WebForm1 : System.Web.UI.Page
    {
        

        protected void page_load(object sender, EventArgs e)
        {
            if ((Session.Count == 0))
            {
                CriminalDataBase.AddCriminal(Session, new Criminal_Scum("Joe Dirt", "walking an unlicensed meteor", "Idaho"));
                CriminalDataBase.AddCriminal(Session, new Criminal_Scum("Alexa Echo", "recording when only one party gave consent", "Florida"));
                CriminalDataBase.AddCriminal(Session, new Criminal_Scum("Drew lisp", "Lemonade stand", "Arizona"));
            }
            
           /* //now adding the source to listview so we can see all criminals
            Criminaldb_listview.DataSource = CriminalDataBase.GetCriminals(Session);
            
            //now binding the data
            Criminaldb_listview.DataBind();*/
            if (!IsPostBack)
            {
                RefreshList();
            }
            DateTime session_start = (DateTime)Application["WebsiteStartTime"];
            session_lb.Text = "Session Start Time: " + session_start.ToString("MM/dd hh:mm tt");


        }
        //making a method to refresh the list of criminals
        public void RefreshList()
        {
            //refreshing list of criminals to be shown
            Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
            Criminaldb_lb.DataBind();
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