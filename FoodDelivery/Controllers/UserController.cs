using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FoodDeliveryApp.Models.Enums;
using FoodDeliveryApp.Models.Interfaces;

namespace FoodDeliveryApp.Controllers
{
    //Спадкування
    public class UserController : Controller, IUser
    {
        //Інкапсуляція
        private readonly UserManager<Person> _userManager;
        private readonly PersonContext _personContext;
        private readonly RestaurantContext _restContext;
        private readonly OrderContext _orderContext;

        public UserController(UserManager<Person> userManager, RestaurantContext restContext, PersonContext personContext, OrderContext orderContext)
        {
            _userManager = userManager;
            _orderContext = orderContext;
            _personContext = personContext;
            _restContext = restContext;
        }

        public async Task<IActionResult> MenuList(int restaurantId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null) 
            { 
                var restaurant = _restContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);

                if (restaurant == null)
                {
                    return NotFound();
                }

                //Collections.Generic
                var dishes = _restContext.Dishes.Where(d => d.RestaurantId == restaurantId).ToList();

                var viewModel = new UserPanelViewModel
                {
                    restaurant = restaurant,
                    Dishes = dishes
                };

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Будь ласка, увійдіть у свій акаунт або зареєструйтесь.";
                return RedirectToAction("Index", "User");
            }
        }

        public IActionResult Index()
        {
            var viewModel = new UserPanelViewModel
            {
                //Collections.Generic
                Restaurants = _restContext.Restaurants.ToList(),
                restaurant = new Restaurant()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Cart()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            currentUser = _userManager.Users.Include(u => u.Cart).FirstOrDefault(u => u.Id == currentUser.Id);

            var viewModel = new UserPanelViewModel
            {
                Cart = currentUser.Cart
            };

            return View(viewModel);
        }


        public async Task<IActionResult> AddToCart(int dishId, int quantity)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var dishToAdd = await _restContext.Dishes.FindAsync(dishId);

            if (dishToAdd == null)
            {
                return NotFound();
            }

            if (currentUser.Cart == null)
            {
                currentUser.Cart = new List<OrderItem>();
            }

            var existingItem = currentUser.Cart.FirstOrDefault(item => item.DishId == dishId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                currentUser.Cart.Add(new OrderItem
                {
                    DishId = dishId,
                    Quantity = quantity,
                    DishName = dishToAdd.Name,
                    Price = dishToAdd.Price
                });
            }

            await _userManager.UpdateAsync(currentUser);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int dishId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            currentUser = _userManager.Users.Include(u => u.Cart).FirstOrDefault(u => u.Id == currentUser.Id);
            if (currentUser == null || currentUser.Cart == null)
            {
                return RedirectToAction("Index");
            }

            var cartItem = currentUser.Cart.FirstOrDefault(item => item.DishId == dishId);
            if (cartItem != null)
            {
                currentUser.Cart.Remove(cartItem);
                await _userManager.UpdateAsync(currentUser);
            }

            return RedirectToAction("Cart", "User");
        }

        public async Task<IActionResult> Checkout(UserPanelViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            currentUser = _userManager.Users.Include(u => u.Cart).FirstOrDefault(u => u.Id == currentUser.Id);

            if (currentUser == null || currentUser.Cart == null || !currentUser.Cart.Any())
            {
                TempData["Message"] = "Кошик порожній щоб замовити доставку :(";
                return RedirectToAction("Index");
            }

            var orderItems = currentUser.Cart.Select(cartItem => new OrderItem
            {
                DishId = cartItem.DishId,
                Quantity = cartItem.Quantity,
                DishName = cartItem.DishName,
                Price = cartItem.Price
            }).ToList();

            //LINQ
            var totalPrice = orderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                UserId = currentUser.Id,
                UserName = currentUser.UserName,
                UserPhone = currentUser.PhoneNumber,
                DeliveryAddress = model.CurrentOrder.DeliveryAddress,
                paymentMethod = model.CurrentOrder.paymentMethod,
                Items = orderItems,
                TotalPrice = totalPrice
            };

            _orderContext.Orders.Add(order);
            await _orderContext.SaveChangesAsync();

            currentUser.Cart.Clear();
            await _userManager.UpdateAsync(currentUser);

            return RedirectToAction("Order", "User", new { orderId = order.Id });
        }

        public IActionResult Order(int orderId)
        {
            //LINQ
            var order = _orderContext.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new UserPanelViewModel
            {
                CurrentOrder = order
            };

            return View(viewModel);
        }

        public async Task<IActionResult> OrderHistory()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            //LINQ
            currentUser = _userManager.Users.Include(u => u.Cart).FirstOrDefault(u => u.Id == currentUser.Id);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Collections.Generic
            //LINQ
            var orders = _orderContext.Orders.Where(o => o.UserId == currentUser.Id).ToList();

            var viewModel = new UserPanelViewModel
            {
                OrderHistory = orders
            };

            return View(viewModel);
        }
    }
}

//public IActionResult AddToCart(int dishId, int quantity, string dishName)
//{
//    var dish = _restContext.Dishes.FirstOrDefault(d => d.Id == dishId);

//    if (dish == null)
//    {
//        return NotFound();
//    }

//    var user = _personContext.Persons
//        .Include(p => p.Cart)
//        .FirstOrDefault(p => p.UserName == User.Identity.Name);

//    if (user == null)
//    {
//        return NotFound();
//    }

//    user.Cart.Add(new OrderItem
//    {
//        DishId = dishId,
//        DishName = dishName,
//        Quantity = quantity
//    });
//    _personContext.SaveChanges();

//    return RedirectToAction("ViewCart", "UserController");
//}

//public IActionResult AddToCart(int dishId, int quantity)
//{
//    var currentUser = _userManager.GetUserAsync(User).Result;

//    var dish = _restContext.Dishes.FirstOrDefault(d => d.Id == dishId);

//    var newOrder = new Order
//    {
//        UserId = currentUser.Id,
//        UserName = currentUser.UserName,
//        UserPhone = currentUser.PhoneNumber,
//        TotalPrice = CalculateTotalPrice(dishId, quantity)
//    };


//    _personContext.Orders.Add(newOrder);
//    _personContext.SaveChanges();

//    var orderItem = new OrderItem
//    {
//        DishId = dishId,
//        Quantity = quantity,
//        DishName = dish.Name,
//        Price = dish.Price,
//        OrderId = currentUser.CartId
//    };

//    _personContext.OrderItems.Add(orderItem);
//    _personContext.SaveChanges();

//    return RedirectToAction("Index");
//}

//private decimal CalculateTotalPrice(int dishId, int quantity)
//{
//    var dish = _restContext.Dishes.FirstOrDefault(d => d.Id == dishId);

//    if (dish == null)
//    {
//        throw new InvalidOperationException("Dish not found");
//    }

//    decimal totalPrice = dish.Price * quantity;

//    return totalPrice;
//}

//public IActionResult ViewCart()
//{
//    var currentUser = _userManager.GetUserAsync(User).Result;

//    var orderItems = _personContext.OrderItems
//        .Where(oi => oi.OrderId == currentUser.CartId)
//        .Include(oi => oi.DishName)
//        .ToList();

//    decimal totalPrice = orderItems.Sum(oi => oi.Price * oi.Quantity);

//    var viewModel = new UserPanelViewModel
//    {
//        CartItems = orderItems,
//        TotalPrice = totalPrice
//    };

//    return View(viewModel);
//}