using NZTravelMate.Droid;
using NZTravelMate.Persistence;
using SQLite;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace NZTravelMate.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            Debug.WriteLine($"Database path:{path}");

            return new SQLiteAsyncConnection(path);
        }
    }
}