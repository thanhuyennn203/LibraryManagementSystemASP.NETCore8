using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public AdminController(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult CategoryManagement()
        //{
        //    var categories = _context.Category.ToList();
        //    return View(categories);
        //}

        //public IActionResult UserManagement()
        //{
        //    return View();
        //}

        //public IActionResult LoanManagement()
        //{
        //    return View();
        //}

        //public IActionResult BookManagement()
        //{
        //    return View();
        //}

        //public IActionResult AuthorManagement()
        //{
        //    return View();
        //}
    }
}
