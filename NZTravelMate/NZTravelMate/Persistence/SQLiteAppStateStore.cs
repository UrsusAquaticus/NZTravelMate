using NZTravelMate.Models;
using NZTravelMate.Persistence;
using NZTravelMate.ViewModels;
using SQLite;
using System.Threading.Tasks;

namespace NZTravelMate
{
    public class SQLiteAppStateStore : IAppStateStore
    {
        private SQLiteAsyncConnection _connection;

        public SQLiteAppStateStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<AppState>();
        }

        //If appState is null, return a new object
        public async Task<AppState> GetAppStateAsync()
        {
            var list = await _connection.Table<AppState>().ToListAsync();
            return list.Count > 0 ? list[0] : null;
        }

        public async Task AddAppState(AppState appState)
        {
            await _connection.InsertAsync(appState);
        }

        public async Task UpdateAppState(AppState appState)
        {
            await _connection.UpdateAsync(appState);
        }
    }
}