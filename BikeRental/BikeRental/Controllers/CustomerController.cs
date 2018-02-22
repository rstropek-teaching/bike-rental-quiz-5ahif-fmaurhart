using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        public readonly BikeRentalContext Context;
        public CustomerController(BikeRentalContext context1)
        {
            Context = context1;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(Context.Customers);
        }


        [HttpPut]
        public IActionResult CreateCustomers([FromBody]Customer cust)
        {
            if (cust == null)
            {
                return BadRequest("Error");
            }
            Context.Customers.Add(cust);
            Context.SaveChanges();
            return Ok("Customer created");
        }

        [Route("{index}")]
        [HttpPut]
        public IActionResult UpdateCustomers(int index, [FromBody] Customer cust)
        {
            if (!Context.Customers.Any(a => a.ID == index))
            {
                return BadRequest("No Customer found");
            }

            var customer = Context.Customers.Where(a => a.ID == index).FirstOrDefault();
            customer.Gender = cust.Gender;
            customer.FirstName = cust.FirstName;
            customer.LastName = cust.LastName;
            customer.Street = cust.Street;
            customer.Birthday = cust.Birthday;
            customer.HouseNumber = cust.HouseNumber;
            customer.Town = cust.Town;
            customer.PLZ = cust.PLZ;
            Context.SaveChanges();

            return Ok("Customer updated");
        }

        [Route("{index}")]
        [HttpDelete]
        public IActionResult DeleteCustomers(int index)
        {
            if (!Context.Customers.Any(a => a.ID == index))
            {
                return BadRequest("No Customer found");
            }

            if (Context.Rentals.Any(a => a.Customer.ID == index))
            {
                return BadRequest("Delete not Possible: Customer has rented a bike ");
            }

            Context.Customers.Remove(Context.Customers.Where(a => a.ID == index).FirstOrDefault());
            Context.SaveChanges();


            return Ok("Customer deleted");
        }

        [Route("{index}")]
        [HttpGet]
        public IActionResult GetRentalsCustomers(int index)
        {
            if (!Context.Customers.Any(a => a.ID == index))
            {
                return BadRequest("No Customer found");
            }

            return Ok(Context.Rentals.Where(a => a.Customer.ID == index));
        }

    }
}
