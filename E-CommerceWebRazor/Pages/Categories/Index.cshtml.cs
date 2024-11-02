using E_CommerceWebRazor.Data;
using E_CommerceWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_CommerceWebRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext _dbcontext)
        {
            dbContext = _dbcontext;
        }

        public void OnGet()
        {
            CategoryList=dbContext.Categories.ToList();
        }
    }
}
