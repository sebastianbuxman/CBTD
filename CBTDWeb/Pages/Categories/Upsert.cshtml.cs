using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Categories
{
    
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category objCategory { get; set; }
        public UpsertModel(ApplicationDbContext db)
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
                TempData["error"] = "Data incomplete.";
                return Page();
            }

            //if this is a new category
            if (objCategory.Id == 0)
            {
                _db.Categories.Add(objCategory);    //not saved to database yet.
                TempData["success"] = "Category added successfully.";
            }

            //if exists
            else
            {
                _db.Categories.Update(objCategory);
                TempData["success"] = "Category updated successfully.";
            }

            _db.SaveChanges();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
