using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1_Assignment5
{
    // Criminal class - defined once, shared across all pages
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

    // Database manager - shared across all pages via session
    public static class CriminalDataBase
    {
        private const string seskey = "AddedCriminal";

        public static List<Criminal_Scum> GetCriminals(HttpSessionState session)
        {
            if (session[seskey] == null)
                session[seskey] = new List<Criminal_Scum>();
            return (List<Criminal_Scum>)session[seskey];
        }

        public static void AddCriminal(HttpSessionState session, Criminal_Scum cs)
        {
            var list = GetCriminals(session);
            list.Add(cs);
            session[seskey] = list;
        }

        public static void ExecutedCriminal(HttpSessionState session, string CriminalName)
        {
            var jail_list = GetCriminals(session);
            for (int j = 0; j < jail_list.Count; j++)
            {
                if (jail_list[j].criminal_name == CriminalName)
                {
                    jail_list.RemoveAt(j);
                    break;
                }
            }
            session[seskey] = jail_list;
        }

        // Add default criminals only once per session
        public static void AddDefaultCriminals(HttpSessionState session)
        {
            if (session[seskey] == null)
            {
                AddCriminal(session, new Criminal_Scum("Joe Dirt", "walking an unlicensed meteor", "Idaho"));
                AddCriminal(session, new Criminal_Scum("Alexa Echo", "recording when only one party gave consent", "Florida"));
                AddCriminal(session, new Criminal_Scum("Drew lisp", "Lemonade stand", "Arizona"));
            }
        }
    }

    // Criminal database management page
    public partial class Criminaldb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add defaults only if session is fresh
            CriminalDataBase.AddDefaultCriminals(Session);

            if (!IsPostBack)
                RefreshList();

            // null check for Application state
            if (Application["WebsiteStartTime"] != null)
            {
                DateTime session_start = (DateTime)Application["WebsiteStartTime"];
                session_lb.Text = "Session Start Time: " + session_start.ToString("MM/dd hh:mm tt");
            }

            // null check for TotalCriminalsAdded
            if (Application["TotalCriminalsAdded"] == null)
                Application["TotalCriminalsAdded"] = 0;
            count_lb.Text = "Total Criminals Booked: " + Application["TotalCriminalsAdded"].ToString();
        }

        public void RefreshList()
        {
            Criminaldb_lb.DataSource = CriminalDataBase.GetCriminals(Session);
            Criminaldb_lb.DataBind();
        }

        public void ClearText()
        {
            Criminalinfo_label.Text = "";
            name_tb.Text = "";
            crime_tb.Text = "";
            state_tb.Text = "";
        }

        protected void selected_criminal(object sender, EventArgs e)
        {
            string select_criminal = Criminaldb_lb.SelectedValue;
            List<Criminal_Scum> criminals = CriminalDataBase.GetCriminals(Session);
            Criminal_Scum criminal_record = criminals.Find(c => c.criminal_name == select_criminal);

            if (criminal_record != null)
            {
                Criminalinfo_label.Text = "Criminal Name: " + criminal_record.criminal_name + "<br/>" +
                    " Crime committed: " + criminal_record.crime + "<br/>" +
                    " Original state: " + criminal_record.crime_location + "<br/>" +
                    " Time booked: " + criminal_record.processed_time.ToString("MM/dd/yyyy hh:mm tt");
            }
        }

        protected void addcriminal_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(name_tb.Text) &&
                !string.IsNullOrEmpty(crime_tb.Text) &&
                !string.IsNullOrEmpty(state_tb.Text))
            {
                CriminalDataBase.AddCriminal(Session, new Criminal_Scum(name_tb.Text, crime_tb.Text, state_tb.Text));
                ClearText();
                RefreshList();

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
                RefreshList();
            }
        }

        protected void home_btm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void file_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedStaff/File-analysis.aspx");
        }
    }
}