using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Карткою")]
        CreditCard,
        [Display(Name = "Готівкою")]
        OnHand
    }
}
