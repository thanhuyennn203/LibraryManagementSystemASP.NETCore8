using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

public class BookController : Controller
{
    private readonly LibraryManagementSystemContext _context;

    public BookController(LibraryManagementSystemContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Book.Include(b => b.Author).Where(b => b.IsDeleted == false).ToList();
        ViewBag.Title = "All book available";
        return View(books);
    }
    public IActionResult Detail(int bookId)
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefault(b => b.BookId == bookId && b.IsDeleted == false);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    public IActionResult ViewAllProgrammingBook()
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category).Where(b=> b.CategoryId == 2 && b.IsDeleted == false).ToList();
        ViewBag.Title = "Programming books";
        return View("Index",book);
    }

    public IActionResult ViewAllNovel()
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category).Where(b => b.CategoryId == 1 && b.IsDeleted == false).ToList();
        ViewBag.Title = "Novels";
        return View("Index", book);
    }

    public IActionResult ViewAllFantasyBook()
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category).Where(b => b.CategoryId == 3 && b.IsDeleted == false).ToList();
        ViewBag.Title = "Fantasy books";
        return View("Index", book);
    }

    public IActionResult ReadPdf(int bookId)
    {
        var book = _context.Book.FirstOrDefault(b => b.BookId == bookId && b.IsDeleted == false);

        if (book == null || string.IsNullOrEmpty(book.Pdf))
        {
            return NotFound("PDF file not found.");
        }

        var pdfPath = "/pdfs/" + book.Pdf;

        return View(model: pdfPath);
    }

    public IActionResult AdminIndex()
    {
        var books = _context.Book.Where(b => b.IsDeleted == false).ToList();
        return View(books);
    }

    public IActionResult Delete(int id)
    {
        var user = _context.Book.FirstOrDefault(b => b.BookId == id);

        if (user == null)
        {
            return NotFound();
        }

        user.IsDeleted = true;
        _context.SaveChanges();

        return RedirectToAction("AdminIndex");
    }

    public IActionResult Add()
    {
        ViewBag.authors = _context.Author.Where(a => a.IsActive == true).ToList();
        ViewBag.categories = _context.Category.Where(c => c.IsActive == true).ToList();
        return View(new Book());
    }

    [HttpPost]
    public async Task<IActionResult> Add(Book model, IFormFile AvatarFile, IFormFile PdfFile)
    {
        if (ModelState.IsValid)
        {
            if (AvatarFile != null && AvatarFile.Length > 0)
            {
                var avatarPath = Path.Combine("wwwroot/images", AvatarFile.FileName);
                using (var stream = new FileStream(avatarPath, FileMode.Create))
                {
                    await AvatarFile.CopyToAsync(stream);
                }
                model.Avatar = AvatarFile.FileName;
            }

            if (PdfFile != null && PdfFile.Length > 0)
            {
                var pdfPath = Path.Combine("wwwroot/pdfs", PdfFile.FileName);
                using (var stream = new FileStream(pdfPath, FileMode.Create))
                {
                    await PdfFile.CopyToAsync(stream);
                }
                model.Pdf = PdfFile.FileName;
            }

            model.CreatedDate = DateTime.Now;
            model.IsDeleted = false;
            _context.Book.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminIndex");
        }

        ViewBag.authors = _context.Author.Where(a => a.IsActive == true).ToList();
        ViewBag.categories = _context.Category.Where(c => c.IsActive == true).ToList();
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _context.Book.Find(id);
        if (book == null) return NotFound();

        ViewBag.authors = _context.Author.Where(a => a.IsActive).ToList();
        ViewBag.categories = _context.Category.Where(c => c.IsActive).ToList();

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book model, IFormFile? AvatarFile, IFormFile? PdfFile)
    {
        if (ModelState.IsValid)
        {
            var book = _context.Book.FirstOrDefault(b => b.BookId == model.BookId);
            if (book == null) return NotFound();

            // Update fields
            book.Title = model.Title;
            book.Description = model.Description;
            book.BookCode = model.BookCode;
            book.Publisher = model.Publisher;
            book.PublishedYear = model.PublishedYear;
            book.CategoryId = model.CategoryId;
            book.AuthorId = model.AuthorId;
            book.TotalCopies = model.TotalCopies;
            book.AvailableCopies = model.AvailableCopies;

            // Image upload
            if (AvatarFile != null)
            {
                var avatarPath = Path.Combine("wwwroot/uploads", AvatarFile.FileName);
                using (var stream = new FileStream(avatarPath, FileMode.Create))
                {
                    await AvatarFile.CopyToAsync(stream);
                }
                book.Avatar = AvatarFile.FileName;
            }

            // PDF upload
            if (PdfFile != null)
            {
                var pdfPath = Path.Combine("wwwroot/uploads", PdfFile.FileName);
                using (var stream = new FileStream(pdfPath, FileMode.Create))
                {
                    await PdfFile.CopyToAsync(stream);
                }
                book.Pdf = PdfFile.FileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("AdminIndex");
        }

        ViewBag.authors = _context.Author.Where(a => a.IsActive).ToList();
        ViewBag.categories = _context.Category.Where(c => c.IsActive).ToList();
        return View(model);
    }

}
