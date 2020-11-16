using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NZTravelMate.ViewModels
{
    //https://hub.packtpub.com/xamarin-how-to-add-a-mvvm-pattern-to-an-app-tutorial/
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}