using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Мобільний телефон")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Електрона пошта")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Підтвердіть пароль")]
        public string ConfirmPassword { get; set; }
    }
}
