using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Models.Interfaces
{
    //Інтерфейси
    public interface IUser
    {
        public Task<IActionResult> MenuList(int restaurantId);
        public IActionResult Index();
        public Task<IActionResult> Cart();
        public Task<IActionResult> AddToCart(int dishId, int quantity);
        public Task<IActionResult> RemoveFromCart(int dishId);
        public Task<IActionResult> Checkout(UserPanelViewModel model);
        public IActionResult Order(int orderId);
        public Task<IActionResult> OrderHistory();
    }
}
