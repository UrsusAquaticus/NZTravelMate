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

        int oldFirstIndex, oldSecondIndex;

        bool isCalculating = false;

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
                MakeCalculation(true);
            }
        }
        public string SecondAmount
        {
            get { return _secondAmount; }
            set
            {
                _secondAmount = value;
                OnPropertyChanged();
                MakeCalculation(false);
            }
        }
        public int FirstCurrency
        {
            get { return _firstCurrency; }
            set
            {
                _firstCurrency = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }
        public int SecondCurrency
        {
            get { return _secondCurrency; }
            set
            {
                _secondCurrency = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }

        //Constructor
        public CurrencyViewModel(ObservableCollection<Currency> currencies)
        {
            Currencies = currencies;
        }

        //Calculation
        public void MakeCalculation(bool isFirst)
        {
            if (isCalculating) return;
            try
            {
                isCalculating = true;

                double firstValue = FirstAmount != "" ? Convert.ToDouble(FirstAmount) : 1;
                double secondValue = SecondAmount != "" ? Convert.ToDouble(SecondAmount) : 1;

                int leftIndex = FirstCurrency;
                int rightIndex = SecondCurrency;

                //If they have the same currency, flip the last pair of currencies around
                if (leftIndex == rightIndex)
                {
                    leftIndex = oldSecondIndex;
                    rightIndex = oldFirstIndex;

                    FirstCurrency = leftIndex;
                    SecondCurrency = rightIndex;
                }

                //Order of calculations based on which block of elements was updated
                if (isFirst)
                {
                    SecondAmount = Math.Round(
                        CurrencyConverter.GetValueByRates(
                        firstValue,
                        _currencies[leftIndex].Rate,
                        _currencies[rightIndex].Rate
                        ), 2).ToString();
                    oldFirstIndex = leftIndex;
                    oldSecondIndex = rightIndex;
                }
                else
                {
                    FirstAmount = Math.Round(
                        CurrencyConverter.GetValueByRates(
                        secondValue,
                        _currencies[rightIndex].Rate,
                        _currencies[leftIndex].Rate
                        ), 2).ToString();
                    oldFirstIndex = rightIndex;
                    oldSecondIndex = leftIndex;
                }
            }
            finally
            {
                isCalculating = false;
            }
        }
    }
}
