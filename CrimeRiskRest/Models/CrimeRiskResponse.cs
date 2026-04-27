namespace CrimeRiskRest.Models
{
    /// <summary>
    /// Response model returned by the crime risk service.
    /// Web API will automatically serialize this object into JSON.
    /// </summary>
    public class CrimeRiskResponse
    {
        public int Score { get; set; }

        public string RiskLevel { get; set; }

        public string Region { get; set; }
    }
}