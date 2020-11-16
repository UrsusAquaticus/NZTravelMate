using NZTravelMate.Models;
using NZTravelMate.Persistence;
using NZTravelMate.ViewModels;
using SQLite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NZTravelMate
{
    public class SQLiteCurrencyStore : ICurrencyStore
    {
        private SQLiteAsyncConnection _connection;

        public SQLiteCurrencyStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Currency>();
        }

        public async Task<ObservableCollection<Currency>> GetCurrenciesAsync()
        {
            var list = await _connection.Table<Currency>().ToListAsync();
            return new ObservableCollection<Currency>(list);
        }

        public async Task DeleteCurrency(Currency currency)
        {
            await _connection.DeleteAsync(currency);
        }

        public async Task AddCurrency(Currency currency)
        {
            await _connection.InsertAsync(currency);
        }

        public async Task UpdateCurrency(Currency currency)
        {
            await _connection.UpdateAsync(currency);
        }

        public async Task<Currency> GetCurrency(string code)
        {
            return await _connection.FindAsync<Currency>(code);
        }
    }
}