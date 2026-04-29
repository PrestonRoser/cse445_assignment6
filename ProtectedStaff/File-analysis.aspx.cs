using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net;


namespace Police_Forensics_CSE445
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime session_start = (DateTime)Application["WebsiteStartTime"];
            session_lb.Text = "Session Start Time: " + DateTime.Now.ToString("MM/dd hh:mm tt");
        }

        protected void scan_btn_Click(object sender, EventArgs e)
        {
            var file_ceral = new JavaScriptSerializer();
            if (!fileupload.HasFile)
            {
                results_lb.Text = "Please select a file to scan...";
                return;
            }
            string uploadedfile_name = fileupload.FileName;
            byte[] forensic_file_bytes = fileupload.FileBytes;
            SHA256 sha256 = SHA256.Create();
            byte[] file_hash_bytes = sha256.ComputeHash(forensic_file_bytes);
            string file_hash_string = BitConverter.ToString(file_hash_bytes).Replace("-", "").ToLower();

            //results_lb.Text = "File Hash: " + file_hash_string;


            WebClient virustotal_client = new WebClient();
            virustotal_client.Headers.Add("x-apikey", ConfigurationManager.
                AppSettings["VirusTotal"]);

            string virustotal_url = "https://www.virustotal.com/api/v3/files/" + file_hash_string;
            try
            {
                string virustotal_rs = virustotal_client.DownloadString(virustotal_url);
                dynamic trust_worthy = file_ceral.DeserializeObject(virustotal_rs);
                var community_score = trust_worthy["data"]["attributes"]["last_analysis_stats"];
                int malicious = community_score["malicious"];
                int undetected = community_score["undetected"];
                int suspicious = community_score["suspicious"];
                string virustotal_fullresults = "";


                int total_badscore = malicious + suspicious;
                int total_score = malicious + undetected + suspicious;
                int perc_score = (total_badscore / total_score) * 100;
                if (perc_score < 10)
                {
                    virustotal_fullresults += "the file is safe";
                }
                    if (perc_score > 10  && perc_score < 50)
                {
                    virustotal_fullresults += "The file is greater than 10% please look closely and run in sandbox";
                }if(perc_score > 50 && perc_score < 90)
                {
                    virustotal_fullresults += "This file is greater than 50% highly suspicious, do not open on your own computer";
                }if(perc_score > 90)
                {
                    virustotal_fullresults += "The file has a very high chance of being malware, with percentage greater than 90%";
                }


                /*Application["TotalScans"] = (int)Application["TotalScans"] + 1;
                scancount_lb.Text = "Total Files scanned: " + Application["TotalScans"].ToString();*/

                    results_lb.Text = "File: " + uploadedfile_name + "<br/>"+ "VirusTotal report: " + total_badscore + "/" + total_score + "<br/>"
                    + "Break down of results: " + perc_score + "%" + "<br/>" + virustotal_fullresults;


            }
            catch(Exception e2)
            {
                results_lb.Text = "Virustotal returned an error: " + e2.Message;
            }



        }

        protected void home_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void criminalman_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Criminal_database.aspx");
        }
    }
}