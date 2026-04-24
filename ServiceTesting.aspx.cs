using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplication1_Assignment5.ServiceReference1;

namespace WebApplication1_Assignment5
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UrlButton_Click(object sender, EventArgs e)
        {
            // unsafe url test: https://httpbin.org/get?text=phishing
            // safe url test: https://venus.sod.asu.edu/webhome/teaching/honors.html


            var client = new ServiceReference1.Service1Client(); // create client
            string url = UrlTextBox.Text; // get url from text box

            bool result = client.isSafeUrl(url); // call service
            //System.Diagnostics.Debug.WriteLine(result);
            if(result == true)
            {
                SafeLabel.Text = "This url is safe"; // set message 
                SafeLabel.ForeColor = System.Drawing.Color.Green; // set label color to green
            }
            else
            {
                SafeLabel.Text = "This url is unsafe"; // set message
                SafeLabel.ForeColor = System.Drawing.Color.Red; // set label color to red

            }

        }
    }
}