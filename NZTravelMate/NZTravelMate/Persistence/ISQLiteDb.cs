using SQLite;

namespace NZTravelMate.Persistence
{
    //Interface which is implemented through system specific SQLiteDb classes.
    //They're the ones in the "Persistence" folders under each project
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}