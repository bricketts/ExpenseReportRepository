using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpenseReportRepo.Models
{
    public class ExpenseReportRepoContext : DbContext
    {
        public ExpenseReportRepoContext (DbContextOptions<ExpenseReportRepoContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseReportRepo.Models.ExpenseReport> ExpenseReport { get; set; }
    }
}
