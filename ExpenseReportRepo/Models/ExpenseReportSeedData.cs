﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseReportRepo.Models
{
    public class ExpenseReportSeedData
    {
        private ExpenseReportRepoContext _dbcontext;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ExpenseReportSeedData(ExpenseReportRepoContext context, 
                                     UserManager<User> userManager, 
                                     RoleManager<IdentityRole> roleManager)
        {
            _dbcontext = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedData()
        {
            if(await _userManager.FindByEmailAsync("bricketts@email.com") == null)
            {
                var user = new User()
                {
                    UserName = "bricketts@email.com",
                    Email = "bricketts@email.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rD!");                
            }

            if(await _userManager.FindByEmailAsync("admin@email.com") == null)
            {
                var admin = new User()
                {
                    UserName = "admin@email.com",
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

            if (!_dbcontext.ExpenseReport.Any())
            {
                var newReport = new ExpenseReport
                {
                    UserName = "bricketts@email.com",
                    Name = "Site Services to SV213-01",
                    DateSubmitted = DateTime.Now,
                    DatePaid = DateTime.Now
                };

                _dbcontext.ExpenseReport.Add(newReport);

                var newReport01 = new ExpenseReport
                {
                    UserName = "bricketts@email.com",
                    Name = "Site Services to SV167-04",
                    DateSubmitted = DateTime.Now,
                    DatePaid = DateTime.Now
                };

                _dbcontext.ExpenseReport.Add(newReport01);

                var newReport03 = new ExpenseReport
                {
                    UserName = "admin@email.com",
                    Name = "Site Services to SLC_03",
                    DateSubmitted = DateTime.Now,
                    DatePaid = DateTime.Now
                };

                _dbcontext.ExpenseReport.Add(newReport03);

                await _dbcontext.SaveChangesAsync();
                
            }
        }
    }
}
