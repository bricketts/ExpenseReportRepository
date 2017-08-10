using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ExpenseReportRepo.Models;

namespace ExpenseReportRepo.Models
{
    public class ExpenseReportRepoContext : IdentityDbContext<User>
    {
        public ExpenseReportRepoContext (DbContextOptions<ExpenseReportRepoContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseReport> ExpenseReport { get; set; }
    }
}
