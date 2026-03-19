using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // ✅ FIX 1: Prevent config crash
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var settings = new Dictionary<string, string>
        {
            {"ConnectionStrings:RestaurantsDb", "Fake"},
            {"BlobStorage:ConnectionString", "Fake"}
        };

            config.AddInMemoryCollection(settings);
        });

        // ✅ FIX 2: Replace DB
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<RestaurantsDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<RestaurantsDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // ✅ FIX 3: Ensure DB created
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RestaurantsDbContext>();
            List<Restaurant> restaurants = GetRestaurants();

            db.Restaurants.AddRange(restaurants);

            db.SaveChanges();
            db.Database.EnsureCreated();
        });
    }

    private static List<Restaurant> GetRestaurants()
    {
        User owner = new()
        {
            Email = "seed-user@test.com"
        };
        List<Restaurant> restaurants =
        [
            new()
                {
                    Owner = owner,
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                        "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes =
                    [
                        new ()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M,
                        },

                        new ()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M,
                        },
                    ],
                    Address = new ()
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU"
                    }
                },
                new ()
                {
                    Owner = owner,
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "London",
                        Street = "Boots 193",
                        PostalCode = "W1F 8SR"
                    }
                }
        ];
        return restaurants;
    }

}