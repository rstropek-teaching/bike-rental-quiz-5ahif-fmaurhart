using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Rental
    {
        
        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Bikes Bike { get; set; }
        [Required]
        public DateTime BeginTime { get; set; }

        [DateGreateAnotation("BeginTime")]
        public DateTime EndTime { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(1.0,Double.MaxValue)]
        public double TotalCost { get; set; }
         
        public Boolean Paid { get; set; }
    }
}
