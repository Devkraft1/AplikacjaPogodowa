using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Maui.Devices;
namespace Pogoda
{
    public class UpdateHourlyWeather
    {
        public static void Update(WeatherData weather, Grid hourlyForecast, ScrollView hourlyScroll)
        {
            double columnWidth = hourlyScroll.Width / 5.0;
            hourlyForecast.ColumnDefinitions.Clear();
            for (int c = 0; c < 24; c++)
            {
                hourlyForecast.ColumnDefinitions.Add(new ColumnDefinition(columnWidth));
            }
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
                VerticalStackLayout hour = new VerticalStackLayout { 
                Padding = new Thickness(0,0,0,5)};
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
                    Text = $"{weather.HourlyWeather?.Temperature?[j].ToString("F1", CultureInfo.InvariantCulture)}°C",
                    HorizontalTextAlignment = TextAlignment.Center
                });
                hour.Children.Add(new Label
                {
                    Text = $"\ue318{weather.HourlyWeather?.Rain?[j].ToString("F1", CultureInfo.InvariantCulture)}",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 20
                });
                hour.Children.Add(new Label
                {
                    Text = $"\ue31a{weather.HourlyWeather?.Snowfall?[j].ToString("F1", CultureInfo.InvariantCulture)}",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 20
                });
                hourlyForecast.Children.Add(hourBorder);
                column++;
            }
        }
    }
}
