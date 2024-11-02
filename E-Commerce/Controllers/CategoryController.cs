using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
        List<Category> categoryList = db.categories.ToList();


        return View(categoryList);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            db.categories.Add(category);
            db.SaveChanges();
            TempData["success"] = "Category created successfully"; 
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        Category category = db.categories.Find(id);
        if (category == null)
            return NotFound();

        return View(category);
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {

        if (ModelState.IsValid)
        {
            db.categories.Update(category);
            db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        Category category = db.categories.Find(id);
        if (category == null)
            return NotFound();

        return View(category);
    }
    [HttpPost, ActionName("Delete")]

    public IActionResult DeletePost(int? id)
    {
        Category? category = db.categories.Find(id);
        if (category == null)
            return NotFound();
        db.categories.Remove(category);
        db.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
