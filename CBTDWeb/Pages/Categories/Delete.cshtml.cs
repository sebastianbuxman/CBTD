using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Categories
{
    
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Category objCategory { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
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
                TempData["error"] = "Data Errornunable to connect to database.";
                return Page();
            }

            
            else
            {
                _unitOfWork.Category.Delete(objCategory);
                TempData["success"] = "Category successfully deleted.";
            }

            _unitOfWork.Commit();  //saves changes to database.
            return RedirectToPage("./Index");
        }
    }
}
