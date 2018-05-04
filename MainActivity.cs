using Android.App;
using Android.Views;
using Android.OS;
using Android.Content;
using Java.Lang;
using GeoLoggerApp.Adapters;
using System.Collections.Generic;
using GeoLoggerApp.SQLite;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace GeoLoggerApp
{
    [Activity(Label = "Geo Logger", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView mRvLocations;
        RecyclerView.LayoutManager mLayoutManager;        
        GeoLocationsAdapter mAdapter;        
        List<GeoLocation> locations;
        const int INTERVAL = 300000; //5 min
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Init();
            ScheduleAlarm();
        }

        private void Init()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //Toolbar will now take on default actionbar characteristics
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Geo Logger";
            locations = DataStore.LocationHelper.GetLocations(this);
            mRvLocations = FindViewById<RecyclerView>(Resource.Id.rvLocations);
            mLayoutManager = new LinearLayoutManager(this);
            mRvLocations.SetLayoutManager(mLayoutManager);
            mAdapter = new GeoLocationsAdapter(locations);
            mRvLocations.SetAdapter(mAdapter);
        }

        private void ScheduleAlarm()
        {           
            Intent locationTrackerIntent = new Intent(this, typeof(AlarmEventReceiver));

            //PASS CONTEXT,YOUR PRIVATE REQUEST CODE,INTENT OBJECT AND FLAG
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, locationTrackerIntent, 0);

            //INITIALIZE ALARM MANAGER
            AlarmManager alarmManager = (AlarmManager)GetSystemService(AlarmService);

            //SET THE ALARM
            alarmManager.SetInexactRepeating(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis(), INTERVAL, pendingIntent);
            //alarmManager.Set(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + (INTERVAL * 1000), pendingIntent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.refresh_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_refresh)
            {
                RefreshData();
            }
            return base.OnOptionsItemSelected(item);
        }

        private void RefreshData()
        {
            RunOnUiThread(() => 
            {
                mAdapter.UpdateData(DataStore.LocationHelper.GetLocations(this));
                //Updating data
            });

        }

    }
}

