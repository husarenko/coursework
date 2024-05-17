using FoodDeliveryApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Models.Interfaces
{
    //Інтерфейси
    public interface IAdmin
    {
        public Task<IActionResult> Index();
        public IActionResult ChangeOrderStatus(int orderId, OrderStatus status);
        public IActionResult UpdateRestaurant(int id, string restaurantName, string address, string photoUrl);
        public Task<IActionResult> ViewMenu(int restaurantId);
        public IActionResult DeleteDish(int id);
        public IActionResult UpdateDish(Dish dish, int restaurantId);
        public IActionResult AddDish(Dish dish, int restaurantId);
        public IActionResult AddRestaurant(Restaurant restaurant, Dish dish);
        public IActionResult DeleteRestaurant(int restaurantId);
    }
}
