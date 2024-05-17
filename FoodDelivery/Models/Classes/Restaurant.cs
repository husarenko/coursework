namespace FoodDeliveryApp.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string restaurantName { get; set; }
        public string Address { get; set; }
        public string? PhotoUrl { get; set; }
    }
}