using E_CommerceWebRazor.Data;
using E_CommerceWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_CommerceWebRazor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
      
        public Category Category{ get; set; }

        public CreateModel(ApplicationDbContext _dbcontext)
        {
            dbContext = _dbcontext;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost( Category obj)
        {
            dbContext.Add(Category);
            dbContext.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
