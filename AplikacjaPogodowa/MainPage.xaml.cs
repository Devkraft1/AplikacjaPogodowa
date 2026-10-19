using Pogoda;

namespace AplikacjaPogodowa
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadWeatherData();
        }
        private async void LoadWeatherData()
        {
            try
            {
                var lastLocation = await LoadLast.LoadLastCity();

                locationBtn.Text =
                    $"{lastLocation.name}, {lastLocation.admin1}";

                double latitude = lastLocation.latitude;
                double longitude = lastLocation.longitude;

                locationBtn.Text = $"\ueb1a {lastLocation.name}, {lastLocation.admin1}";
                var weather = await GetWeatherData.LoadData(latitude, longitude);
                if (weather is null)
                    return;
                UpdateCurrentWeather.Update(weather, currentTemp, currentWeatherCodeText, feelsLike, currentWindData, currentHumidity, currentPressure);
                UpdateHourlyWeather.Update(weather, hourlyForecast, hourlyScroll);
                UpdateDailyWeather.Update(weather, dailyForecast);
                UpdateSunMoondata.Update(weather, SunMoonData);
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }
}
