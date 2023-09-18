using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Manufacturer objManufacturer { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objManufacturer = new Manufacturer();
        }
        public IActionResult OnGet(int? id)
        {
            //assuming am i in edit mode:
            if (id != 0)
            {
                objManufacturer = _unitOfWork.Manufacturer.GetById(id);
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
                _unitOfWork.Manufacturer.Delete(objManufacturer);
                TempData["success"] = "Category successfully deleted.";
            }

            _unitOfWork.Commit();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
