using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using GeoCoordinatePortable;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Models;
using University_advisor_web.Tools;

namespace University_advisor_web.Models
{
    public class MapModel
    {
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Range")]
        public double Range { get; set; }
        public (double, double) GetCoordinates(string url)
        {
            var jsonRes = new GeoLocationApi().GetLocationJson(url);
            var locationData = JsonConvert.DeserializeObject<List<LocationModel>>(jsonRes);
            var firstLocation = locationData[0];

            return (double.Parse(firstLocation.Lat, CultureInfo.InvariantCulture.NumberFormat), double.Parse(firstLocation.Lon, CultureInfo.InvariantCulture.NumberFormat));
        }
        public double GetDistance(double latA, double lonA, double latB, double lonB)
        {
            var locA = new GeoCoordinate(latA, lonA);
            var locB = new GeoCoordinate(latB, lonB);
            return locA.GetDistanceTo(locB);
        }
        public string ConstructApiUrl(string address)
        {
            var apiCallUrl = "https://eu1.locationiq.com/v1/search.php?key=" + "5e66cc9d64db23" + "&q=" + address + "&format=json";
            return apiCallUrl.Replace(" ", "%20");
        }
        public List<MarkerModel> GetSchoolsInRangeMarkers(double range, MarkerModel user)
        {
            var schoolsInRange = new List<MarkerModel>();

            foreach (var school in SqlDriver.Fetch("SELECT name,latitude,longitude FROM universities"))
            {
                var name = school["name"].ToString();
                var lat = school["latitude"].ToString();
                var lon = school["longitude"].ToString();
                var distance = GetDistance(user.Latitude, user.Longitude, Convert.ToDouble(lat), Convert.ToDouble(lon));
                if (distance <= range * 1000)
                {
                    var newSchoolInfo = new MarkerModel
                    {
                        Latitude = Convert.ToDouble(lat),
                        Longitude = Convert.ToDouble(lon),
                        Name = name
                    };
                    schoolsInRange.Add(newSchoolInfo);
                }
            }
            return (schoolsInRange);
        }
    }
}
