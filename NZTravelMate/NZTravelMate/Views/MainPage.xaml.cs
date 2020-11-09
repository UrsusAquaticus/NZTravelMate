using NZTravelMate.Models;
using NZTravelMate.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NZTravelMate.Views
{
    public partial class MainView : ContentPage
    {
        //The api string requires a "base currency code" to work off
        string apiString = "https://v6.exchangerate-api.com/v6/985c1703315672382c3c7b6c/latest/";
        string baseCode = "NZD"; //New Zealand Dollar
        string isoFile = "NZTravelMate.iso-4217.json";

        public MainView()
        {
            InitializeComponent();
            //Give the View something to bind to
            GetCurrencyViewModel();
        }

        async void GetCurrencyViewModel()
        {
            var ERA = new ExchangeRateApi();
            var CR = await ERA.GetRatesDataAsync(apiString + baseCode);
            var namesByCode = await CurrencyDataReader.NamesByCode(isoFile);
            var currencies = CurrencyBuilder.GetCurrencies(CR, namesByCode);

            this.BindingContext = new CurrencyViewModel(currencies);
        }
    }
}
