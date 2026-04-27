using System;

namespace CrimeRiskRest.Models
{
    /// <summary>
    /// Request model for the crime risk REST service.
    /// Supports several input styles so the TryIt page can remain simple:
    /// ZIP code, city/state, or explicit latitude/longitude coordinates.
    /// </summary>
    public class LocationRequest
    {
        /// <summary>
        /// ZIP code entered by the user.
        /// Example: 85004
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// City entered by the user.
        /// Example: Phoenix
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// State entered by the user.
        /// Example: AZ
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Latitude value used when the browser provides location access.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude value used when the browser provides location access.
        /// </summary>
        public double? Longitude { get; set; }
    }
}
