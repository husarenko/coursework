using FoodDeliveryApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryApp.Models
{
    //Статичні члени
    public static class OrderPrice
    {
        public static decimal CalculateTotalPrice(List<OrderItem> orderItems)
        {
            return orderItems.Sum(item => item.Price * item.Quantity);
        }
    }
}
