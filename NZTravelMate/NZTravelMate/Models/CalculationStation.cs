using NZTravelMate.ViewModels;
using System;
using System.Diagnostics;

namespace NZTravelMate.Models
{
    public class CalculationStation
    {
        //Requires a Currency View Model
        private CurrencyViewModel _currencyViewModel;

        public CalculationStation(CurrencyViewModel currencyViewModel)
        {
            _currencyViewModel = currencyViewModel;
        }

        //Main Calculation
        public static double GetValueByRates(double value, double left, double right)
        {
            //left rate converted to right rate
            //To convert from one currency to another, first convert to NZD
            try
            {
                //E.g USD -> NZD
                double baseValue = value / left;

                //E.G NZD -> AUS
                return baseValue * right;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tCalculation Failed: {0}", ex.Message);
                return 0;
            }
        }
    }
}