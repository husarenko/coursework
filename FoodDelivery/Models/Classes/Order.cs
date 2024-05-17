using FoodDeliveryApp.Models.Enums;

namespace FoodDeliveryApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string DeliveryAddress { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.On_The_Way;
        public decimal TotalPrice { get; set; }
    }
}
