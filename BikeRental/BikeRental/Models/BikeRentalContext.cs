using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class BikeRentalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RentalBikeDB;");
        }
        public BikeRentalContext(DbContextOptions<BikeRentalContext> options)
        : base(options)
        { }

        public BikeRentalContext()
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
