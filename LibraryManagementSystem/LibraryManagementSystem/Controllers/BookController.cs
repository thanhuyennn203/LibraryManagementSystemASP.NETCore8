using LibraryManagementSystem.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller
{
    private readonly LibraryManagementSystemContext _context;

    public BookController(LibraryManagementSystemContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Book.Include(b => b.Author).ToList();
        ViewBag.Title = "All book available";
        return View(books);
    }
    public IActionResult Detail(int bookId)
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefault(b => b.BookId == bookId);

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
            .Include(b => b.Category).Where(b=> b.CategoryId == 2).ToList();
        ViewBag.Title = "Programming books";
        return View("Index",book);
    }

    public IActionResult ViewAllNovel()
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category).Where(b => b.CategoryId == 1).ToList();
        ViewBag.Title = "Novels";
        return View("Index", book);
    }

    public IActionResult ViewAllFantasyBook()
    {
        var book = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Category).Where(b => b.CategoryId == 3).ToList();
        ViewBag.Title = "Fantasy books";
        return View("Index", book);
    }

    public IActionResult ReadPdf(int bookId)
    {
        var book = _context.Book.FirstOrDefault(b => b.BookId == bookId);

        if (book == null || string.IsNullOrEmpty(book.Pdf))
        {
            return NotFound("PDF file not found.");
        }

        var pdfPath = "/pdfs/" + book.Pdf;

        return View(model: pdfPath);
    }
}
