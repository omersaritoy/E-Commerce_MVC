using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
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
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Product = new Product()
        };
        if (id == null || id == 0)
        {
            //ekleme
            return View(productVM);
        }
        else
        {
            //güncelleme
            productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
            return View(productVM); 
        }
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM,IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(productVM.Product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        else
        {

            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVM);
        }

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
