using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

public class BooksController : Controller
{
    private readonly LibraryContext _db;

    public BooksController(LibraryContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var books = _db.Books.Include(b => b.Author).ToList();
        return View(books);
    }

    public IActionResult Create()
    {
        ViewBag.Authors = new SelectList(_db.Authors.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (!ModelState.IsValid || book.AuthorId == 0)
        {
            ViewBag.Authors = new SelectList(_db.Authors.ToList(), "Id", "Name");
            return View(book);
        }

        _db.Books.Add(book);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var book = _db.Books.Find(id);
        if (book != null)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
