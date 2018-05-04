using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using System;
using System.Collections.Generic;

namespace GeoLoggerApp.SQLite
{
    // Inheriting from the SQLiteOpenHelper
    public class DataStore : SQLiteOpenHelper
    {
        private static string _DatabaseName = "Location.db";

        /*
         * A default constructor is required, to call the base constructor.
         * The base constructor, takes in the context and the database name; rest of the 
         * 2 parameters are not as much important to understand. 
         */
        public DataStore(Context context) : base(context, name: _DatabaseName, factory: null, version: 1)
        {
        }

        // Default function to create the database. 
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(LocationHelper.CreateQuery);
        }

        // Default function to upgrade the database.
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(LocationHelper.DeleteQuery);
            OnCreate(db);
        }

        public class LocationHelper
        {
            private const string TableName = "locations";
            private const string ColumnID = "id";
            private const string ColumnLongitude = "location";
            private const string ColumnLatitude = "latitude";
            private const string ColumnTime = "time_created";

            public const string CreateQuery = "CREATE TABLE " + TableName + " ( "
                + ColumnID + " INTEGER PRIMARY KEY,"
                + ColumnLongitude + " INTEGER,"
                + ColumnLatitude + " INTEGER,"
                + ColumnTime + " datetime default CURRENT_TIMESTAMP)";


            public const string DeleteQuery = "DROP TABLE IF EXISTS " + TableName;

            public LocationHelper()
            {
            }

            public static void InsertLocation(Context context, GeoLocation location)
            {
                SQLiteDatabase db = new DataStore(context).WritableDatabase;
                ContentValues contentValues = new ContentValues();
                contentValues.Put(ColumnLongitude, location.Longitude);
                contentValues.Put(ColumnLatitude, location.Latitude);
                contentValues.Put(ColumnTime, location.TimeLocated.ToString());

                db.Insert(TableName, null, contentValues);
                db.Close();
            }

            public static List<GeoLocation> GetLocations(Context context)
            {
                List<GeoLocation> locations = new List<GeoLocation>();
                SQLiteDatabase db = new DataStore(context).ReadableDatabase;
                string[] columns = new string[] { ColumnID, ColumnLongitude, ColumnLatitude, ColumnTime };

                using (ICursor cursor = db.Query(TableName, columns, null, null, null, null, null))
                {
                    while (cursor.MoveToNext())
                    {
                        locations.Add(new GeoLocation
                        {
                            Id = cursor.GetInt(cursor.GetColumnIndexOrThrow(ColumnID)),
                            Longitude = cursor.GetDouble(cursor.GetColumnIndexOrThrow(ColumnLongitude)),
                            Latitude = cursor.GetDouble(cursor.GetColumnIndexOrThrow(ColumnLatitude)),
                            TimeLocated = DateTime.Parse(cursor.GetString(cursor.GetColumnIndexOrThrow(ColumnTime)))
                        });
                    }
                }
                db.Close();
                return locations;
            }
        }
    }
}