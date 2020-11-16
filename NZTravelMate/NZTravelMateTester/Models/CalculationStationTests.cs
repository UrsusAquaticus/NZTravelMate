using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NZTravelMate.Models.Tests
{
    [TestClass()]
    public class CalculationStationTests
    {
        [TestMethod()]
        public void GetValueByRatesTest()
        {
            //Arrange
            double value = 4;
            double leftRate = 1.2;
            double rightRate = 0.8;
            double expected = 2.66666666667;

            //Act
            var actual = CalculationStation.GetValueByRates(value, leftRate, rightRate);

            //Assert
            //Close enough
            Assert.IsTrue(Math.Abs(expected - actual) < 0.01);
        }
    }
}