using Microsoft.VisualStudio.TestTools.UnitTesting;
using NZTravelMate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NZTravelMate.Models.Tests
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod()]
        public void CurrencyTest()
        {
            //Arrange
            string code = "NZD";
            string name = "New Zealand Dollar";
            double rate = 1;

            //Act
            var testCurrency = new Currency {
                Code = code,
                Name = name,
                Rate = rate
            };

            //Assert
            Assert.AreEqual(testCurrency.Code, code);
            Assert.AreEqual(testCurrency.Name, name);
            Assert.AreEqual(testCurrency.Rate, rate);
        }
    }
}