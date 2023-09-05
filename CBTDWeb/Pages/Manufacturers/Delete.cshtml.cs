using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Manufacturer objManufacturer { get; set; }
        public DeleteModel(ApplicationDbContext db)
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
                TempData["error"] = "Data Errornunable to connect to database.";
                return Page();
            }


            else
            {
                _db.Manufacturers.Remove(objManufacturer);
                TempData["success"] = "Category successfully deleted.";
            }

            _db.SaveChanges();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
