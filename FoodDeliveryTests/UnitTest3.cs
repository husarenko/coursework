using FoodDeliveryApp.Models;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class RestaurantTests
    {
        [Fact]
        public void Restaurant_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = 1,
                restaurantName = "Pizza Palace",
                Address = "456 Main St",
                PhotoUrl = "https://example.com/photo.jpg"
            };

            // Act

            // Assert
            Assert.Equal(1, restaurant.Id);
            Assert.Equal("Pizza Palace", restaurant.restaurantName);
            Assert.Equal("456 Main St", restaurant.Address);
            Assert.Equal("https://example.com/photo.jpg", restaurant.PhotoUrl);
        }
    }
}
