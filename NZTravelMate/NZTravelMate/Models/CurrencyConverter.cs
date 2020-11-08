using NZTravelMate.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NZTravelMate.Models
{
    public class CurrencyConverter
    {
        //Requires a Currency View Model
        private CurrencyViewModel _currencyViewModel;
        public CurrencyConverter(CurrencyViewModel currencyViewModel)
        {
            _currencyViewModel = currencyViewModel;
        }

        //Main Calculation
        public static double GetValueByRates (double value, double left, double right)
        {
            //left rate converted to right rate
            //To convert from one currency to another, first convert to NZD 

            //E.g USD -> NZD 
            double baseValue = value / left;

            //E.G NZD -> AUS
            return baseValue * right;
        }
    }
}
