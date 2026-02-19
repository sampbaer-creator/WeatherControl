using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather
{
    public static class WeatherGV
    {
        public static string CityName { get; set; } = string.Empty;
        public static double CurTemp { get; set; }
        public static double MinTemp { get; set; }
        public static double MaxTemp { get; set; }

        // New properties
        public static double WindSpeed { get; set; }
        public static double WindDirection { get; set; }
        public static double Pressure { get; set; }
        public static double Humidity { get; set; }
    }
}


