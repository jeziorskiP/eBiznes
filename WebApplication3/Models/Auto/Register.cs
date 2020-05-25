using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Auto
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Podaj hasło")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Potwierdź hasło")]
        [Compare("Password",ErrorMessage ="Hasło i potwierdzenie nie pasują")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
