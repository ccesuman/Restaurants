using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace Restaurants.APITests.Controllers
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        // private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();
        // private readonly Mock<IRestaurantsSeeder> _restaurantsSeederMock = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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

        [Fact()]
        public void GetByIdTest()
        {

        }

        [Fact()]
        public void CreateRestaurantTest()
        {

        }

        [Fact()]
        public void UpdateRestaurantTest()
        {

        }

        [Fact()]
        public void DeleteRestaurantTest()
        {

        }
    }
}