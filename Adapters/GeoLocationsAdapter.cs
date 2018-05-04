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
using Android.Support.V7.Widget;
using GeoLoggerApp.SQLite;

namespace GeoLoggerApp.Adapters
{
   
    public class GeoLocationsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<GeoLocation> mLocations;
        public GeoLocationsAdapter(List<GeoLocation> locations)
        {
            mLocations = locations;
        }
        public override int ItemCount
        {
            get { return mLocations.Count; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            LocationViewHolder vh = holder as LocationViewHolder;
            vh.tvLongitude.Text = "Longitude - " + mLocations[position].Longitude.ToString();
            vh.tvLatitude.Text = "Latitude - " + mLocations[position].Latitude.ToString();
            vh.tvTime.Text = "Logged at - " + mLocations[position].TimeLocated.ToString();

        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.location_item, parent, false);
            LocationViewHolder vh = new LocationViewHolder(itemView);
            return vh;
        }
        
        public void UpdateData(List<GeoLocation> locations)
        {
            this.mLocations.Clear();
            this.mLocations.AddRange(locations);
            this.NotifyDataSetChanged();
        }
        class LocationViewHolder : RecyclerView.ViewHolder
        {
            
            public TextView tvLongitude { get; set; }
            public TextView tvLatitude { get; set; }
            public TextView tvTime { get; set; }
            public LocationViewHolder(View itemview) : base(itemview)  
        {

                tvLongitude = itemview.FindViewById<TextView>(Resource.Id.Longitude);
                tvLatitude = itemview.FindViewById<TextView>(Resource.Id.Latitude);
                tvTime = itemview.FindViewById<TextView>(Resource.Id.LocTime);

            }
        }
    }
}