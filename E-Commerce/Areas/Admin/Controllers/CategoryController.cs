﻿using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();

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
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        Category category = _unitOfWork.Category.Get(u => u.Id == id);
        if (category == null)
            return NotFound();

        return View(category);
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();
        Category category = _unitOfWork.Category.Get(u => u.Id == id);
        if (category == null)
            return NotFound();
        return View(category);
    }
    [HttpPost, ActionName("Delete")]

    public IActionResult DeletePost(int? id)
    {
        Category? category = _unitOfWork.Category.Get(u => u.Id == id);
        if (category == null)
            return NotFound();
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}