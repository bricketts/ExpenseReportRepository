using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseReportRepo.Models;
using ExpenseReportRepo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExpenseReportRepo.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ExpenseReportRepoContext _dbcontext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(ExpenseReportRepoContext dbContext, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UserManagementIndexViewModel
            {
                Users = _dbcontext.Users.OrderBy(u => u.UserName).Include(u => u.Roles).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            var user = await GetUserById(id);
            var vm = new UserManagementAddRoleViewModel
            {
                Roles = GetAllRoles(),
                UserId = id,
                UserName = user.UserName
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserManagementAddRoleViewModel rvm)
        {
            var user = await GetUserById(rvm.UserId);
            if (ModelState.IsValid)
            {
                
                var result = await _userManager.AddToRoleAsync(user, rvm.NewRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }                
            }
            rvm.UserName = user.UserName;
            rvm.Roles = GetAllRoles();
            return View(rvm);
            
        }

        private async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        private SelectList GetAllRoles() => 
            new SelectList(_roleManager.Roles.OrderBy(r => r.Name));

    }
}
