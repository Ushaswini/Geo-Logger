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
using Android.Locations;

namespace GeoLoggerApp
{
    [BroadcastReceiver]
    class AlarmEventReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //Receives when alarm is fired i.e every 5 minutes

            LocationManager manager = (LocationManager)context.GetSystemService(Context.LocationService);
            if (!manager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                //show alert dailog
                //   buildAlertMessageNoGps();
            }
            else
            {
                Intent i = new Intent(context, typeof(Services.GeoLoggerService));
                context.StartService(i);
            }
        }
    }
}