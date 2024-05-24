using CarRentalApp.API.Initializer;
using CarRentalApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.API.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            
        }
        //adding models
        public DbSet<Car> Cars { get; set; }
        public DbSet<RentalAgreement> RentalAgreements { get; set; }
        public DbSet<UserRentalAgreement> UserRentalAgreements { get; set; }
        public DbSet<CarInspection> CarInspections { get; set; }


    }
}
