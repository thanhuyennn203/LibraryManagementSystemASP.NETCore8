using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public AuthorController(LibraryManagementSystemContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var authors = _context.Author.Where(a => a.IsActive == true).ToList();
            return View(authors);
        }

        public IActionResult Add()
        {
            return View(new Author());
        }

        [HttpPost]
        public IActionResult Add(Author author)
        {
            if (ModelState.IsValid)
            {
                author.CreatedDate = DateTime.Now;
                _context.Author.Add(author);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }


        public IActionResult Delete(int id)
        {
            var author = _context.Author.FirstOrDefault(c => c.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }

            author.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var author = _context.Author.FirstOrDefault(c => c.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            var existingauthor = _context.Author.FirstOrDefault(c => c.AuthorId == author.AuthorId);
            if (existingauthor == null)
            {
                return NotFound();
            }

            // Update fields
            existingauthor.FirstName = author.FirstName;
            existingauthor.LastName = author.LastName;
            existingauthor.DateOfBirth = author.DateOfBirth;
            existingauthor.Biography = author.Biography;

            existingauthor.Nationality = author.Nationality;
            existingauthor.Email = author.Email;
            existingauthor.Website = author.Website;
            existingauthor.IsActive = author.IsActive;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
