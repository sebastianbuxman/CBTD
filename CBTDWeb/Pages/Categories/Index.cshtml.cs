using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Categories
{
    public class IndexModel : PageModel
    {
        //local instance of database service
        private readonly ApplicationDbContext _db;

        //UI front end to support looping through several categories.
        public List<Category> objCategoryList;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
            objCategoryList = new List<Category>();
        }

        public IActionResult OnGet()
            //IActionResult returns 
            //1. Server Status COde
            //2. #1 AND Object Results
            //3. Redirection to another Razor page
            //4. File Results
            //5. Return Content Results - a razor page for eg.
        {
            objCategoryList = _db.Categories.ToList();
            return Page();
        }
    }
}
