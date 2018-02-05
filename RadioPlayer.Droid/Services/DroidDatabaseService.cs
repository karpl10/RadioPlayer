using System;
using System.IO;
using RadioPlayer.Droid.Services;
using RadioPlayer.Services;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidDatabaseService))]

namespace RadioPlayer.Droid.Services
{
    public class DroidDatabaseService : IDatabaseService
    {
        private SQLiteConnection connection;

        public SQLiteConnection Connection
        {
            get
            {
                if (connection != null) return connection;
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "stations.sqlite");

                connection = new SQLiteConnection(new SQLitePlatformAndroid(), path);

                return connection;
            }
        }
    }
}