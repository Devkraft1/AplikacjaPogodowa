using Pogoda;
using System.Diagnostics;

namespace AplikacjaPogodowa
{
    public partial class MainPage : ContentPage
    {
        public double searchRowHeight = 0;
        public bool didCalculateSearchRowHeight = false;
        public MainPage()
        {
            InitializeComponent();
            LoadWeatherData();
            backBtn.Text = "\udb85\udfa7";
        }
        public async void LoadWeatherData()
        {
            ToggleLoadingOverlay("Aktualizowanie pogody...");
            try
            {
                var lastLocationService = new LastLocationService();
                var lastLocation = await lastLocationService.LoadData();
                if (lastLocation is null || lastLocation.Name is null || lastLocation.Latitude is null || lastLocation.Longitude is null) return;
                double latitude = lastLocation.Latitude ?? 0;
                double longitude = lastLocation.Longitude ?? 0;

                locationBtn.Text = $"\ueb1a {lastLocation.Name}, {lastLocation.Admin1}";
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
            finally
            {
                ToggleLoadingOverlay();
            }
        }
        public void ToggleSearchMenu(object? sender, EventArgs e)
        {
            searchMenu.IsVisible = !searchMenu.IsVisible;
        }
        public void ToggleLoadingOverlay(string message = "")
        {
            loadingMessage.Text = message;
            LoadingOverlay.IsVisible = !LoadingOverlay.IsVisible;
        }
        public async void ShowSearchResults(object? sender, EventArgs e)
        {
            ToggleLoadingOverlay("Wyszukiwanie miast...");
            while (searchGrid.Children.Count > 2)
            {
                searchGrid.Children.RemoveAt(searchGrid.Children.Count - 1);
            }
            while (searchGrid.RowDefinitions.Count > 1)
            {
                searchGrid.RowDefinitions.RemoveAt(searchGrid.RowDefinitions.Count - 1);
            }

            if (sender is null) return;
            Entry s = (Entry)sender;
            if (s.Text is null) return;
            try
            {
                string name = s.Text;
                var locations = await GetLocationData.LoadData(name);
                if (locations is null) return;
                if (locations.Results is null) return;
                foreach (var location in locations.Results)
                {
                    searchGrid.RowDefinitions.Add(new RowDefinition(searchRowHeight));
                    Button cityButton = new Button
                    {
                        Text = $"{location.Country_code} {location.Name}, {location.Admin1}",
                        CommandParameter = location,
                        BackgroundColor = Colors.Transparent,
                        FontSize = 20
                    };
                    cityButton.Clicked += SetLocation;
                    searchGrid.Add(new Border 
                    { 
                        Stroke = Colors.Gray,
                        StrokeThickness = 1,
                        Content = cityButton
                    }, 0, searchGrid.RowDefinitions.Count - 1);
                    Button favoriteButton = new Button
                    {
                        Text = $"F",
                        FontSize = 30,
                        BackgroundColor = Colors.Transparent
                    };
                    searchGrid.Add(new Border
                    {
                        Stroke = Colors.Gray,
                        StrokeThickness = 1,
                        Content = favoriteButton
                    }, 1, searchGrid.RowDefinitions.Count - 1);
                }
            }
            catch(Exception ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
            finally
            {
                ToggleLoadingOverlay();
            }
        }
        public void CalculateSearchRowHeight(object? sender, EventArgs e)
        {
            if (!didCalculateSearchRowHeight)
            {
                searchRowHeight = searchGrid.Height / 10.0;
                searchGrid.RowDefinitions.Add(new RowDefinition(searchRowHeight));
                didCalculateSearchRowHeight = true;
            }
            return;
        }
        public async void SetLocation(object? sender, EventArgs e)
        {
            try
            {
                if (sender is not Button button) return;
                if (button.CommandParameter is not LocationResult location) return;
                Button s = (Button)sender;
                ToggleSearchMenu(sender, e);
                var lastLocationService = new LastLocationService();
                await lastLocationService.SaveData(location);
                LoadWeatherData();
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }
}