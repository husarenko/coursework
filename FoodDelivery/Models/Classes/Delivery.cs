using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Models
{
    public class Delivery
    {
        public string deliveryStatus { get; set; }
        public string deliveryAddress { get; set; }
        public string deliveryId { get; set; }
    }
}
