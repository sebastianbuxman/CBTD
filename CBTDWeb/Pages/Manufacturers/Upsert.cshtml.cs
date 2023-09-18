using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Manufacturer objManufacturer { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
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
                TempData["error"] = "Data incomplete.";
                return Page();
            }

            //if this is a new category
            if (objManufacturer.Id == 0)
            {
                _unitOfWork.Manufacturer.Add(objManufacturer);    //not saved to database yet.
                TempData["success"] = "Category added successfully.";
            }

            //if exists
            else
            {
                _unitOfWork.Manufacturer.Update(objManufacturer);
                TempData["success"] = "Category updated successfully.";
            }

            _unitOfWork.Commit();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
