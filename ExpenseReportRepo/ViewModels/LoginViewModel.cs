using System.ComponentModel.DataAnnotations;

namespace ExpenseReportRepo.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Rememeber Me")]
        public bool RememberMe { get; set; }
    }
}
