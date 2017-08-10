using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseReportRepo.Models
{
    public class ExpenseReport
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Display(Name = "Date Submitted")]
        [DataType(DataType.Date)]
        public DateTime DateSubmitted { get; set; }
        [Display(Name = "Date Paid")]
        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }
    }
}
