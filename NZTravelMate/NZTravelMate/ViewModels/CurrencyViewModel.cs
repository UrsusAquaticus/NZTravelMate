using NZTravelMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace NZTravelMate.ViewModels
{
    public class CurrencyViewModel : ViewModelBase
    {
        private ObservableCollection<Currency> _currencies;

        private string _firstAmount = "1";
        private string _secondAmount = "1";
        private int _firstCurrency = 32; // NZD
        private int _secondCurrency = 2; // AUD

        //What the View binds to
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }


        public string FirstAmount
        {
            get { return _firstAmount; }
            set
            {
                _firstAmount = value;
                OnPropertyChanged();
            }
        }
        public string SecondAmount
        {
            get { return _secondAmount; }
            set
            {
                _secondAmount = value;
                OnPropertyChanged();
            }
        }
        public int FirstCurrency
        {
            get { return _firstCurrency; }
            set
            {
                _firstCurrency = value;
                OnPropertyChanged();
            }
        }
        public int SecondCurrency
        {
            get { return _secondCurrency; }
            set
            {
                _secondCurrency = value;
                OnPropertyChanged();
            }
        }

        //Constructor
        public CurrencyViewModel(ObservableCollection<Currency> currencies)
        {
            Currencies = currencies;
        }
    }
}
