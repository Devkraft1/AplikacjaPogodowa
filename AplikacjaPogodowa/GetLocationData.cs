using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pogoda
{
    public class LocationData
    {
        [JsonPropertyName("results")]
        public List<LocationResult>? Results { get; set; }
    }
    public class LocationResult
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }
        [JsonPropertyName("country_code")]
        public string? Country_code { get; set; }
        [JsonPropertyName("admin1")]
        public string? Admin1 { get; set; }
    }
    class GetLocationData
    {
        private static HttpClient client = new HttpClient();
        public static async Task<LocationData?> LoadData(string name)
        {
            string url = $"https://geocoding-api.open-meteo.com/v1/search?name={name}&count=10&language=en&format=json";
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<LocationData>(json);
        }
    }
}
