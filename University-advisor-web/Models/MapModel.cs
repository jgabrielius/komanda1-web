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
using University_advisor_web.Constants;
using System.Diagnostics;

namespace University_advisor_web.Models
{
    public class MapModel
    {
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Range")]
        public double Range { get; set; }
        public GeoCoordinate MapCenter { get; set; }
        public List<MarkerModel> LocationsInRange { get; set; }
        public MapModel(string address, string showOnMap = "", double range = 0) //if range = 0, show all locations
        {
            var geoCoordinate = GetCoordinates(ConstructApiUrl(address));
            MapCenter = new GeoCoordinate(geoCoordinate.Item1, geoCoordinate.Item2); 
            Range = range;
            Address = address;
            if (showOnMap == "Universities") LocationsInRange = GetLocationsInRangeMarkers(SqlDriver.Fetch("SELECT name,latitude,longitude FROM universities"));
            else LocationsInRange = new List<MarkerModel>();
        }
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
            var apiCallUrl = "https://eu1.locationiq.com/v1/search.php?key=" + ApiKeys.GeoLocationApiKey + "&q=" + address + "&format=json";
            return apiCallUrl.Replace(" ", "%20");
        }
        public List<MarkerModel> GetLocationsInRangeMarkers(List<Dictionary<string, object>> listOfLocations)
        {
            var LocationsInRange = new List<MarkerModel>();

            foreach (var location in listOfLocations)
            {

                var name = location["name"].ToString();
                var lat = location["latitude"].ToString();
                var lon = location["longitude"].ToString();

                var distance = GetDistance(MapCenter.Latitude, MapCenter.Longitude, Convert.ToDouble(lat), Convert.ToDouble(lon));
                if (distance <= Range * 1000 || Range == 0)
                {
                    var newMarker = new MarkerModel(Convert.ToDouble(lat), Convert.ToDouble(lon), name);
                    LocationsInRange.Add(newMarker);
                }
            }
            return LocationsInRange;
        }
    }
}
