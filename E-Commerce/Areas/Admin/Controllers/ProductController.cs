using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Product> productList = _unitOfWork.Product.GetAll().ToList();

        return View(productList);
    }
    public IActionResult Create()
    {
        IEnumerable<SelectListItem> CategorList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString(),
        });
        ViewBag.CategorList = CategorList;
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        Product Product = _unitOfWork.Product.Get(u => u.Id == id);
        if (Product == null)
            return NotFound();

        return View(Product);
    }
    [HttpPost]
    public IActionResult Edit(Product Product)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        Product? product = _unitOfWork.Product.Get(u => u.Id == id);
        if (product == null)
            return NotFound();
        return View(product);
    }
    [HttpPost, ActionName("Delete")]

    public IActionResult DeletePost(int? id)
    {
        Product? Product = _unitOfWork.Product.Get(u => u.Id == id);
        if (Product == null)
            return NotFound();
        _unitOfWork.Product.Remove(Product);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
    }
}
