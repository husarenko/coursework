using FoodDeliveryApp.Models.Enums;

namespace FoodDeliveryApp.Models
{
    public class AdminPanelViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public Restaurant restaurant { get; set; }
        public List<Dish> Dishes { get; set; }
        public Dish dish { get; set; }
        public List<Person> Persons { get; set; }
        public Person person { get; set; }

        public UserRole userRole { get; set; }
        public List<Order> Orders { get; set; }
        public Order order { get; set; }
    }

}
