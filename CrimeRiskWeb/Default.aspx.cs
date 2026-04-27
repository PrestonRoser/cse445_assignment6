using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace CrimeRiskWeb
{
    /// <summary>
    /// Code-behind for the main TryIt and summary page.
    /// This page demonstrates all required Assignment 5 elements from one interface.
    /// </summary>
    public partial class _Default : Page
    {
        /// <summary>
        /// Base URL for the REST API. Replace the localhost value with your WebStrar
        /// deployment URL once the REST service is published.
        /// </summary>
        private const string RestBaseUrl = "https://webstrarportal.fulton.asu.edu/sites/website117/Page2/api/crimerisk";

        /// <summary>
        /// Sets the displayed service URL and current counters when the page loads.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litServiceUrl.Text = RestBaseUrl;
                RefreshMetrics();
            }
        }

        /// <summary>
        /// Loads a few sample values into the page so the grader can test quickly.
        /// </summary>
        protected void btnLoadSample_Click(object sender, EventArgs e)
        {
            txtZipCode.Text = "85004";
            txtCity.Text = "Phoenix";
            txtState.Text = "AZ";
            txtLatitude.Text = "33.4484";
            txtLongitude.Text = "-112.0740";
            lblStatus.Text = "Sample values loaded.";
        }

        /// <summary>
        /// Clears all input and output areas on the page.
        /// </summary>
        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            txtZipCode.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtLatitude.Text = string.Empty;
            txtLongitude.Text = string.Empty;

            litRegion.Text = string.Empty;
            litScore.Text = string.Empty;
            litRiskLevel.Text = string.Empty;
            lblStatus.Text = string.Empty;

            CrimeCaptcha.GenerateCaptcha();
            RefreshMetrics();
        }

        /// <summary>
        /// Validates the CAPTCHA, builds the service URL from whichever input mode
        /// the user supplied, calls the REST service, and displays the parsed results.
        /// </summary>
        protected async void btnGetRisk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CrimeCaptcha.IsValid())
                {
                    lblStatus.Text = "CAPTCHA validation failed. Please solve the math question and try again.";
                    CrimeCaptcha.GenerateCaptcha();
                    return;
                }

                string requestUrl = BuildRequestUrl();
                litServiceUrl.Text = HttpUtility.HtmlEncode(requestUrl);

                CrimeRiskResult result = await GetCrimeRiskAsync(requestUrl);

                litRegion.Text = HttpUtility.HtmlEncode(result.Region);
                litScore.Text = result.Score.ToString();
                litRiskLevel.Text = HttpUtility.HtmlEncode(result.RiskLevel);

                Global.IncrementSearchCount(Application);
                RefreshMetrics();

                lblStatus.Text = "Crime risk lookup completed successfully.";
                CrimeCaptcha.GenerateCaptcha();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Request failed: " + ex.Message;
                CrimeCaptcha.GenerateCaptcha();
            }
        }

        /// <summary>
        /// Builds the outgoing GET request URL using the first valid supported input mode.
        /// Priority order: ZIP, city/state, then coordinates.
        /// </summary>
        private string BuildRequestUrl()
        {
            string zip = txtZipCode.Text.Trim();
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string latitude = txtLatitude.Text.Trim();
            string longitude = txtLongitude.Text.Trim();

            if (!string.IsNullOrWhiteSpace(zip))
            {
                return RestBaseUrl + "?zipCode=" + HttpUtility.UrlEncode(zip);
            }

            if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(state))
            {
                return RestBaseUrl
                    + "?city=" + HttpUtility.UrlEncode(city)
                    + "&state=" + HttpUtility.UrlEncode(state);
            }

            if (!string.IsNullOrWhiteSpace(latitude) && !string.IsNullOrWhiteSpace(longitude))
            {
                return RestBaseUrl
                    + "?latitude=" + HttpUtility.UrlEncode(latitude)
                    + "&longitude=" + HttpUtility.UrlEncode(longitude);
            }

            throw new InvalidOperationException("Enter a ZIP code, city/state, or both latitude and longitude.");
        }

        /// <summary>
        /// Performs the GET request and deserializes the returned JSON payload into
        /// a simple strongly typed model for the page.
        /// </summary>
        private async Task<CrimeRiskResult> GetCrimeRiskAsync(string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(requestUrl);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Deserialize<CrimeRiskResult>(response);
            }
        }

        /// <summary>
        /// Refreshes the visible visit and search counters from application state.
        /// </summary>
        private void RefreshMetrics()
        {
            litTotalVisits.Text = Convert.ToString(Application["TotalVisits"]);
            litTotalSearches.Text = Convert.ToString(Application["TotalSearches"]);
        }

        /// <summary>
        /// Local page model for deserializing the REST response.
        /// </summary>
        private class CrimeRiskResult
        {
            public int Score { get; set; }
            public string RiskLevel { get; set; }
            public string Region { get; set; }
        }
    }
}
