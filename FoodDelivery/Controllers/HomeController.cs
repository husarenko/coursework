using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Controllers;

//Спадкування
public class HomeController : Controller
{
    //Інкапсуляція
    private readonly UserManager<Person> _userManager;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, UserManager<Person> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            return RedirectToAction("Index", "User");
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}