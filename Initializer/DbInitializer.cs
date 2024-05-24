using System.Runtime.CompilerServices;

namespace CarRentalApp.API.Initializer
{
    public static class DbInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var userSeeder = serviceProvider.GetRequiredService<UserSeeder>();
                userSeeder.SeedAsync().Wait();
            }
            return app;
        }
    }
}
