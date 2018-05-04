using Android.Content;
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
                
            }
            else
            {
                Intent i = new Intent(context, typeof(Services.GeoLoggerService));
                context.StartService(i);
            }
        }
    }
}