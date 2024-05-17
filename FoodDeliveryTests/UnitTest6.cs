using FoodDeliveryApp.Models;
using FoodDeliveryApp.Models.Enums;
using System.Collections.Generic;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class UserPanelViewModelTests
    {
        [Fact]
        public void UserPanelViewModel_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, restaurantName = "Pizza Place", Address = "123 Main St", PhotoUrl = "https://example.com/photo1.jpg" },
                new Restaurant { Id = 2, restaurantName = "Sushi Spot", Address = "456 Elm St", PhotoUrl = "https://example.com/photo2.jpg" }
            };

            var restaurant = new Restaurant
            {
                Id = 3,
                restaurantName = "Burger Barn",
                Address = "789 Oak St",
                PhotoUrl = "https://example.com/photo3.jpg"
            };

            var dishes = new List<Dish>
            {
                new Dish { Id = 1, RestaurantId = 1, Name = "Margherita Pizza", Price = 9.99m },
                new Dish { Id = 2, RestaurantId = 1, Name = "Pepperoni Pizza", Price = 10.99m }
            };

            var dish = new Dish
            {
                Id = 3,
                RestaurantId = 2,
                Name = "California Roll",
                Price = 12.99m
            };

            var cart = new List<OrderItem>
            {
                new OrderItem { Id = 1, DishId = 1, Quantity = 2, DishName = "Margherita Pizza", Price = 9.99m },
                new OrderItem { Id = 2, DishId = 2, Quantity = 1, DishName = "Pepperoni Pizza", Price = 10.99m }
            };

            var currentOrder = new Order
            {
                Id = 1,
                UserId = "user123",
                UserName = "John Doe",
                UserPhone = "123456789",
                OrderDate = System.DateTime.Parse("2024-05-12"),
                DeliveryAddress = "123 Main St",
                paymentMethod = PaymentMethod.CreditCard,
                Items = cart,
                Status = OrderStatus.Ordered,
                TotalPrice = 30.97m
            };

            var orderHistory = new List<Order>
            {
                new Order
                {
                    Id = 2,
                    UserId = "user123",
                    UserName = "John Doe",
                    UserPhone = "123456789",
                    OrderDate = System.DateTime.Parse("2024-05-10"),
                    DeliveryAddress = "123 Main St",
                    paymentMethod = PaymentMethod.CreditCard,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { Id = 3, DishId = 3, Quantity = 1, DishName = "California Roll", Price = 12.99m }
                    },
                    Status = OrderStatus.Ordered,
                    TotalPrice = 12.99m
                }
            };

            var userPanelViewModel = new UserPanelViewModel
            {
                Restaurants = restaurants,
                restaurant = restaurant,
                Dishes = dishes,
                dish = dish,
                Cart = cart,
                CurrentOrder = currentOrder,
                OrderHistory = orderHistory
            };

            // Act

            // Assert
            Assert.Equal(restaurants, userPanelViewModel.Restaurants);
            Assert.Equal(restaurant, userPanelViewModel.restaurant);
            Assert.Equal(dishes, userPanelViewModel.Dishes);
            Assert.Equal(dish, userPanelViewModel.dish);
            Assert.Equal(cart, userPanelViewModel.Cart);
            Assert.Equal(currentOrder, userPanelViewModel.CurrentOrder);
            Assert.Equal(orderHistory, userPanelViewModel.OrderHistory);
        }

        [Fact]
        public void UserPanelViewModel_SetEmptyProperties_ReturnsEmptyCollections()
        {
            // Arrange
            var userPanelViewModel = new UserPanelViewModel
            {
                Restaurants = new List<Restaurant>(),
                Dishes = new List<Dish>(),
                Cart = new List<OrderItem>(),
                OrderHistory = new List<Order>()
            };

            // Act

            // Assert
            Assert.Empty(userPanelViewModel.Restaurants);
            Assert.Empty(userPanelViewModel.Dishes);
            Assert.Empty(userPanelViewModel.Cart);
            Assert.Empty(userPanelViewModel.OrderHistory);
        }
    }
}
