using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public UserController(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.User.Where(u => u.IsActive==true).ToList();
            return View(users);
        }

        public IActionResult Add()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedDate = DateTime.Now;
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _context.User.FirstOrDefault(c => c.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = _context.User.FirstOrDefault(c => c.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var existinguser = _context.User.FirstOrDefault(c => c.UserId == user.UserId);
            if (existinguser == null)
            {
                return NotFound();
            }

            // Update fields
            existinguser.Fullname = user.Fullname;
            existinguser.Description = user.Description;
            existinguser.Password = user.Password;
            existinguser.Address = user.Address;

            existinguser.Phone = user.Phone;
            existinguser.Email = user.Email;
            existinguser.UserCode = user.UserCode;
            existinguser.Status = user.Status;
            existinguser.IsLocked = user.IsLocked;
            existinguser.Avatar = user.Avatar;


            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
