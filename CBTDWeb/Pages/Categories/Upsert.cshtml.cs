using DataAccess;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Categories
{
    
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Category objCategory { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objCategory = new Category();
        }
        public IActionResult OnGet(int? id)
        {
            //assuming am i in edit mode:
            if (id != 0)
            {
                objCategory = _unitOfWork.Category.GetById(id);
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
                _unitOfWork.Category.Add(objCategory);    //not saved to database yet.
                TempData["success"] = "Category added successfully.";
            }

            //if exists
            else
            {
                _unitOfWork.Category.Update(objCategory);
                TempData["success"] = "Category updated successfully.";
            }

            _unitOfWork.Commit();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
