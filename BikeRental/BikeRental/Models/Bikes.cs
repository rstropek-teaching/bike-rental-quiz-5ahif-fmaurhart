using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Bikes
    {
        public int ID { get; set; }

        [MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public DateTime LastService { get; set; }


        [Range(0.00d, 9999d)]
        public double PriceFirstHour { get; set; }


        [Range(0.00d, 9999d)]
        public double PriceAddHour { get; set; }

        public Category Category { get; set; }
    }
}
