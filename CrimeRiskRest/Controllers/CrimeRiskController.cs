using CrimeRiskRest.Models;
using System;
using System.Web.Http;

namespace CrimeRiskRest.Controllers
{
    /// <summary>
    /// RESTful controller that returns a lightweight synthetic crime risk score
    /// based on one of three location inputs:
    /// 1. ZIP code
    /// 2. City and state
    /// 3. Latitude and longitude
    ///
    /// The service uses deterministic scoring so the same input returns the same
    /// result. This keeps the service simple, predictable, and easy to test.
    /// </summary>
    [RoutePrefix("api/crimerisk")]
    public class CrimeRiskController : ApiController
    {
        /// <summary>
        /// GET endpoint for callers that prefer query string input.
        /// Any one of the supported input formats may be supplied.
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(
            string zipCode = null,
            string city = null,
            string state = null,
            double? latitude = null,
            double? longitude = null)
        {
            LocationRequest request = new LocationRequest
            {
                ZipCode = zipCode,
                City = city,
                State = state,
                Latitude = latitude,
                Longitude = longitude
            };

            return BuildResponse(request);
        }

        /// <summary>
        /// POST endpoint for callers that want to send JSON in the request body.
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] LocationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body cannot be empty.");
            }

            return BuildResponse(request);
        }

        /// <summary>
        /// Validates input, determines which input mode is being used, computes
        /// a deterministic score, maps it to a label, and returns a response object.
        /// </summary>
        private IHttpActionResult BuildResponse(LocationRequest request)
        {
            string region;
            int score;

            if (HasZipInput(request))
            {
                region = "ZIP " + request.ZipCode.Trim();
                score = ScoreFromZip(request.ZipCode.Trim());
            }
            else if (HasCityStateInput(request))
            {
                string cityValue = request.City.Trim();
                string stateValue = request.State.Trim().ToUpperInvariant();

                region = cityValue + ", " + stateValue;
                score = ScoreFromCityState(cityValue, stateValue);
            }
            else if (HasCoordinateInput(request))
            {
                double latitudeValue = request.Latitude.Value;
                double longitudeValue = request.Longitude.Value;

                region = string.Format("Coordinates ({0:F4}, {1:F4})", latitudeValue, longitudeValue);
                score = ScoreFromCoordinates(latitudeValue, longitudeValue);
            }
            else
            {
                return BadRequest("Provide a ZIP code, city/state, or both latitude and longitude.");
            }

            CrimeRiskResponse response = new CrimeRiskResponse
            {
                Score = score,
                RiskLevel = GetRiskLevel(score),
                Region = region
            };

            return Ok(response);
        }

        /// <summary>
        /// Returns true when the request contains a ZIP code.
        /// </summary>
        private bool HasZipInput(LocationRequest request)
        {
            return !string.IsNullOrWhiteSpace(request.ZipCode);
        }

        /// <summary>
        /// Returns true when the request contains both city and state.
        /// </summary>
        private bool HasCityStateInput(LocationRequest request)
        {
            return !string.IsNullOrWhiteSpace(request.City)
                && !string.IsNullOrWhiteSpace(request.State);
        }

        /// <summary>
        /// Returns true when the request contains both latitude and longitude.
        /// </summary>
        private bool HasCoordinateInput(LocationRequest request)
        {
            return request.Latitude.HasValue && request.Longitude.HasValue;
        }

        /// <summary>
        /// Computes a stable score from a ZIP code by summing numeric digits and
        /// applying a simple transformation into the 0-100 range.
        /// </summary>
        private int ScoreFromZip(string zipCode)
        {
            int digitSum = 0;

            foreach (char c in zipCode)
            {
                if (char.IsDigit(c))
                {
                    digitSum += (c - '0');
                }
                else
                {
                    digitSum += c;
                }
            }

            return NormalizeScore((digitSum * 11) + (zipCode.Length * 7));
        }

        /// <summary>
        /// Computes a stable score from city and state text by hashing character
        /// values in a lightweight deterministic way.
        /// </summary>
        private int ScoreFromCityState(string city, string state)
        {
            string combined = (city + "|" + state).ToLowerInvariant();
            int total = 0;

            foreach (char c in combined)
            {
                total += c;
            }

            return NormalizeScore((total * 3) + (city.Length * 5) + (state.Length * 13));
        }

        /// <summary>
        /// Computes a stable score from coordinate input.
        /// </summary>
        private int ScoreFromCoordinates(double latitude, double longitude)
        {
            int latPart = Math.Abs((int)Math.Round(latitude * 1000));
            int longPart = Math.Abs((int)Math.Round(longitude * 1000));
            int raw = (latPart * 7) + (longPart * 5);

            return NormalizeScore(raw);
        }

        /// <summary>
        /// Maps an integer into the inclusive 0-100 range.
        /// </summary>
        private int NormalizeScore(int raw)
        {
            int score = raw % 101;

            if (score < 0)
            {
                score += 101;
            }

            return score;
        }

        /// <summary>
        /// Maps the numeric score to a human-readable risk band.
        /// </summary>
        private string GetRiskLevel(int score)
        {
            if (score <= 33)
            {
                return "Low";
            }

            if (score <= 66)
            {
                return "Moderate";
            }

            return "High";
        }
    }
}