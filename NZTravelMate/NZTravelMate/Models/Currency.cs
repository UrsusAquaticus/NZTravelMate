using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NZTravelMate.Models
{
    public class Currency
    {
        public Currency(string code, string name, double rate)
        {
            Code = code;
            Name = name;
            Rate = rate;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
    }

    class CurrencyBuilder
    {
        //Takes the conversion rates from API
        public static ObservableCollection<Currency> GetCurrencies(ConversionRates conversionRates, Dictionary<string, string> namesByCode)
        {
            PropertyInfo[] codeProperties = typeof(ConversionRates).GetProperties();

            List<Currency> tempCurrencies = new List<Currency>();
            foreach (var property in codeProperties)
            {
                var rate = (double)property.GetValue(conversionRates, null);
                var code = property.Name;
                var name = namesByCode[code];

                tempCurrencies.Add(new Currency(code, name, rate));
                Debug.WriteLine($"Code: {code}, Rate: {rate}, Name: {name}");
            }
            //Alphabetise by code
            var sortedList = tempCurrencies.OrderBy(x => x.Code).ToList();
            return new ObservableCollection<Currency>(sortedList);
        }
    }
}
