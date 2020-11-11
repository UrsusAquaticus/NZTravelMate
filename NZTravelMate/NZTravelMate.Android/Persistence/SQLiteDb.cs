using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using NZTravelMate.Droid;
using NZTravelMate.Persistence;

[assembly: Dependency(typeof(SQLiteDb))]

namespace NZTravelMate.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}