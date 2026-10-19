using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
namespace Pogoda
{
    class UpdateDailyWeather
    {
        public static void Update(WeatherData weather, Grid dailyForecast)
        {
            dailyForecast.Children.Clear();
            string dayName = "";
            int row = 0, col = 0;
            for(int i = 0; i < 7; i++)
            {
                if (i == 0) dayName = "Dzisiaj";
                else
                {
                    DateTime date = DateTime.Parse(weather.DailyWeather!.Time![i]);
                    dayName = date.ToString("dddd", new CultureInfo("pl-PL"));
                    dayName = char.ToUpper(dayName[0], new CultureInfo("pl-PL")) + dayName.Substring(1);
                }
                HorizontalStackLayout day = new HorizontalStackLayout();
                Border dayBorder = new Border
                {
                    Stroke = Colors.Gray,
                    StrokeThickness = 1,
                    Margin = new Thickness(1),
                    Content = day,
                };
                day.Children.Add(new Label
                {
                    FontSize = 40,
                    Text = WeatherCodeHelper.GetIcon(weather.DailyWeather?.WeatherCode?[i] ?? 0),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    Padding = new Thickness(5,0,2,0)
                });
                day.Children.Add(new Label
                {
                    FontSize = 20,
                    Text = dayName,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    Padding = new Thickness(0, 0, 20, 0)
                });
                day.Children.Add(new Label
                {
                    FontSize = 20,
                    Text = $"\ue353{weather.DailyWeather?.TemperatureMax?[i]}\u00B0C \ue340{weather.DailyWeather?.TemperatureMin?[i]}\u00B0C",
                    HorizontalTextAlignment = TextAlignment.Start,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalTextAlignment = TextAlignment.Center,
                });
                Grid.SetRow(dayBorder, row);
                Grid.SetColumn(dayBorder, col);
                row++;
                dailyForecast.Children.Add(dayBorder);
            }
        }
    }
}
