using FoodDeliveryApp.Models.Enums;

namespace FoodDeliveryApp.Models.Delegates
{
    //Delegates
    public delegate void OrderStatusChangedHandler(int orderId, OrderStatus newStatus);
}
