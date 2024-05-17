using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Електрона пошта")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}