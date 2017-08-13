using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ExpenseReportRepo.Models;
using Microsoft.Extensions.Configuration;

namespace ExpenseReportRepo.Models
{
    public class ExpenseReportRepoContext : IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public ExpenseReportRepoContext (IConfigurationRoot config, DbContextOptions<ExpenseReportRepoContext> options)
            : base(options)
        {
            _config = config;
        }

        public DbSet<ExpenseReport> ExpenseReport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:ExpenseReportRepoContext"]);
        }

        public DbSet<ExpenseReportRepo.Models.User> User { get; set; }
    }
}
