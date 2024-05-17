using FoodDeliveryApp.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryApp.Models
{
    public class Person : IdentityUser
    {
        public UserRole Role { get; set; } = UserRole.User;

        public List<OrderItem>? Cart { get; set; } = null;
    }
}