using NZTravelMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NZTravelMate.ViewModels
{
    public class CurrencyViewModel : ViewModelBase
    {
        private ObservableCollection<Currency> _currencies;

        private double _firstAmount = 1;
        private double _secondAmount = 1;
        private int _firstCurrency = 32; // NZD
        private int _secondCurrency = 2; // AUD
        private string _firstOutput = "";
        private string _secondOutput = "";
        private double _taxInput = 0;
        private string _taxOutput = "";
        private bool _taxVisible = false;

        int oldFirstIndex, oldSecondIndex;

        bool isCalculating = false;

        #region Exposed Bindables
        //Swap is assigned in the constructor
        public ICommand SwapCommand { private set; get; }
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }
        public double FirstAmount
        {
            get { return _firstAmount; }
            set
            {
                _firstAmount = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }
        public double SecondAmount
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
        public string FirstOutput
        {
            get { return _firstOutput; }
            set
            {
                _firstOutput = value;
                OnPropertyChanged();
            }
        }
        public string SecondOutput
        {
            get { return _secondOutput; }
            set
            {
                _secondOutput = value;
                OnPropertyChanged();
            }
        }
        //Tax calculation part
        public double TaxInput
        {
            get { return _taxInput; }
            set
            {
                _taxInput = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }
        public string TaxOutput
        {
            get { return _taxOutput; }
            set
            {
                _taxOutput = value;
                OnPropertyChanged();
            }
        }
        public bool TaxVisible
        {
            get { return _taxVisible; }
            set
            {
                _taxVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //Constructor
        public CurrencyViewModel(ObservableCollection<Currency> currencies)
        {
            Currencies = currencies;

            //Set the property to a new Command containing the method I want to call
            SwapCommand = new Command(
            execute: () =>
            {
                SwapCurrencies();
            });
        }

        //Calculation
        public void MakeCalculation(bool isFirst)
        {
            if (isCalculating) return;
            try
            {
                isCalculating = true;

                double firstValue = FirstAmount != 0 ? FirstAmount : 1;
                double secondValue = SecondAmount != 0 ? SecondAmount : 1;

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
                        CalculationStation.GetValueByRates(
                        firstValue,
                        _currencies[leftIndex].Rate,
                        _currencies[rightIndex].Rate
                        ), 2);
                    oldFirstIndex = leftIndex;
                    oldSecondIndex = rightIndex;
                }
                else
                {
                    FirstAmount = Math.Round(
                        CalculationStation.GetValueByRates(
                        secondValue,
                        _currencies[rightIndex].Rate,
                        _currencies[leftIndex].Rate
                        ), 2);
                    oldFirstIndex = rightIndex;
                    oldSecondIndex = leftIndex;
                }

                double taxValue = 0;
                //Show or hide Tax
                if (_taxInput > 0)
                {
                    taxValue = Math.Round(SecondAmount * (_taxInput * 0.01), 2);
                    TaxOutput = $"+ Sales Tax: {taxValue}";
                    TaxVisible = true;
                }
                else
                {
                    _taxInput = 0;
                    TaxVisible = false;
                }

                //Final display
                FirstOutput = $"{_firstAmount} {_currencies[oldFirstIndex].Name}";
                SecondOutput = $"{_secondAmount + taxValue} {_currencies[oldSecondIndex].Name}";
            }
            finally
            {
                isCalculating = false;
            }
        }

        public void SwapCurrencies()
        {
            //Bit hacky relying on make calculation to flip it
            isCalculating = true;
            SecondCurrency = oldSecondIndex;
            isCalculating = false;
            FirstCurrency = oldSecondIndex;
        }
    }
}
