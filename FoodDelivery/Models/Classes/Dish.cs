namespace FoodDeliveryApp.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
