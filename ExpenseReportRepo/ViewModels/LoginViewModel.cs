using System.ComponentModel.DataAnnotations;

namespace ExpenseReportRepo.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Rememeber Me")]
        public bool RememberMe { get; set; }
    }
}
