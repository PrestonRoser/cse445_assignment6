using System;

namespace CrimeRiskWeb.UserControls
{
    /// <summary>
    /// Simple user control that generates and validates a math CAPTCHA.
    /// The expected answer is stored in ViewState so it survives postback.
    /// </summary>
    public partial class CaptchaControl : System.Web.UI.UserControl
    {
        private const string CaptchaAnswerKey = "CaptchaAnswer";

        /// <summary>
        /// Ensures the control has a CAPTCHA prompt on first page load.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }

        /// <summary>
        /// Regenerates the CAPTCHA when the user clicks the refresh button.
        /// </summary>
        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        /// <summary>
        /// Returns true when the user's answer matches the current CAPTCHA answer.
        /// </summary>
        public bool IsValid()
        {
            int expected;
            int actual;

            if (ViewState[CaptchaAnswerKey] == null)
            {
                return false;
            }

            if (!int.TryParse(ViewState[CaptchaAnswerKey].ToString(), out expected))
            {
                return false;
            }

            if (!int.TryParse(txtAnswer.Text.Trim(), out actual))
            {
                return false;
            }

            return actual == expected;
        }

        /// <summary>
        /// Creates a new simple addition prompt and stores the answer in ViewState.
        /// </summary>
        public void GenerateCaptcha()
        {
            Random rng = new Random(Guid.NewGuid().GetHashCode());
            int left = rng.Next(1, 10);
            int right = rng.Next(1, 10);

            ViewState[CaptchaAnswerKey] = left + right;
            lblPrompt.Text = "CAPTCHA: What is " + left + " + " + right + "?";
            txtAnswer.Text = string.Empty;
        }
    }
}
