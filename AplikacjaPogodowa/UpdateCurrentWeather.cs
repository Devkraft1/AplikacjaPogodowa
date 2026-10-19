using System;
using System.Collections.Generic;
using System.Text;

namespace Pogoda
{
    public class UpdateCurrentWeather
    {
        public static void Update(WeatherData weather, Label currentTemp, Label currentWeatherCodeText,Label feelsLike, Label currentWindData, Label currentHumidity, Label currentPressure)
        {
            currentTemp.Text = $"{weather.CurrentWeather?.Temperature}\u00B0C";
            currentWeatherCodeText.Text = $"{WeatherCodeHelper.GetDescription(weather.CurrentWeather?.WeatherCode ?? 0)}";
            feelsLike.Text = $"{weather.CurrentWeather?.ApparentTemperature}\u00B0C";
            currentWindData.Text = $"\ue27e {weather.CurrentWeather?.WindSpeed}km/h{WindDirectionHelper.GetWindDirectionIcon(weather.CurrentWeather?.WindDirection ?? 0)}";
            currentHumidity.Text = $"\ue373 {weather.CurrentWeather?.Humidity}%";
            currentPressure.Text = $"\ue372{weather.CurrentWeather?.Pressure} hPa";
        }
    }
}
