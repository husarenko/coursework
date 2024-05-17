using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "У шляху")]
        On_The_Way,
        [Display(Name = "Доставлено")]
        Ordered
    }
}