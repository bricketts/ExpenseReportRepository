using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ExpenseReportRepo.Models;

namespace ExpenseReportRepo.Migrations
{
    [DbContext(typeof(ExpenseReportRepoContext))]
    [Migration("20170729032252_Dates")]
    partial class Dates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpenseReportRepo.Models.ExpenseReport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("DatePaid");

                    b.Property<DateTime>("DateSubmitted");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ExpenseReport");
                });
        }
    }
}
