# Geo-Logger
Repository to hold android app which tracks user location

This native Xamarin.Android app logs the device location every 5 minutes. Used Alarm Manager to log the location even when the app is killed and is not running either in foreground nor in background

Logged location is stored in SQLite db with db name "Location.db" and with table name "locations".

On app start, we get the list of locations stored in db. Click Refresh icon in toolbar to update the list of locations.
