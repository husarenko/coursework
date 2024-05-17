using FoodDeliveryApp.Models;
using FoodDeliveryApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Person_SetProperties_ReturnsCorrectValues()
        {
            // Arrange
            var person = new Person
            {
                UserName = "john.doe@example.com",
                Role = UserRole.Admin,
                Cart = new List<OrderItem>()
            };

            // Act

            // Assert
            Assert.Equal("john.doe@example.com", person.UserName);
            Assert.Equal(UserRole.Admin, person.Role);
            Assert.NotNull(person.Cart);
            Assert.Empty(person.Cart);
        }

        [Fact]
        public void Person_ChangeRole_UpdatesRoleCorrectly()
        {
            // Arrange
            var person = new Person();

            // Act
            person.Role = UserRole.Admin;

            // Assert
            Assert.Equal(UserRole.Admin, person.Role);
        }
    }
}
