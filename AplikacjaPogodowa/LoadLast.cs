using System.Text.Json;

namespace AplikacjaPogodowa
{
    public class LoadLast
    {
        public static async Task<(string name, string admin1,
            double latitude, double longitude)> LoadLastCity()
        {
            using var stream =
                await FileSystem.OpenAppPackageFileAsync("lastlocation.json");

            using var reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();

            using JsonDocument doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            string name = root.GetProperty("name").GetString()!;
            string admin1 = root.GetProperty("admin1").GetString()!;
            double latitude = root.GetProperty("latitude").GetDouble();
            double longitude = root.GetProperty("longitude").GetDouble();

            return (name, admin1, latitude, longitude);
        }
    }
}