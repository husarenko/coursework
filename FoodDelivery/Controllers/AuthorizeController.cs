using FoodDelivery.Controllers;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers
{
    //Спадкування
    public class AuthorizeController : Controller
    {
        //Інкапсуляція
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly PersonContext _context;
        private readonly OrderContext _orderContext;

        public AuthorizeController(UserManager<Person> userManager, 
            SignInManager<Person> signInManager, PersonContext context, OrderContext orderContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _orderContext = orderContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Person { UserName = model.Name, Email = model.Email, PhoneNumber = "+380" + model.Phone };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt or account not found.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction(nameof(Login));
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        TempData["ChangePassError"] = "Невірний теперішній пароль!";
                    }
                    return View(model);
                }

                await _signInManager.RefreshSignInAsync(user);
                model.Changed = true;
                TempData["PasswordHasChanged"] = "Ваш пароль був успішно змінений!";
                return RedirectToAction("ViewProfile", "Authorize");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            user = _userManager.Users.Include(u => u.Cart).FirstOrDefault(u => u.Id == user.Id);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var orders = _orderContext.Orders.Where(o => o.UserId == user.Id).ToList();

            var model = new ViewProfileViewModel
            {
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                OrderHistory = orders
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewProfile(ViewProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                user.UserName = model.Name;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;

                var orders = _orderContext.Orders.Where(o => o.UserId == user.Id).ToList();
                model.OrderHistory = orders;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    model.Changed = true;
                    return View(model);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
