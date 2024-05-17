using FoodDeliveryApp.Models;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void OrderItem_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var orderItem = new OrderItem
            {
                Id = 1,
                DishId = 101,
                Quantity = 2,
                DishName = "Spaghetti Carbonara",
                Price = 10.99m
            };

            // Act

            // Assert
            Assert.Equal(1, orderItem.Id);
            Assert.Equal(101, orderItem.DishId);
            Assert.Equal(2, orderItem.Quantity);
            Assert.Equal("Spaghetti Carbonara", orderItem.DishName);
            Assert.Equal(10.99m, orderItem.Price);
        }

        [Fact]
        public void OrderItem_CalculateTotalPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var orderItem = new OrderItem
            {
                Id = 2,
                DishId = 102,
                Quantity = 3,
                DishName = "Chicken Caesar Salad",
                Price = 8.49m
            };

            // Act
            var totalPrice = orderItem.Quantity * orderItem.Price;

            // Assert
            Assert.Equal(25.47m, totalPrice);
        }
    }
}
