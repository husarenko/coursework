using FoodDeliveryApp.Models;
using FoodDeliveryApp.Models.Enums;
using System.Collections.Generic;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class AdminPanelViewModelTests
    {
        [Fact]
        public void AdminPanelViewModel_SetProperties_ReturnsCorrectValues()
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

            var persons = new List<Person>
            {
                new Person { Id = "1", UserName = "john_doe", Role = UserRole.Admin },
                new Person { Id = "2", UserName = "jane_doe", Role = UserRole.User }
            };

            var person = new Person
            {
                Id = "3",
                UserName = "admin_user",
                Role = UserRole.Admin
            };

            var orders = new List<Order>
            {
                new Order { Id = 1, UserId = "user123", UserName = "John Doe", UserPhone = "123456789", DeliveryAddress = "123 Main St", TotalPrice = 50.99m },
                new Order { Id = 2, UserId = "user456", UserName = "Jane Doe", UserPhone = "987654321", DeliveryAddress = "456 Elm St", TotalPrice = 30.75m }
            };

            var order = new Order
            {
                Id = 3,
                UserId = "user789",
                UserName = "Mike Smith",
                UserPhone = "555555555",
                DeliveryAddress = "789 Oak St",
                TotalPrice = 25.99m
            };

            var adminPanelViewModel = new AdminPanelViewModel
            {
                Restaurants = restaurants,
                restaurant = restaurant,
                Dishes = dishes,
                dish = dish,
                Persons = persons,
                person = person,
                userRole = UserRole.Admin,
                Orders = orders,
                order = order
            };

            // Act

            // Assert
            Assert.Equal(restaurants, adminPanelViewModel.Restaurants);
            Assert.Equal(restaurant, adminPanelViewModel.restaurant);
            Assert.Equal(dishes, adminPanelViewModel.Dishes);
            Assert.Equal(dish, adminPanelViewModel.dish);
            Assert.Equal(persons, adminPanelViewModel.Persons);
            Assert.Equal(person, adminPanelViewModel.person);
            Assert.Equal(UserRole.Admin, adminPanelViewModel.userRole);
            Assert.Equal(orders, adminPanelViewModel.Orders);
            Assert.Equal(order, adminPanelViewModel.order);
        }

        [Fact]
        public void AdminPanelViewModel_SetEmptyProperties_ReturnsEmptyCollections()
        {
            // Arrange
            var adminPanelViewModel = new AdminPanelViewModel
            {
                Restaurants = new List<Restaurant>(),
                Dishes = new List<Dish>(),
                Persons = new List<Person>(),
                Orders = new List<Order>()
            };

            // Act

            // Assert
            Assert.Empty(adminPanelViewModel.Restaurants);
            Assert.Empty(adminPanelViewModel.Dishes);
            Assert.Empty(adminPanelViewModel.Persons);
            Assert.Empty(adminPanelViewModel.Orders);
        }
    }
}
