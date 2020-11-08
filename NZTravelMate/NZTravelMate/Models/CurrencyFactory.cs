using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NZTravelMate.Models
{
    class CurrencyFactory
    {
        //Takes the conversion rates from API
        public static ObservableCollection<Currency> GetCurrencies(ConversionRates conversionRates)
        {
            PropertyInfo[] codeProperties = typeof(ConversionRates).GetProperties();

            List<Currency> tempCurrencies = new List<Currency>();
            foreach(var property in codeProperties)
            {
                var rate = (double)property.GetValue(conversionRates, null);
                var code = property.Name;

                //Name will come from Currency data
                var name = "Temp";

                tempCurrencies.Add(new Currency(code, name, rate));
                Debug.WriteLine($"Code: {code}, Rate: {rate}, Name: {name}");
            }
            //Alphabetise by code
            var sortedList = tempCurrencies.OrderBy(x => x.Code).ToList();
            return new ObservableCollection<Currency>(sortedList);
        }
    }
}
