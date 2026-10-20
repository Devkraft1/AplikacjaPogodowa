using Pogoda;
using System.Diagnostics;

namespace AplikacjaPogodowa
{
    public partial class MainPage : ContentPage
    {
        public double searchRowHeight = 0;
        public bool didCalculateSearchRowHeight = false;
        public double favoritesRowHeight = 0;
        public bool didCalculateFavoritesRowHeight = false;
        string backSymbol = "\udb85\udfa7";
        public MainPage()
        {
            InitializeComponent();
            LoadWeatherData();
            backBtn1.Text = backSymbol;
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
            bool shouldOpen = !searchMenu.IsVisible;

            searchMenu.IsVisible = shouldOpen;

            if (shouldOpen && !string.IsNullOrWhiteSpace(searchEntry.Text))
            {
                ShowSearchResults(sender, e);
            }
        }
        public void ToggleFavoritesMenu(object? sender = null, EventArgs? e = null)
        {
            favoritesMenu.IsVisible = !favoritesMenu.IsVisible;
        }
        public void ToggleLoadingOverlay(string message = "")
        {
            loadingMessage.Text = message;
            LoadingOverlay.IsVisible = !LoadingOverlay.IsVisible;
        }
        public async void ShowFavoritesMenu(object? sender, EventArgs e)
        {
            while (favoritesGrid.Children.Count > 2)
            {
                favoritesGrid.Children.RemoveAt(favoritesGrid.Children.Count - 1);
            }
            while (favoritesGrid.RowDefinitions.Count > 1)
            {
                favoritesGrid.RowDefinitions.RemoveAt(favoritesGrid.RowDefinitions.Count - 1);
            }

            Border titleBorder = new()
            {
                Stroke = Colors.Gray,
                StrokeThickness = 1
            };
            Label titleLabel = new()
            {
                FontSize = 20,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Twoje ulubione miasta"
            };
            titleBorder.Content = titleLabel;
            favoritesGrid.Add(titleBorder, 0, 0);

            Border backBorder = new()
            {
                Stroke = Colors.Gray,
                StrokeThickness = 1
            };
            Button backButton = new()
            {
                BackgroundColor = Colors.Transparent,
                FontSize = 30,
                Text = backSymbol
            };
            backButton.Clicked += ToggleFavoritesMenu;
            backBorder.Content = backButton;
            favoritesGrid.Add(backBorder, 1, 0);
            ToggleFavoritesMenu();
            if (sender is null) return;
            try
            {
                FavoriteLocationsService favlocser = new FavoriteLocationsService();
                var favoriteLocations = await favlocser.LoadData();
                if (favoriteLocations is null) return;
                if (favoriteLocations.Results is null) return;
                foreach (var location in favoriteLocations.Results)
                {
                    favoritesGrid.RowDefinitions.Add(new RowDefinition(favoritesRowHeight));
                    Button cityButton = new Button
                    {
                        Text = $"{location.Country_code} {location.Name}, {location.Admin1}",
                        CommandParameter = location,
                        BackgroundColor = Colors.Transparent,
                        FontSize = 20
                    };
                    cityButton.Clicked += SetLocation;
                    favoritesGrid.Add(new Border
                    {
                        Stroke = Colors.Gray,
                        StrokeThickness = 1,
                        Content = cityButton
                    }, 0, favoritesGrid.RowDefinitions.Count - 1);
                    Button UnFavoriteButton = new Button
                    {
                        Text = $"\ueb59",
                        FontSize = 30,
                        BackgroundColor = Colors.Transparent,
                        CommandParameter = location
                    };
                    UnFavoriteButton.Clicked += RemoveFavoriteLocation;
                    favoritesGrid.Add(new Border
                    {
                        Stroke = Colors.Gray,
                        StrokeThickness = 1,
                        Content = UnFavoriteButton
                    }, 1, favoritesGrid.RowDefinitions.Count - 1);
                }
            }
            catch(Exception ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
        public async void AddFavoriteLocation(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;
            if (button.CommandParameter is not LocationResult location) return;
            FavoriteLocationsService favlocser = new FavoriteLocationsService();
            await favlocser.AddData(location);
            ShowSearchResults(sender, e);
        }
        public async void RemoveFavoriteLocation(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;
            if (button.CommandParameter is not LocationResult location) return;
            FavoriteLocationsService favlocser = new FavoriteLocationsService();
            await favlocser.RemoveData(location);
            ShowFavoritesMenu(sender, e);
        }
        public async void RemoveFavoriteLocationFromSearch(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;
            if (button.CommandParameter is not LocationResult location) return;
            FavoriteLocationsService favlocser = new FavoriteLocationsService();
            await favlocser.RemoveData(location);
            ShowSearchResults(sender, e);
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
            if (searchEntry.Text is null) return;
            try
            {
                string name = searchEntry.Text;
                var locations = await GetLocationData.LoadData(name);
                var favlocsec = new FavoriteLocationsService();
                var favorites = await favlocsec.LoadData();
                if (locations is null) return;
                if (locations.Results is null) return;
                foreach (var location in locations.Results)
                {
                    bool isFavorite = false;
                    if (favorites is not null && favorites.Results is not null)
                    {
                        isFavorite = favorites.Results.Any(x =>
                        x.Name == location.Name &&
                        x.Latitude == location.Latitude &&
                        x.Longitude == location.Longitude &&
                        x.Country_code == location.Country_code &&
                        x.Admin1 == location.Admin1
                        );
                    }
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
                        Text = isFavorite ? "\ueb59" : "\uea6a",
                        FontSize = 30,
                        BackgroundColor = Colors.Transparent,
                        CommandParameter = location
                    };
                    favoriteButton.Clicked += isFavorite ? RemoveFavoriteLocationFromSearch : AddFavoriteLocation;
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
        public void CalculateSearchRowHeight(object? sender, EventArgs? e)
        {
            if (!didCalculateSearchRowHeight)
            {
                searchRowHeight = searchGrid.Height / 10.0;
                didCalculateSearchRowHeight = true;
                searchGrid.RowDefinitions.Add(new RowDefinition { Height = searchRowHeight });
            }
        }
        public void CalculateFavoritesRowHeight(object? sender, EventArgs? e)
        {
            if (!didCalculateFavoritesRowHeight)
            {
                favoritesRowHeight = favoritesGrid.Height / 10.0;
                didCalculateFavoritesRowHeight = true;
                favoritesGrid.RowDefinitions.Add(new RowDefinition { Height = favoritesRowHeight });
            }
        }
        public async void SetLocation(object? sender, EventArgs e)
        {
            try
            {
                if (sender is not Button button) return;
                if (button.CommandParameter is not LocationResult location) return;
                Button s = (Button)sender;
                searchMenu.IsVisible = false;
                favoritesMenu.IsVisible = false;
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