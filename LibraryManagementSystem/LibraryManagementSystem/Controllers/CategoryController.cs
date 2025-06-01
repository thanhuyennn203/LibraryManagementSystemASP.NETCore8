using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public CategoryController(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Category
                .Where(c => c.IsActive == true)
                .ToList();

            return View(categories);
        }

        public IActionResult Add()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedDate = DateTime.Now;
                _context.Category.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }


        public IActionResult Delete(int id)
        {
            var category = _context.Category.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            category.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Category.FirstOrDefault(c => c.CategoryId == id);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var existingCategory = _context.Category.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Update fields
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.Avatar = category.Avatar;
            existingCategory.IsActive = category.IsActive;
            existingCategory.UpdatedDate = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
