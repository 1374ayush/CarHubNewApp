using CarRentalApp.API.DAO.Abstract;
using CarRentalApp.API.DAO.Implementation;
using CarRentalApp.API.Data;
using CarRentalApp.API.Initializer;
using CarRentalApp.API.Services.Abstract;
using CarRentalApp.API.Services.Implementation;
using CarRentalApp.API.Services_layer.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.

            //service for db context class
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            //provide the user class and role class
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddControllers();


            //adding business services
            
            builder.Services.AddScoped<ICarCrudService, CarCrudService>();
            builder.Services.AddScoped<ICarRentService, CarRentService>();
            builder.Services.AddScoped<UserSeeder>();
            builder.Services.AddScoped<ICarCrudRepository, CarCrudRepository>();
            builder.Services.AddScoped<ICarRentRepository, CarRentRepository>();
            builder.Services.AddScoped<IAuthService,AuthService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Seed();


            app.MapControllers();

            app.Run();
        }
    }
}