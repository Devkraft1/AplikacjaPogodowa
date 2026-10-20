using Pogoda;
using System.Diagnostics;
using System.Text.Json;

namespace AplikacjaPogodowa
{
    public class LastLocationService
    {
        private readonly string _filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "lastlocation.json");
        public async Task<LocationResult?> LoadData()
        {
            if (File.Exists(_filePath))
            {
                string json = await File.ReadAllTextAsync(_filePath);
                var data = JsonSerializer.Deserialize<LocationResult>(json);
                if (data is not null) return data;
            }
            var defaultLocation = new LocationResult
            {
                Name = "Zamość",
                Latitude = 50.72314,
                Longitude = 23.25196,
                Country_code = "PL",
                Admin1 = "Lublin"
            };
            await SaveData(defaultLocation);
            return defaultLocation;
        }
        public async Task SaveData(LocationResult location)
        {
            if (location is null) return;
            string json = JsonSerializer.Serialize(location);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}