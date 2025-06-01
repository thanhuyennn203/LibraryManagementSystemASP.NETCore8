using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class LoanController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public LoanController(LibraryManagementSystemContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var loans = _context.Loan.Where(l=>l.Status!=5).ToList();
            return View(loans);
        }

        public IActionResult Delete(int id)
        {
            var loan = _context.Loan.FirstOrDefault(c => c.LoanId == id);

            if (loan == null)
            {
                return NotFound();
            }

            loan.Status = 5;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            ViewBag.Users = _context.User.Where(u => u.IsActive == true).ToList();
            ViewBag.Books = _context.Book.Where(b => b.IsDeleted == false).ToList();
            return View(new Loan());
        }

        [HttpPost]
        public IActionResult Add(Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Loan.Add(loan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loan);
        }

        public IActionResult Edit(int id)
        {
            var loan = _context.Loan.FirstOrDefault(c => c.LoanId == id);
            ViewBag.Users = _context.User.Where(u => u.IsActive == true).ToList();
            ViewBag.Books = _context.Book.Where(b => b.IsDeleted == false).ToList();
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        [HttpPost]
        public IActionResult Edit(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return View(loan);
            }

            var existingloan = _context.Loan.FirstOrDefault(c => c.LoanId == loan.LoanId);
            if (existingloan == null)
            {
                return NotFound();
            }

            // Update fields
            existingloan.UserId = loan.UserId;
            existingloan.BookId = loan.BookId;
            existingloan.LoanDate = loan.LoanDate;
            existingloan.DueDate = loan.DueDate;

            existingloan.ReturnDate = loan.ReturnDate;
            existingloan.Status = loan.Status;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
