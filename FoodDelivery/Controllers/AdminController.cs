using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using FoodDeliveryApp.Models.Interfaces;
using FoodDeliveryApp.Models.Delegates;

namespace FoodDeliveryApp.Controllers
{
    //Спадкування
    public class AdminController : Controller, IAdmin
    {
        //Інкапсуляція
        private readonly OrderContext _orderContext;
        private readonly PersonContext _personContext;
        private readonly RestaurantContext _restContext;
        private readonly UserManager<Person> _userManager;

        //Delegates
        public event OrderStatusChangedHandler OnOrderStatusChanged;

        public AdminController(PersonContext personContext, RestaurantContext restContext, UserManager<Person> userManager, OrderContext orderContext)
        {
            _orderContext = orderContext;
            _userManager = userManager;
            _personContext = personContext;
            _restContext = restContext;
        }

        public async Task<IActionResult> Index() 
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == UserRole.Admin)
            {
                var viewModel = new AdminPanelViewModel
                {
                    //Collections.Generic
                    Restaurants = _restContext.Restaurants.ToList(),
                    restaurant = new Restaurant(),
                    Persons = _personContext.Persons.ToList(),
                    person = new Person(),
                    Orders = _orderContext.Orders.ToList(),
                    order = new Order()
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ChangeOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orderContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
                _orderContext.SaveChanges();

                //Delegates
                OnOrderStatusChanged?.Invoke(orderId, status);

                TempData["OrderSuccess"] = "Статус замовлення " + order.Id + " змінений на " + order.Status;
            }
            return RedirectToAction("Index");
        }

        public IActionResult UpdateRestaurant(int id, string restaurantName, string address, string photoUrl)
        {
            var restaurant = _restContext.Restaurants.Find(id);
            if (restaurant != null)
            {
                restaurant.restaurantName = restaurantName;
                restaurant.Address = address;
                restaurant.PhotoUrl = photoUrl;
                _restContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> ViewMenu(int restaurantId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == UserRole.Admin) 
            { 
                var restaurant = _restContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);

                if (restaurant == null)
                {
                    return NotFound();
                }

                //Collections.Generic
                var dishes = _restContext.Dishes.Where(d => d.RestaurantId == restaurantId).ToList();

                var viewModel = new AdminPanelViewModel
                {
                    restaurant = restaurant,
                    Dishes = dishes
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteDish(int id)
        {
            var dish = _restContext.Dishes.FirstOrDefault(d => d.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            _restContext.Dishes.Remove(dish);
            _restContext.SaveChanges();

            return RedirectToAction("ViewMenu", new { restaurantId = dish.RestaurantId });
        }

        public IActionResult UpdateDish(Dish dish, int restaurantId)
        {
            if (ModelState.IsValid)
            {
                dish.RestaurantId = restaurantId;
                _restContext.Dishes.Update(dish);
                _restContext.SaveChanges();

                return RedirectToAction("ViewMenu", new { restaurantId });
            }

            return View("ViewMenu");
        }

        [HttpPost]
        public IActionResult AddDish(Dish dish, int restaurantId)
        {
            if (ModelState.IsValid)
            {
                dish.RestaurantId = restaurantId;
                _restContext.Dishes.Add(dish);
                _restContext.SaveChanges();
                return RedirectToAction("ViewMenu", new { restaurantId });
            }

            return View("ViewMenu");
        }


        [HttpPost]
        public IActionResult AddRestaurant(Restaurant restaurant, Dish dish)
        {
            if (ModelState.IsValid)
            {
                restaurant.PhotoUrl = "";
                dish.Name = "";
                _restContext.Restaurants.Add(restaurant);
                _restContext.SaveChanges();
                dish.RestaurantId = restaurant.Id;
                _restContext.Dishes.Add(dish);
                _restContext.SaveChanges();
                TempData["RestSuccess"] = "Ресторан " + restaurant.restaurantName + " успішно доданий!";

                return RedirectToAction("Index");
            }

            var viewModel = new AdminPanelViewModel
            {
                Restaurants = _restContext.Restaurants.ToList(),
                restaurant = restaurant
            };

            return View("Index", viewModel);
        }

        public IActionResult DeleteRestaurant(int restaurantId)
        {
            var restaurant = _restContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);

            var dishes = _restContext.Dishes.Where(d => d.RestaurantId == restaurantId).ToList();
            _restContext.Dishes.RemoveRange(dishes);

            _restContext.Restaurants.Remove(restaurant);
            _restContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
