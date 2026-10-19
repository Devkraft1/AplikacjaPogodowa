using System;
using System.Collections.Generic;
using System.Text;

namespace Pogoda
{
    public class UpdateHourlyWeather
    {
        public static void Update(WeatherData weather, Grid hourlyForecast)
        {
            hourlyForecast.Children.Clear();
            int i = 0, row = 0, column = 0;
            while (weather.CurrentWeather?.Time?.Substring(0, 13) != weather.HourlyWeather?.Time?[i].Substring(0, 13))
            {
                i++;
            }
            for(int j = i; j < i+24; j++)
            {
                Border hourBorder = new Border { 
                    Stroke = Colors.Gray,
                    StrokeThickness = 1,
                    Margin = new Thickness(1)
                };
                VerticalStackLayout hour = new VerticalStackLayout();
                hourBorder.Content = hour;
                Grid.SetRow(hourBorder, row);
                Grid.SetColumn(hourBorder, column);
                hour.Children.Add(new Label
                {
                    Text = weather.HourlyWeather?.Time?[j].Substring(11, 5),
                    HorizontalTextAlignment = TextAlignment.Center
                });
                hour.Children.Add(new Label { 
                    Text = WeatherCodeHelper.GetIcon(weather.HourlyWeather?.WeatherCode?[j] ?? 0),
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 24
                });
                hour.Children.Add(new Label
                {
                    Text = $"{weather.HourlyWeather?.Temperature?[j]}°C",
                    HorizontalTextAlignment = TextAlignment.Center
                });
                hour.Children.Add(new Label
                {
                    Text = $"\ue318{weather.HourlyWeather?.Rain?[j]}",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 24
                });
                hour.Children.Add(new Label
                {
                    Text = $"\ue31a{weather.HourlyWeather?.Snowfall?[j]}",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 24
                });
                hourlyForecast.Children.Add(hourBorder);
                column++;
            }
        }
    }
}
