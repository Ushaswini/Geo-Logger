using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GeoLoggerApp.SQLite
{
    public class GeoLocation
    {
        public int Id { get; set; }
       public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime TimeLocated { get; set; }

        public override string ToString()
        {
            return "Lat -" + Latitude + " & Long- " + Longitude + " recorded at " + TimeLocated;
        }
    }
}