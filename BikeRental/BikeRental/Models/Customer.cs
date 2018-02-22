using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Customer
    {

        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }


        [Required]
        [MaxLength(75)]
        public String LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(75)]
        public String Street { get; set; }

        [MaxLength(10)]
        public int HouseNumber { get; set; }

        //String because in some coutnrys the plz consists of numbers and letters
        [MaxLength(10)]
        [Required]
        public String PLZ { get; set; }

        [MaxLength(75)]
        [Required]
        public String Town { get; set; }
    }
}
