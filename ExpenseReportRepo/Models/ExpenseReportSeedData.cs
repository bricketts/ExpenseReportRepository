using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseReportRepo.Models
{
    public class ExpenseReportSeedData
    {
        private ExpenseReportRepoContext _context;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ExpenseReportSeedData(ExpenseReportRepoContext context, 
                                     UserManager<User> userManager, 
                                     RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedData()
        {
            if(await _userManager.FindByEmailAsync("brody.ricketts@email.com") == null)
            {
                var user = new User()
                {
                    UserName = "bricketts",
                    Email = "brody.ricketts@email.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rD!");                
            }

            if(await _userManager.FindByEmailAsync("admin@email.com") == null)
            {
                var admin = new User()
                {
                    UserName = "admin",
                    Email = "admin@email.com"
                };


                await _userManager.CreateAsync(admin, "P@ssw0rD!");
            }

            if(await _roleManager.FindByNameAsync("Member") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            }

            if (await _roleManager.FindByNameAsync("Administrator") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });
            }

            if (await _roleManager.FindByNameAsync("Manager") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
            }

            if (!_context.ExpenseReport.Any())
            {
                var newReport = new ExpenseReport
                {
                    UserName = "bricketts",
                    Name = "Site Services to SV213-01",
                    DateSubmitted = DateTime.Now,
                    DatePaid = DateTime.Now
                };

                _context.ExpenseReport.Add(newReport);

                var newReport01 = new ExpenseReport
                {
                    UserName = "bricketts",
                    Name = "Site Services to SV167-04",
                    DateSubmitted = DateTime.Now,
                    DatePaid = DateTime.Now
                };

                _context.ExpenseReport.Add(newReport01);

                var newReport03 = new ExpenseReport
                {
                    UserName = "admin",
                    Name = "Site Services to SLC_03",
                    DateSubmitted = DateTime.Now,
                    DatePaid = DateTime.Now
                };

                _context.ExpenseReport.Add(newReport03);

                await _context.SaveChangesAsync();
                
            }
        }
    }
}
