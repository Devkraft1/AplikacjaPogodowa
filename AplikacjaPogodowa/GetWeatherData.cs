using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
namespace Pogoda
{
    public class WeatherData
    {
        [JsonPropertyName("current")]
        public currentWeather? CurrentWeather { get; set; }
        [JsonPropertyName("hourly")]
        public hourlyWeather? HourlyWeather { get; set; }
        [JsonPropertyName("daily")]
        public dailyWeather? DailyWeather { get; set; }
    }
    public class currentWeather
    {
        [JsonPropertyName("time")]
        public string? Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public double? Temperature { get; set; }
        [JsonPropertyName("weather_code")]
        public int? WeatherCode { get; set; }
        [JsonPropertyName("relative_humidity_2m")]
        public double? Humidity { get; set; }
        [JsonPropertyName("pressure_msl")]
        public double? Pressure { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public double? WindSpeed { get; set; }
        [JsonPropertyName("wind_direction_10m")]
        public double? WindDirection { get; set; }
        [JsonPropertyName("apparent_temperature")]
        public double? ApparentTemperature { get; set; }
    }
    public class hourlyWeather
    {
        [JsonPropertyName("time")]
        public List<string>? Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public List<double>? Temperature { get; set; }
        [JsonPropertyName("weather_code")]
        public List<int>? WeatherCode { get; set; }
        [JsonPropertyName("rain")]
        public List<double>? Rain { get; set; }
        [JsonPropertyName("snowfall")]
        public List<double>? Snowfall { get; set; }
        [JsonPropertyName("wind_speed_10m")]
        public List<double>? WindSpeed { get; set; }
        [JsonPropertyName("wind_direction_10m")]
        public List<double>? WindDirection { get; set; }
        [JsonPropertyName("apparent_temperature")]
        public List<double>? ApparentTemperature { get; set; }
    }
    public class dailyWeather
    {
        [JsonPropertyName("time")]
        public List<string>? Time { get; set; }
        [JsonPropertyName("weather_code")]
        public List<int>? WeatherCode { get; set; }
        [JsonPropertyName("temperature_2m_max")]
        public List<double>? TemperatureMax { get; set; }
        [JsonPropertyName("temperature_2m_min")]
        public List<double>? TemperatureMin { get; set; }
        [JsonPropertyName("sunrise")]
        public List<string>? Sunrise { get; set; }
        [JsonPropertyName("sunset")]
        public List<string>? Sunset { get; set; }
        [JsonPropertyName("moonrise")]
        public List<string>? Moonrise { get; set; }
        [JsonPropertyName("moonset")]
        public List<string>? Moonset { get; set; }
        [JsonPropertyName("moon_phase")]
        public List<double>? MoonPhase { get; set; }
        [JsonPropertyName("rain_sum")]
        public List<double>? RainSum { get; set; }
        [JsonPropertyName("snowfall_sum")]
        public List<double>? SnowfallSum { get; set; }
    }
    class GetWeatherData
    {
        private static HttpClient client = new HttpClient();
        public static async Task<WeatherData?> LoadData(
        double lat, double lon)
        {
            string latitude = lat.ToString(CultureInfo.InvariantCulture);
            string longitude = lon.ToString(CultureInfo.InvariantCulture);
            string url =
                $"https://api.open-meteo.com/v1/forecast" +
                $"?latitude={latitude}&longitude={longitude}" +
                $"&daily=weather_code,temperature_2m_max,temperature_2m_min,sunrise,sunset,moonrise,moonset,moon_phase,rain_sum,snowfall_sum" +
                $"&hourly=temperature_2m,weather_code,rain,snowfall,wind_speed_10m,wind_direction_10m,apparent_temperature" +
                $"&current=temperature_2m,weather_code,relative_humidity_2m,pressure_msl,wind_speed_10m,wind_direction_10m,apparent_temperature" +
                $"&timezone=auto";
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<WeatherData>(json);
        }
    }
}