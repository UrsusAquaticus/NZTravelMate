using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NZTravelMate.Models
{
    static class CurrencyDataReader
    {
        //https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getmanifestresourcestream?view=netcore-3.1#System_Reflection_Assembly_GetManifestResourceStream_System_String_

        public async static Task<Dictionary<string, string>> NamesByCode(string targetFile)
        {
            var fileStream = GetFileStream(targetFile);
            var rawCurrencyData = await GetRawCurrencyData(fileStream);
            var currencyDataNoNull = RemoveNullCodes(rawCurrencyData);
            return GetDictionaryNamesByCode(currencyDataNoNull);
        }

        //Get ISO-4217 codes and their relevant long form currency name
        static Stream GetFileStream(string targetFile)
        {
            Stream stream = null;
            try
            {
                //Get the assembly so you can find the embeded resource file
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(CurrencyDataReader)).Assembly;
                stream = assembly.GetManifestResourceStream(targetFile); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tStream Failed: {0}", ex.Message);
            }
            //Set up to read json file
            return stream;
        }

        //Read from .JSON file
        static async Task<List<RawCurrencyData>> GetRawCurrencyData(Stream stream)
        {
            List<RawCurrencyData> currencyData = new List<RawCurrencyData>();
            try
            {
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    currencyData = JsonConvert.DeserializeObject<RawCurrencyData[]>(json).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return currencyData;
        }

        //Make sure no codes are null
        static List<RawCurrencyData> RemoveNullCodes (List<RawCurrencyData> rawCurrencyData)
        {
            return rawCurrencyData.Where(x => !string.IsNullOrEmpty(x.Alphabetic_Code)).ToList();
        }

        //Capture and save relevant info to dictionary
        static Dictionary<string, string> GetDictionaryNamesByCode(List<RawCurrencyData> rawCurrencyData)
        {
            Dictionary<string, string> NameByCode = new Dictionary<string, string>();
            try
            {
                foreach (RawCurrencyData RCD in rawCurrencyData)
                {
                    if (!NameByCode.ContainsKey(RCD.Alphabetic_Code)) 
                        NameByCode.Add(RCD.Alphabetic_Code, RCD.Currency);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return NameByCode;
        }
    }

    //Read from ISO-4217 json file
    public class RawCurrencyData
    {
        public string Alphabetic_Code { get; set; } //code
        public string Currency { get; set; } //Long form name
        public string Entity { get; set; }
        public int? Minor_Unit { get; set; }
        public string Numeric_Code { get; set; }
        public string Withdrawal_Date { get; set; }
        public string Withdrawal_Interval { get; set; }
    }
}
