using System;
using System.Collections.Generic;
using System.Text;

namespace Pogoda
{
    public class UpdateSunMoondata
    {
        private static string GetMoonPhaseName(double phase)
        {
            string[] phases =
            {
                "Nów",
                "Sierp przybywający",
                "Pierwsza kwadra",
                "Garb przybywający",
                "Pełnia",
                "Garb ubywający",
                "Ostatnia kwadra",
                "Sierp ubywający"
            };
            int index = (int)Math.Round(phase * 8) % 8;

            return phases[index];
        }
        private static string GetMoonPhaseIcon(double phase)
        {
            string[] phases =
            {
                "\ue38d", // moon_new
                "\ue38e", // moon_waxing_crescent_1
                "\ue38f", // moon_waxing_crescent_2
                "\ue390", // moon_waxing_crescent_3
                "\ue391", // moon_waxing_crescent_4
                "\ue392", // moon_waxing_crescent_5
                "\ue393", // moon_waxing_crescent_6
                "\ue394", // moon_first_quarter
                "\ue395", // moon_waxing_gibbous_1
                "\ue396", // moon_waxing_gibbous_2
                "\ue397", // moon_waxing_gibbous_3
                "\ue398", // moon_waxing_gibbous_4
                "\ue399", // moon_waxing_gibbous_5
                "\ue39a", // moon_waxing_gibbous_6
                "\ue39b", // moon_full
                "\ue39c", // moon_waning_gibbous_1
                "\ue39d", // moon_waning_gibbous_2
                "\ue39e", // moon_waning_gibbous_3
                "\ue39f", // moon_waning_gibbous_4
                "\ue3a0", // moon_waning_gibbous_5
                "\ue3a1", // moon_waning_gibbous_6
                "\ue3a2", // moon_third_quarter
                "\ue3a3", // moon_waning_crescent_1
                "\ue3a4", // moon_waning_crescent_2
                "\ue3a5", // moon_waning_crescent_3
                "\ue3a6", // moon_waning_crescent_4
                "\ue3a7", // moon_waning_crescent_5
                "\ue3a8"  // moon_waning_crescent_6
            };
            int index = (int)Math.Round(phase * 28) % 28;
            return phases[index];
        }
        public static void Update(WeatherData weather, Grid SunMoonData)
        {
            string sunIcon = "\ue30d", sunrise = "\ue34c", sunset = "\ue34d";
            string moonrise = "\ue3c1", moonset = "\ue3c2";
            string moonIcon = GetMoonPhaseIcon(weather.DailyWeather?.MoonPhase?[0] ?? 0);
            string moonPhaseName = GetMoonPhaseName(weather.DailyWeather?.MoonPhase?[0] ?? 0);
            Grid sunData = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                RowDefinitions =
                    {
                        new RowDefinition(GridLength.Star), // Weather icon
                        new RowDefinition(GridLength.Star), // Name
                        new RowDefinition(GridLength.Star),  // Sunrise
                        new RowDefinition(GridLength.Star)  // Sunset
                    }
            };
            Border sunBorder = new Border
            {
                Stroke = Colors.Gray,
                StrokeThickness = 1,
                Content = sunData
            };
            sunData.Add(new Label
            {
                Text = sunIcon,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 40
            }, 0, 0);
            sunData.Add(new Label
            {
                Text = "Słońce",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 15
            }, 0, 1);
            sunData.Add(new Label
            {
                Text = sunrise + ((string.IsNullOrEmpty(weather.DailyWeather?.Sunrise?[0])) ? "brak" : weather.DailyWeather?.Sunrise?[0].Substring(11, 5)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20
            }, 0, 2);
            sunData.Add(new Label
            {
                Text = sunset + ((string.IsNullOrEmpty(weather.DailyWeather?.Sunset?[0])) ? "brak" : weather.DailyWeather?.Sunset?[0].Substring(11, 5)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20
            }, 0, 3);
            SunMoonData.Add(sunBorder, 0, 0);
            Grid moonData = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                RowDefinitions =
                    {
                        new RowDefinition(GridLength.Star), // Weather icon
                        new RowDefinition(GridLength.Star), // Name
                        new RowDefinition(GridLength.Star),  // Moonrise
                        new RowDefinition(GridLength.Star)  // Moonset
                    }
            };
            Border moonBorder = new Border
            {
                Stroke = Colors.Gray,
                StrokeThickness = 1,
                Content = moonData
            };
            moonData.Add(new Label
            {
                Text = moonIcon,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 40
            }, 0, 0);
            moonData.Add(new Label
            {
                Text = moonPhaseName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 15
            }, 0, 1);
            moonData.Add(new Label
            {
                Text = moonrise + ((string.IsNullOrEmpty(weather.DailyWeather?.Moonrise?[0])) ? "brak" : weather.DailyWeather?.Moonrise?[0].Substring(11, 5)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20
            }, 0, 2);
            moonData.Add(new Label {
                Text = moonset + ((string.IsNullOrEmpty(weather.DailyWeather?.Moonset?[0])) ? "brak" : weather.DailyWeather?.Moonset?[0].Substring(11, 5)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 20
            }, 0, 3);
            SunMoonData.Add(moonBorder, 1, 0);
        }
    }
}
