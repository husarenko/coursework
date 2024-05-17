namespace FoodDeliveryApp.Models
{
    public class UserPanelViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public Restaurant restaurant { get; set; }
        public List<Dish> Dishes { get; set; }
        public Dish dish { get; set; }
        public List<OrderItem> Cart { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
    }
}
