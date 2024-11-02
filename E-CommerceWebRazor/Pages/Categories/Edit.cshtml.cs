using E_CommerceWebRazor.Data;
using E_CommerceWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_CommerceWebRazor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public Category? Category { get; set; }

        public EditModel(ApplicationDbContext _dbcontext)
        {
            dbContext = _dbcontext;
        }

        public void OnGet(int? id)
        {
            if (id != 0 && id != null)
                Category = dbContext.Categories.Find(id);
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Update(Category);
                dbContext.SaveChanges();
               TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
           
            return Page();
        }
    }
}
