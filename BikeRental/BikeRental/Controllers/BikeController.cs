using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    public class BikeController : Controller
    {
        public readonly BikeRentalContext Context;
        public BikeController(BikeRentalContext context1)
        {
            Context = context1;
        }

        [HttpGet]
        public IActionResult GetAvailableBikes(String sort)
        {

            var result = Context.Bikes.Select(a => a).Where(e => !Context.Rentals.Any(f => f.Bike.ID == e.ID & f.BeginTime > f.EndTime)).
                OrderBy(b => sort == "firstHour" ? b.PriceFirstHour : sort == "addHour" ? b.PriceAddHour : sort == "purchase" ? b.ID : b.ID); //? Descending
            return Ok(result);

        }

        [HttpPut]
        public IActionResult CreateBike([FromBody] Bikes bike)
        {
            if (bike == null)
            {
                return BadRequest("Error");
            }
            Context.Bikes.Add(bike);
            Context.SaveChanges();
            return Ok("Bike Created");
        }

        [HttpPut]
        [Route("{index}")]
        public IActionResult UpdateBike(int index, [FromBody]Bikes bike)
        {
            if (!Context.Bikes.Any(a => a.ID == index))
            {
                return BadRequest("No Bike found");
            }
            var b = Context.Bikes.Where(a => a.ID == index).FirstOrDefault();
            b.Brand = bike.Brand;
            b.Category = bike.Category;
            b.LastService = bike.LastService;
            b.Notes = bike.Notes;
            b.PriceAddHour = bike.PriceAddHour;
            b.PriceFirstHour = bike.PriceFirstHour;
            b.PurchaseDate = bike.PurchaseDate;
            Context.SaveChanges();

            return Ok("Bike Updated");
        }

        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteBike(int index)
        {
            if (!Context.Bikes.Any(a => a.ID == index))
            {
                return BadRequest("No Bike found");
            }

            if (Context.Rentals.Any(a => a.Bike.ID == index))
            {
                return BadRequest("Delete Not Possible: Bike is rented");
            }

            Context.Bikes.Remove(Context.Bikes.Where(a => a.ID == index).FirstOrDefault());
            Context.SaveChanges();

            return Ok("Bike Deleted");
        }
    }
}