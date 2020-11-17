using Microsoft.VisualStudio.TestTools.UnitTesting;
using NZTravelMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NZTravelMate.Models.Tests
{
    [TestClass()]
    public class OpenExchangeRateApiTests
    {
        [TestMethod()]
        public void GetRatesTest()
        {
            //Arrange
            string exampleRates = "{ 'NZD':'1', 'AUD':'0.85'}";
            string testRate1 = "1";
            string testRate2 = "0.85";

            //Act
            Names rates = OpenExchangeRateApi.GetNames(exampleRates);

            //Assert
            Assert.AreEqual(rates.NZD, testRate1);
            Assert.AreEqual(rates.AUD, testRate2);
        }

        [TestMethod()]
        public void GetNamesTest()
        {
            //Arrange
            string exampleNames = "{ 'NZD':'New Zealand Dollar', 'AUD':'Australian Dollar'}";
            string testName1 = "New Zealand Dollar";
            string testName2 = "Australian Dollar";

            //Act
            Names names = OpenExchangeRateApi.GetNames(exampleNames);

            //Assert
            Assert.AreEqual(names.NZD, testName1);
            Assert.AreEqual(names.AUD, testName2);
        }

        //[TestMethod()]
        //public void GetCurrenciesTest()
        //{
        //    //Arrange
        //    Names testNames = new Names { NZD = "New Zealand Dollar", AUD = "Australian Dollar" };
        //    Rates testRates = new Rates { NZD = 1, AUD = 0.85 };

        //    //Act
        //    System.Collections.ObjectModel.ObservableCollection<Currency> testCurrencies = OpenExchangeRateApi.GetCurrencies(testRates, testNames);

        //    //Assert
        //    Assert.AreEqual(testCurrencies)
        //}
    }
}