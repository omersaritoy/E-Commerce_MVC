using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext db;
    public CategoryController(ApplicationDbContext _db)
    {
        db = _db;
    }
    public IActionResult Index()
    {
        List<Category> categoryList=db.categories.ToList();


        return View(categoryList); 
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category category)
    {
        db.categories.Add(category);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
