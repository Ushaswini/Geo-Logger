package md5e9558780f16cd61da438ee920848436a;


public class GeoLocationsAdapter_LocationViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("GeoLoggerApp.Adapters.GeoLocationsAdapter+LocationViewHolder, GeoLoggerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GeoLocationsAdapter_LocationViewHolder.class, __md_methods);
	}


	public GeoLocationsAdapter_LocationViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == GeoLocationsAdapter_LocationViewHolder.class)
			mono.android.TypeManager.Activate ("GeoLoggerApp.Adapters.GeoLocationsAdapter+LocationViewHolder, GeoLoggerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
