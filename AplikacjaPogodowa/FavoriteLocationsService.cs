using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pogoda
{
    public class FavoriteLocationsService
    {
        private readonly string _filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "favorites.json");
        public async Task<LocationData?> LoadData()
        {
            if (File.Exists(_filePath))
            {
                string json = await File.ReadAllTextAsync(_filePath);
                var data = JsonSerializer.Deserialize<LocationData>(json);
                if (data is not null) return data;
            }
            return null;
        }
        public async Task AddData(LocationResult location)
        {
            LocationData locations = File.Exists(_filePath) ? JsonSerializer.Deserialize<LocationData>(await File.ReadAllTextAsync(_filePath)) ?? new() : new LocationData();
            locations.Results ??= new List<LocationResult>();
            locations.Results.Add(location);
            string json = JsonSerializer.Serialize(locations, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
        public async Task RemoveData(LocationResult location)
        {
            if (!File.Exists(_filePath)) return;
            LocationData locations = JsonSerializer.Deserialize<LocationData>(await File.ReadAllTextAsync(_filePath)) ?? new();
            if (locations.Results is null) return;
            int index = locations.Results.FindIndex(x => x.Name == location.Name && x.Latitude == location.Latitude && x.Longitude == location.Longitude && x.Country_code == location.Country_code && x.Admin1 == location.Admin1);
            if (index < 0) return;
            locations.Results.RemoveAt(index);
            string json = JsonSerializer.Serialize(locations, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}