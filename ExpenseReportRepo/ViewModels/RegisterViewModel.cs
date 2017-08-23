using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseReportRepo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(50)]
        [Compare("Password", ErrorMessage = "Does not patch password")]
        public string ConfirmPassword { get; set; }
    }
}
