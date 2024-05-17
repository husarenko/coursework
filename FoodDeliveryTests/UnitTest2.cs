using FoodDeliveryApp.Models;
using FoodDeliveryApp.Models.Enums;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                UserId = "user123",
                UserName = "John Doe",
                UserPhone = "123456789",
                OrderDate = DateTime.Parse("2024-05-12"),
                DeliveryAddress = "123 Main St",
                paymentMethod = PaymentMethod.CreditCard,
                Items = new List<OrderItem>(),
                Status = OrderStatus.Ordered,
                TotalPrice = 50.99m
            };

            // Act

            // Assert
            Assert.Equal(1, order.Id);
            Assert.Equal("user123", order.UserId);
            Assert.Equal("John Doe", order.UserName);
            Assert.Equal("123456789", order.UserPhone);
            Assert.Equal(DateTime.Parse("2024-05-12"), order.OrderDate);
            Assert.Equal("123 Main St", order.DeliveryAddress);
            Assert.Equal(PaymentMethod.CreditCard, order.paymentMethod);
            Assert.NotNull(order.Items);
            Assert.Equal(OrderStatus.Ordered, order.Status);
            Assert.Equal(50.99m, order.TotalPrice);
        }

        [Fact]
        public void Order_SetProperties_WithItemsNotEmpty_ReturnsCorrectValues()
        {
            // Arrange
            var order = new Order
            {
                Id = 2,
                UserId = "user456",
                UserName = "Jane Doe",
                UserPhone = "987654321",
                OrderDate = DateTime.Parse("2024-05-15"),
                DeliveryAddress = "456 Elm St",
                paymentMethod = PaymentMethod.CreditCard,
                Items = new List<OrderItem>
        {
            new OrderItem { DishName = "Pizza", Quantity = 2, Price = 12.99m },
            new OrderItem { DishName = "Salad", Quantity = 1, Price = 8.49m }
        },
                Status = OrderStatus.Ordered,
                TotalPrice = 34.47m
            };

            // Act

            // Assert
            Assert.Equal(2, order.Id);
            Assert.Equal("user456", order.UserId);
            Assert.Equal("Jane Doe", order.UserName);
            Assert.Equal("987654321", order.UserPhone);
            Assert.Equal(DateTime.Parse("2024-05-15"), order.OrderDate);
            Assert.Equal("456 Elm St", order.DeliveryAddress);
            Assert.Equal(PaymentMethod.CreditCard, order.paymentMethod);
            Assert.NotNull(order.Items);
            Assert.NotEmpty(order.Items);
            Assert.Equal(OrderStatus.Ordered, order.Status);
            Assert.Equal(34.47m, order.TotalPrice);
        }

        [Fact]
        public void Order_CalculateTotalPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var order = new Order();
            order.Items = new List<OrderItem>
            {
                new OrderItem { DishName = "Burger", Quantity = 2, Price = 5.99m },
                new OrderItem { DishName = "Fries", Quantity = 1, Price = 3.49m }
            };

            // Act
            decimal totalPrice = 0;
            foreach (var item in order.Items)
            {
                totalPrice += item.Price * item.Quantity;
            }

            // Assert
            Assert.Equal(15.47m, totalPrice);
        }

        [Fact]
        public void Order_ChangeStatus_UpdatesStatusCorrectly()
        {
            // Arrange
            var order = new Order { Status = OrderStatus.On_The_Way };

            // Act
            order.Status = OrderStatus.Ordered;

            // Assert
            Assert.Equal(OrderStatus.Ordered, order.Status);
        }
    }
}
