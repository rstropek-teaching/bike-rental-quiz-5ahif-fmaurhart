using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    public class RentalController : Controller
    {

        public readonly BikeRentalContext Context = new BikeRentalContext();




        [HttpGet]
        [Route("start")]
        public IActionResult StartRental(int IDCustomer, int IDBike)
        {
            //Check if Customer and Bike are valID
            if (!CheckID(IDCustomer, IDBike))
            {
                return BadRequest("IDs are not valID");
            }

            Rental neu = new Rental()
            {
                Customer = Context.Customers.First(a => a.ID == IDCustomer),
                Bike = Context.Bikes.First(a => a.ID == IDBike),
                BeginTime = DateTime.Now,
                /*EndTime = null,*/ //does not work
                EndTime = new DateTime(2000, 1, 1),
                Paid = false,
                TotalCost = 0
            };

            Context.Rentals.Add(neu);
            Context.SaveChanges();

            return Ok("Rental has start" + neu);
        }
        public Boolean CheckID(int IDC, int IDB)
        {
            if (!Context.Customers.Any(a => a.ID == IDC) || !Context.Bikes.Any(a => a.ID == IDB))
            {
                return false;
            }
            if (Context.Rentals.Any(a => a.Customer.ID == IDC))
            {
                return false;
            }
            if (Context.Rentals.Any(a => a.Bike.ID == IDB))
            {
                return false;
            }
            return true;
        }


        [HttpGet]
        [Route("stop")]
        public IActionResult StopRental(int IDCustomer, int IDBike)
        {
            var rent = Context.Rentals.First(a => a.Customer.ID == IDCustomer & a.Bike.ID == IDBike);
            rent.EndTime = DateTime.Now;


            //Calculate
            double cost = calculateCost(rent, IDBike);
            rent.TotalCost = cost;
            Context.SaveChanges();

            return Ok(rent);
        }

        public double calculateCost(Rental rent, int IDBike)
        {
            TimeSpan t = rent.EndTime - rent.BeginTime;
            double min = t.TotalMinutes;
            double cost = 0;

            if (min <= 15)
            {
                return 0;
            }

            cost += Context.Bikes.First(a => a.ID == IDBike).PriceFirstHour;
            min -= 60;
            //The first hour is over

            while (min > 0)
            {
                cost += Context.Bikes.First(a => a.ID == IDBike).PriceAddHour;
                min -= 60;
            }

            Console.WriteLine(cost);
            return cost;

        }


        [HttpGet]
        [Route("paid")]
        public IActionResult MarkPaID(int IDCustomer, int IDBike)
        {
            var rent = Context.Rentals.First(a => a.Customer.ID == IDCustomer & a.Bike.ID == IDBike);
            if (rent.TotalCost > 0)
            {
                rent.Paid = true;
            }
            else
            {
                return BadRequest("Customer has not paID");
            }
            Context.SaveChanges();
            return Ok("Customer (ID=" + IDCustomer + "), Bike (ID=" + IDBike + ") PaID");
        }


        [HttpGet]
        [Route("unpaid")]
        public IActionResult GetUnpaID()
        {
            return Ok(Context.Rentals.Where(a => a.Paid == false && a.EndTime > a.BeginTime && a.EndTime != null && a.TotalCost > 0).SelectMany(a => new Object[] { a.Customer.ID, a.Customer.FirstName, a.Customer.LastName, a.ID, a.BeginTime, a.EndTime, a.TotalCost }));
        }
    }
    }
