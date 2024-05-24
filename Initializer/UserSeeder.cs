using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.X86;

namespace CarRentalApp.API.Initializer
{
    public class UserSeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserSeeder(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            var users = new List<IdentityUser>
            {
                new IdentityUser
                {
                    UserName = "johndoe123@gmail.com",
                    Email = "johndoe123@gmail.com",


                },
                new IdentityUser
                {
                    UserName = "test123@gmail.com",
                    Email = "test123@gmail.com",


                },
                new IdentityUser
                {
                    UserName = "admin@nagarro.com",
                    Email = "admin@nagarro.com",


                },

             };

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, "Dotnet@12345"); 
            }

        }
    }
}