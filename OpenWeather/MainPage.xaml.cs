using Newtonsoft.Json.Linq;

namespace OpenWeather;

public partial class MainPage : ContentPage
{
    private const string ApiKey = "c29021984456328c698fb7327d2bd505";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
    private static readonly HttpClient HttpClient = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void BtnShowTemp_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryZipCode.Text))
        {
            await DisplayAlert("Invalid Input", "Please enter a zip code.", "Close");
            return;
        }

        await LoadWeatherAsync($"zip={Uri.EscapeDataString(EntryZipCode.Text.Trim())}");
    }

    private async void CityPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CityPicker.SelectedIndex == -1 || CityPicker.SelectedItem is null)
        {
            return;
        }

        await LoadWeatherAsync($"q={Uri.EscapeDataString(CityPicker.SelectedItem.ToString()!)}");
    }

    private async Task LoadWeatherAsync(string locationQuery)
    {
        BtnShowTemp.IsEnabled = false;
        CityPicker.IsEnabled = false;

        try
        {
            var url = $"{BaseUrl}?{locationQuery}&appid={ApiKey}&units=imperial";
            var jsonText = await HttpClient.GetStringAsync(url);

            var weatherRoot = JObject.Parse(jsonText);
            var main = weatherRoot["main"] as JObject ?? new JObject();
            var wind = weatherRoot["wind"] as JObject ?? new JObject();

            WeatherGV.CityName = weatherRoot["name"]?.ToString() ?? "Unknown";
            WeatherGV.CurTemp = main["temp"]?.Value<double>() ?? 0;
            WeatherGV.MinTemp = main["temp_min"]?.Value<double>() ?? 0;
            WeatherGV.MaxTemp = main["temp_max"]?.Value<double>() ?? 0;
            WeatherGV.WindSpeed = wind["speed"]?.Value<double>() ?? 0;
            WeatherGV.WindDirection = wind["deg"]?.Value<double>() ?? 0;
            WeatherGV.Pressure = main["pressure"]?.Value<double>() ?? 0;
            WeatherGV.Humidity = main["humidity"]?.Value<double>() ?? 0;

            WindLabel.Text = $"Wind: {WeatherGV.WindSpeed:F1} mph at {WeatherGV.WindDirection:F0}°";
            PressureHumidityLabel.Text = $"Pressure: {WeatherGV.Pressure:F0} hPa, Humidity: {WeatherGV.Humidity:F0}%";

            await Navigation.PushAsync(new WeatherPage());
        }
        catch (HttpRequestException)
        {
            await DisplayAlert("Network Error", "Unable to reach weather service. Check your connection and try again.", "Close");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Close");
        }
        finally
        {
            BtnShowTemp.IsEnabled = true;
            CityPicker.IsEnabled = true;
        }
    }
}
