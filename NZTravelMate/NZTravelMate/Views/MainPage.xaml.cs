using NZTravelMate.Persistence;
using NZTravelMate.ViewModels;
using Xamarin.Forms;

namespace NZTravelMate.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            //Get the tables
            var database = DependencyService.Get<ISQLiteDb>();
            var currencyStore = new SQLiteCurrencyStore(database);
            var appStateStore = new SQLiteAppStateStore(database);

            ViewModel = new CurrencyViewModel(currencyStore, appStateStore);

            InitializeComponent();
        }

        //Load from database upon app loading
        //unsure how MVVM friendly it is
        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        //Get and set Currency View Model
        public CurrencyViewModel ViewModel
        {
            get { return BindingContext as CurrencyViewModel; }
            set { BindingContext = value; }
        }
    }
}