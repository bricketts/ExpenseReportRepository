using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ExpenseReportRepo.Models;
using ExpenseReportRepo.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseReportRepo.Controllers
{
    public class AcountController : Controller
    {
        private SignInManager<User> _signInManager;

        public AcountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();            
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.UserName,
                    vm.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        
    }
}