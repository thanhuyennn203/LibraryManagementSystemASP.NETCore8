using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public HomeController(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var carousel = _context.Carousel.ToList();
            ViewBag.Carousel = carousel;
            var books = _context.Book.Include(b => b.Author).Where(b => b.IsDeleted == false).ToList();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
