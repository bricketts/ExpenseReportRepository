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
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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

        #region MVC Methods

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

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public IActionResult AddNewUser()
        {
            var model = new AddNewUserViewModel();
            model.Roles = new SelectList(_dbcontext.Roles);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(AddNewUserViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var user = new User { UserName = vm.Email, Email = vm.Email };
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, vm.RoleId);
                    if(addRole.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View(vm);
        }

        public async Task<IActionResult> Details(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = await GetUserById(id);
            if(user ==  null)
            {
                return NotFound();
            }
            
            var vm = new UserManagementDetailsViewModel
            {
                UserId = id,
                UserName = user.UserName,
                Email = user.Email,
                Password = "********",                
                Reports = NumberOfReports(user)
            };
            
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = await GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User updatedUser)
        {
            if (updatedUser == null) return BadRequest();

            var currentUser = _dbcontext.Users.First(x => x.Id == updatedUser.Id);

            currentUser.CopyData(updatedUser);
            await _dbcontext.SaveChangesAsync();

            return View(currentUser);
        }

        #endregion

        #region User Management Helper Methods
        private async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        private async Task<User> GetUserByName(string id)
        {
            return await _userManager.FindByNameAsync(id);
        }

        private SelectList GetAllRoles() => 
            new SelectList(_roleManager.Roles.OrderBy(r => r.Name));

        private int NumberOfReports(User user)
        {
            return _dbcontext.ExpenseReport.Count(r => r.UserName == user.UserName);
        }
        #endregion
    }
}
