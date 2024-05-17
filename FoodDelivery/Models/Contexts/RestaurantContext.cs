using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Models;

public class RestaurantContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
    {

    }
}