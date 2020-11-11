using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NZTravelMate.Models;

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