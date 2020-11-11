using SQLite;
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
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        [PrimaryKey, MaxLength(3), NotNull]
        public string Code { get; set; }
        [MaxLength(255)]
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

                tempCurrencies.Add(new Currency
                {
                    Rate = rate,
                    Code = code,
                    Name = name
                });
                Debug.WriteLine($"Code: {code}, Rate: {rate}, Name: {name}");
            }
            //Alphabetise by code
            var sortedList = tempCurrencies.OrderBy(x => x.Code).ToList();
            return new ObservableCollection<Currency>(sortedList);
        }
    }
}
