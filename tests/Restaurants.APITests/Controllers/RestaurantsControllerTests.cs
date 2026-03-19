using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Seeders;
using Xunit;
namespace Restaurants.APITests.Controllers
{
    public class RestaurantsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();
        private readonly Mock<IRestaurantsSeeder> _restaurantsSeederMock = new();

        public RestaurantsControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(service =>
                {
                    service.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    service.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                        _ => _restaurantsRepositoryMock.Object));

                    service.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsSeeder),
                        _ => _restaurantsSeederMock.Object));
                });
            });

            _client = _factory.CreateClient();
        }

        #region GetRestaurant



        [Fact()]
            public async Task GetAll_ForValidRequest_Returns200Ok()
            {
                // Arrange
                var client = _factory.CreateClient();
                const string requestUri = "/api/restaurants?PageNumber=5&PageSize=5&SortBy=Name&SortDirection=0";

                // Act
                var result = await client.GetAsync(requestUri);

                // Assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            }

            [Fact]
            public async Task GetAll_ForInvalidRequest_Returns400()
            {
                // Arrange
                const string requestUri = "/api/restaurants";
                var client = _factory.CreateClient();

                // Act
                var result = await client.GetAsync(requestUri);

                // Assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            }

            #endregion

            #region GetById

            [Fact()]
            public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
            {
                // Arrange
                var id = 11233;

                _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);
                var client = _factory.CreateClient();

                // Act
                var response = await client.GetAsync($"/api/restaurants/{id}");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact()]
            public async Task GetById_ForExistingId_ShouldReturn200Ok()
            {
                // Arrange
                var id = 99;

                var restaurant = new Restaurant()
                {
                    Id = id,
                    Name = "Test Restaurant",
                    Description = "Test Description",
                    Category = "Test Category",
                    HasDelivery = true,
                    ContactEmail = "test@test.com",
                    ContactNumber = "1234567890",
                };

                _restaurantsRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(restaurant);
                var client = _factory.CreateClient();

                // Act
                var response = await client.GetAsync($"/api/restaurants/{id}");

                var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                restaurantDto.Should().NotBeNull();
                restaurantDto.Id.Should().Be(id);
                restaurantDto.Name.Should().Be(restaurant.Name);
            }


        #endregion

        }
    }