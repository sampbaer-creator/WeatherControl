namespace OpenWeather;

public partial class WeatherPage : ContentPage
{
    public WeatherPage()
    {
        InitializeComponent();

        LblCity.Text = $"City: {WeatherGV.CityName}";
        LblCurTemp.Text = $"Current: {WeatherGV.CurTemp:F1}°F";
        LblHighTemp.Text = $"{WeatherGV.MaxTemp:F1}°F";
        LblLowTemp.Text = $"{WeatherGV.MinTemp:F1}°F";

        LblWind.Text = $"Wind: {WeatherGV.WindSpeed:F1} mph at {WeatherGV.WindDirection:F0}°";
        LblPressureHumidity.Text = $"Pressure: {WeatherGV.Pressure:F0} hPa, Humidity: {WeatherGV.Humidity:F0}%";
    }
}
