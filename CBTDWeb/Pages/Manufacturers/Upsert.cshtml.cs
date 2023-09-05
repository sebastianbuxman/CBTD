using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Manufacturer objManufacturer { get; set; }
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
            objManufacturer = new Manufacturer();
        }
        public IActionResult OnGet(int? id)
        {
            //assuming am i in edit mode:
            if (id != 0)
            {
                objManufacturer = _db.Manufacturers.Find(id);
            }
            if (objManufacturer == null) //nothing found in database
            {
                return NotFound();  //built in page.
            }

            //assuming im in create mode:
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data incomplete.";
                return Page();
            }

            //if this is a new category
            if (objManufacturer.Id == 0)
            {
                _db.Manufacturers.Add(objManufacturer);    //not saved to database yet.
                TempData["success"] = "Category added successfully.";
            }

            //if exists
            else
            {
                _db.Manufacturers.Update(objManufacturer);
                TempData["success"] = "Category updated successfully.";
            }

            _db.SaveChanges();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
