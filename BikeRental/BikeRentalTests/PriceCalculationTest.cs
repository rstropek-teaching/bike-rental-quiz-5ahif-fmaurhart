using BikeRental.Controllers;
using BikeRental.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace BikeRentalTests
{
    [TestClass]
    public class PriceCalculationTest
    {

        private TestContext testContextInstance;

        [TestMethod]
        public void PriceLogic()
        {
            //Smaller 15 --> 0

            RentalController controller = new RentalController();

            Rental neu = new Rental()
            {
                BeginTime = new DateTime(2018, 2, 14, 8, 15, 00),
                EndTime = new DateTime(2018, 2, 14, 8, 25, 00),
            };

            //Bike with id 9 has a price first hour 3€ and Additional Hour 5€

            Assert.AreEqual(0, controller.calculateCost(neu, 9));
        }

        [TestMethod]
        public void PriceLogicOnlyFirstHour()
        {
            RentalController controller = new RentalController();
            Rental neu = new Rental()
            {
                BeginTime = new DateTime(2018, 2, 14, 12, 15, 00),
                EndTime = new DateTime(2018, 2, 14, 12, 52, 00),
            };


   

            Assert.AreEqual(3, controller.calculateCost(neu, 9));

        }

        [TestMethod]
        public void PriceLogicSomeHours()
        {
            RentalController controller = new RentalController();
            Rental neu = new Rental()
            {
                BeginTime = new DateTime(2018, 2, 14, 8, 15, 00),
                EndTime = new DateTime(2018, 2, 14, 10, 30, 00),
            };


            Assert.AreEqual(13, controller.calculateCost(neu, 9));
        }

        [TestMethod]
        public void PriceLogicKomplex()
        {
            RentalController controller = new RentalController();
            Rental neu = new Rental()
            {
                BeginTime = new DateTime(2018, 2, 14, 8, 15, 00),
                EndTime = new DateTime(2018, 2, 14, 13, 16, 00),
            };


            Assert.AreEqual(28, controller.calculateCost(neu, 9));
        }
    }
}
    
