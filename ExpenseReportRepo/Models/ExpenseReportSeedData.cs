using Microsoft.AspNetCore.Identity;
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

        public ExpenseReportSeedData(ExpenseReportRepoContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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

                await _context.SaveChangesAsync();
                
            }
        }
    }
}
