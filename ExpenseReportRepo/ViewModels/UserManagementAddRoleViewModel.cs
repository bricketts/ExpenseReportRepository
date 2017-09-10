using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseReportRepo.ViewModels
{
    public class UserManagementAddRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NewRole { get; set; }
        public SelectList Roles { get; set; }
    }
}
