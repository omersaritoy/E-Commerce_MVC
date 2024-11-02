using E_CommerceWebRazor.Data;
using E_CommerceWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_CommerceWebRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public Category? Category { get; set; }
        public DeleteModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
                Category = dbContext.Categories.Find(id);
        }
        public IActionResult OnPost()
        {
            Category? category = dbContext.Categories.Find(Category.Id);
            if (category == null)
                return NotFound();
            dbContext.Categories.Remove(category);
            TempData["success"] = "Category deleted successfully";
            dbContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
