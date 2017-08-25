using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseReportRepo.ViewModels
{
    public class AddNewUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(256)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        public SelectList Roles { get; set; }
        public string RoleId { get; set; }

    }
}
