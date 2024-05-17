namespace FoodDeliveryApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }        
    }
}