using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Categories
{
    
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category objCategory { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
            objCategory = new Category();
        }
        public IActionResult OnGet(int? id)
        {
            //assuming am i in edit mode:
            if (id != 0)
            {
                objCategory = _db.Categories.Find(id);
            }
            if (objCategory == null) //nothing found in database
            {
                return NotFound();  //built in page.
            }

            //assuming im in create mode:
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                TempData["error"] = "Data Errornunable to connect to database.";
                return Page();
            }

            
            else
            {
                _db.Categories.Remove(objCategory);
                TempData["success"] = "Category successfully deleted.";
            }

            _db.SaveChanges();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
