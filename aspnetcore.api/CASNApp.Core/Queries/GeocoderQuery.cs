using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using CASNApp.Core.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CASNApp.Core.Queries
{
    public class GeocoderQuery
    {
        private readonly string apiKey;
        private readonly ILogger<GeocoderQuery> logger;

		public GeocoderQuery(string apiKey, ILogger<GeocoderQuery> logger)
        {
            this.apiKey = apiKey;
            this.logger = logger;
        }

        public async Task<LatLng> ForwardLookupAsync(string address)
        {
            try
            {
                logger.LogDebug($"address = {address.ApplyLogFormat()}");

                string encodedAddress = HttpUtility.UrlEncode(address);
                string requestUrl = $"https://maps.googleapis.com/maps/api/geocode/json?key={apiKey}&address={encodedAddress}";
                string resultString = null;

                using (var webClient = new WebClient())
                {
                    resultString = await webClient.DownloadStringTaskAsync(requestUrl);
                }

                logger.LogDebug($"{nameof(resultString)} = {resultString.ApplyLogFormat()}");

                var jObject = JObject.Parse(resultString);

                var latitude = Convert.ToDecimal((double)jObject.SelectToken("results[0].geometry.location.lat"));
                var longitude = Convert.ToDecimal((double)jObject.SelectToken("results[0].geometry.location.lng"));

                return new LatLng(latitude, longitude);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(ForwardLookupAsync)}(): Exception");
                logger.LogError($"{nameof(ForwardLookupAsync)}(): address = {address.ApplyLogFormat()}");
                return null;
            }
        }

        public class LatLng
        {
            public decimal Latitude { get; private set; }
            public decimal Longitude { get; private set; }

			public enum UnitType
			{
				Unknown = 0,
				Miles = 1,
				Kilometers = 2,
				NauticalMiles = 3,
			}

			public LatLng(decimal latitude, decimal longitude)
            {
                Latitude = latitude;
                Longitude = longitude;
            }

			public LatLng ToVagueLocation(Random random, double minOffset, double maxOffset)
            {
                random = random ?? new Random();
                var offset1 = (decimal)GetRandomDouble(random, minOffset, maxOffset);
                var offset2 = (decimal)GetRandomDouble(random, minOffset, maxOffset);

                return new LatLng(Latitude + offset1, Longitude + offset2);
            }

            private double GetRandomDouble(Random random, double min, double max)
            {
                var result = random.NextDouble() * (max - min) + min;

                // roughly 50% chance of making the result negative
                if (random.Next(0, 2) == 0)
                {
                    result = result * -1.0;
                }

                return result;
            }

			public static double GetDistance(double startLatitude, double startLongitude, double endLatitude, double endLogitude, UnitType unit)
			{
				if ((startLatitude == endLatitude) && (startLongitude == endLogitude))
				{
					return 0;
				}
				else
				{
					double theta = startLongitude - endLogitude;
					double dist = Math.Sin(deg2rad(startLatitude)) * Math.Sin(deg2rad(endLatitude)) + Math.Cos(deg2rad(startLatitude)) * Math.Cos(deg2rad(endLatitude)) * Math.Cos(deg2rad(theta));
					dist = Math.Acos(dist);
					dist = rad2deg(dist);

					//convert the distance in the proper units 
					switch (unit)
					{
						case UnitType.Kilometers:
							dist = dist * 1.609344;
							break;
						case UnitType.NauticalMiles:
							dist = dist * 0.8684;
							break;
						default:
							dist = dist * 60 * 1.1515;
							break;
					}

					return (dist);
				}
			}

			private static double deg2rad(double deg)
			{
				return (deg * Math.PI / 180.0);
			}

			private static double rad2deg(double rad)
			{
				return (rad / Math.PI * 180.0);
			}
		}

    }
}
