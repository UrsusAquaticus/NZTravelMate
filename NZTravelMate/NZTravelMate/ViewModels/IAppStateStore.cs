using NZTravelMate.Models;
using System.Threading.Tasks;

namespace NZTravelMate.ViewModels
{
    //CRUD Interfaces used to keep the other aspects of the app persistent
    public interface IAppStateStore
    {
        Task<AppState> GetAppStateAsync();

        Task AddAppState(AppState appState);

        Task UpdateAppState(AppState appState);
    }
}