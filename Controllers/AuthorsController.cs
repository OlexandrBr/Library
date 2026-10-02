using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class AuthorsController : Controller
{
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db)
    {
        _db = db;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid)
        {
            return View(author);
        }

        _db.Authors.Add(author);
        _db.SaveChanges();
        return RedirectToAction("Index", "Books");
    }
}
