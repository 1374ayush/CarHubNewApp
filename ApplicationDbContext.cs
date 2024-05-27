using CarRentalApp.API.Initializer;
using CarRentalApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace CarRentalApp.API
{
    //DbContext class is like a bridge in b/w the application and the database , which is used to establish the connection
    //b/w the database and application
    public class ApplicationDbContext : IdentityDbContext
    {

        // DBContextOptions: To configure and provide all necessary information to the DbContext about
        // how it should connect to and interact with the database.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }
        
        //tables present in the database.
        public DbSet<Car> Cars { get; set; }
        public DbSet<RentalAgreement> RentalAgreements { get; set; }
        public DbSet<UserRentalAgreement> UserRentalAgreements { get; set; }
        public DbSet<CarInspection> CarInspections { get; set; }

    }
}
