using NZTravelMate.Droid;
using NZTravelMate.Persistence;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace NZTravelMate.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            Debug.WriteLine($"Database path:{path}");

            return new SQLiteAsyncConnection(path);
        }
    }
}