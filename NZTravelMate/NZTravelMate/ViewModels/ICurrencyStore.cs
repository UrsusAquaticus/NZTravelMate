using NZTravelMate.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NZTravelMate.ViewModels
{
    public interface ICurrencyStore
    {
        Task<ObservableCollection<Currency>> GetCurrenciesAsync();

        Task AddCurrency(Currency currency);

        Task UpdateCurrency(Currency currency);

        Task DeleteCurrency(Currency currency);

        Task<Currency> GetCurrency(string code);
    }
}