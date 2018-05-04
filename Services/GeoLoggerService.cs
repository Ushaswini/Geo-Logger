using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Locations;
using Android.Util;
using GeoLoggerApp.SQLite;

namespace GeoLoggerApp.Services
{
    [Service]
    public class GeoLoggerService : Service
    {
        public GeoLoggerService()
        {
        }

        // Set our location manager as the system location service
        protected LocationManager LocMgr = Android.App.Application.Context.GetSystemService("location") as LocationManager;

        readonly string logTag = "LocationService";
        

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug(logTag, "OnCreate called in the Location Service");
        }

        // This gets called when StartService is called in our App class
        [Obsolete("deprecated in base class")]
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(logTag, "LocationService started");
            StartLocationUpdates();

            return StartCommandResult.Sticky;
        }

       
        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(logTag, "OnBind");
            return null;
        }

        // Handle location updates from the location manager
        public void StartLocationUpdates()
        {
            var locationCriteria = new Criteria();

            locationCriteria.Accuracy = Accuracy.NoRequirement;
            locationCriteria.PowerRequirement = Power.NoRequirement;

            // get provider: GPS, Network, etc.
            var locationProvider = LocMgr.GetBestProvider(locationCriteria, true);
            Log.Debug(logTag, string.Format("You are about to get location updates via {0}", locationProvider));

            Log.Debug(logTag, "Now saving location updates");

            var location = LocMgr.GetLastKnownLocation(locationProvider);

            GeoLocation locToSave = new GeoLocation
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                TimeLocated = DateTime.Now
            };

            DataStore.LocationHelper.InsertLocation(ApplicationContext, locToSave);

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Log.Debug(logTag, "Service has been terminated");            
        }

    }

}