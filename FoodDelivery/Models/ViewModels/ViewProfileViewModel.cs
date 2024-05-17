namespace FoodDeliveryApp.Models
{
    public class ViewProfileViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Changed { get; set; } = false;
        public List<Order> OrderHistory { get; set; }

        public ViewProfileViewModel()
        {
            OrderHistory = new List<Order>();
        }
    }
}