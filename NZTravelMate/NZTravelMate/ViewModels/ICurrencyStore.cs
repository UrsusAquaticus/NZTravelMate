using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NZTravelMate.Models;

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