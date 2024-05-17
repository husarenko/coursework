using FoodDeliveryApp.Models;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class DishTests
    {
        [Fact]
        public void Dish_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var dish = new Dish
            {
                Id = 1,
                RestaurantId = 100,
                Name = "Spaghetti Carbonara",
                Price = 10.99m
            };

            // Act

            // Assert
            Assert.Equal(1, dish.Id);
            Assert.Equal(100, dish.RestaurantId);
            Assert.Equal("Spaghetti Carbonara", dish.Name);
            Assert.Equal(10.99m, dish.Price);
        }

        [Fact]
        public void Dish_SetProperties_WithSpecialCharacters_ReturnsCorrectValues()
        {
            // Arrange
            var dish = new Dish
            {
                Id = 2,
                RestaurantId = 200,
                Name = "Burger with Bacon & Cheese",
                Price = 12.49m
            };

            // Act

            // Assert
            Assert.Equal(2, dish.Id);
            Assert.Equal(200, dish.RestaurantId);
            Assert.Equal("Burger with Bacon & Cheese", dish.Name);
            Assert.Equal(12.49m, dish.Price);
        }


        [Fact]
        public void Dish_CalculateTotalPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var dish = new Dish
            {
                Id = 1,
                RestaurantId = 100,
                Name = "Spaghetti Carbonara",
                Price = 10.99m
            };

            var quantity = 2;

            // Act
            var totalPrice = dish.Price * quantity;

            // Assert
            Assert.Equal(21.98m, totalPrice);
        }

        [Fact]
        public void Dish_CalculateTotalPrice_WithDifferentPriceAndQuantity_ReturnsCorrectTotal()
        {
            // Arrange
            var dish = new Dish
            {
                Id = 2,
                RestaurantId = 200,
                Name = "Chicken Caesar Salad",
                Price = 8.49m
            };

            var quantity = 3;

            // Act
            var totalPrice = dish.Price * quantity;

            // Assert
            Assert.Equal(25.47m, totalPrice);
        }
    }
}
