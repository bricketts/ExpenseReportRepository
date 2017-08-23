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
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
