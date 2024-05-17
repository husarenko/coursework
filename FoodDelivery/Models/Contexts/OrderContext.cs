using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Models
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
    }
}
