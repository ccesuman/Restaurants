using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 🔴 Remove existing DbContext (SQL Server)
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<RestaurantsDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // ✅ Add InMemory Database
            services.AddDbContext<RestaurantsDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
        });
    }
}