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
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }
}
