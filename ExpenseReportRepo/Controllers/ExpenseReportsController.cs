using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseReportRepo.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseReportRepo.Controllers
{
    [Authorize]
    public class ExpenseReportsController : Controller
    {
        private readonly ExpenseReportRepoContext _context;

        #region MVC Methods
        public ExpenseReportsController(ExpenseReportRepoContext context)
        {
            _context = context;    
        }

        // GET: ExpenseReports
        
        public async Task<IActionResult> Index()
        {
            ViewData["MonthlyReceived"] = MonthlyAmountReceived(_context.ExpenseReport);
            ViewData["YearlyReceived"] = YearlyAmountReceived(_context.ExpenseReport);
            ViewData["TotalReceived"] = TotalAmountReceived(_context.ExpenseReport);
            var UserReports = await _context.ExpenseReport
                .Where(t => t.UserName == User.Identity.Name)
                .ToListAsync();
            return View(UserReports);
        }

        // GET: ExpenseReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseReport = await _context.ExpenseReport
                .SingleOrDefaultAsync(m => m.ID == id);
            if (expenseReport == null)
            {
                return NotFound();
            }

            return View(expenseReport);
        }

        // GET: ExpenseReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,Name,Amount,DateSubmitted,DatePaid")] ExpenseReport expenseReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseReport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(expenseReport);
        }

        // GET: ExpenseReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseReport = await _context.ExpenseReport.SingleOrDefaultAsync(m => m.ID == id);
            if (expenseReport == null)
            {
                return NotFound();
            }
            return View(expenseReport);
        }

        // POST: ExpenseReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,Name,Amount,DatePaid,DateSubmitted")] ExpenseReport expenseReport)
        {
            if (id != expenseReport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseReportExists(expenseReport.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(expenseReport);
        }

        // GET: ExpenseReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseReport = await _context.ExpenseReport
                .SingleOrDefaultAsync(m => m.ID == id);
            if (expenseReport == null)
            {
                return NotFound();
            }

            return View(expenseReport);
        }

        // POST: ExpenseReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseReport = await _context.ExpenseReport.SingleOrDefaultAsync(m => m.ID == id);
            _context.ExpenseReport.Remove(expenseReport);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion


        #region Private Helper Methods
        private bool ExpenseReportExists(int id)
        {
            return _context.ExpenseReport.Any(e => e.ID == id);
        }

        //Returns the value for total amount received for the CURRENT month
        private decimal MonthlyAmountReceived(IEnumerable<ExpenseReport> reports)
        {
            //Create variable to store query filtered by current month
            var currentMonthAmount = reports.Where(r => r.DatePaid.Month == DateTime.Now.Month && r.UserName == User.Identity.Name);
            //Return total values for the Amount field for current filter
            return currentMonthAmount.Sum(r => r.Amount);
        }

        //Returns the value for total amount receieved for the CURRENT YEAR
        private decimal YearlyAmountReceived(IEnumerable<ExpenseReport> reports)
        {
            //Create variable to store query filtered by current year
            var currentYearlyAmount = reports.Where(r => r.DatePaid.Year == DateTime.Now.Year && r.UserName == User.Identity.Name);
            //return total values for Amount field for current filter
            return currentYearlyAmount.Sum(r => r.Amount);
        }

        //Returns the value for total amount received. 
        private decimal TotalAmountReceived(IEnumerable<ExpenseReport> reports)
        {
            //Total values for Amount field of all db entries
            return reports.Where(r => r.UserName == User.Identity.Name)
                          .Sum(r => r.Amount);
        }

        #endregion
    }
}
