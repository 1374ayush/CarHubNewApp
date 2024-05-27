using CarRentalApp.API.DAO.Abstract;
using CarRentalApp.API.DAO.Implementation;
using CarRentalApp.API.Initializer;
using CarRentalApp.API.Services.Abstract;
using CarRentalApp.API.Services.Implementation;
using CarRentalApp.API.Services_layer.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CarRentalApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.

            builder.Services.AddControllers();


            //adding business services
            
            builder.Services.AddScoped<ICarCrudService, CarCrudService>();
            builder.Services.AddScoped<ICarRentService, CarRentService>();
            builder.Services.AddScoped<UserSeeder>();
            builder.Services.AddScoped<ICarCrudRepository, CarCrudRepository>();
            builder.Services.AddScoped<ICarRentRepository, CarRentRepository>();
            builder.Services.AddScoped<IAuthService,AuthService>();


            //service for db context class
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            //provide the user class and role class
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //jwt authentication service
            builder.Services.AddAuthentication(options =>
            {
                //setting up the authentication schemes
                //The default authentication scheme determines the default method used by the application to authenticate users.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                //The default challenge scheme determines the method used to challenge the user for authentication
                //when a user tries to access a resource that requires authentication, but is not authenticated.
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

            // Add Authorization Service
            builder.Services.AddAuthorization();

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

            app.UseHttpsRedirection(); //for https

            app.UseAuthentication();

            //: it responsible for enforcing authorization policies.
            app.UseAuthorization();

            app.Seed(); //for seeding data

            app.MapControllers();

            app.Run();
        }
    }
}