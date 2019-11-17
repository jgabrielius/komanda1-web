using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class MarkerModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public MarkerModel(double lat, double lon, string name = "", int id = 0)
        {
            Latitude = lat;
            Longitude = lon;
            Name = name;
            Id = id;
        }
    }
}
