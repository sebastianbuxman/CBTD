using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class IndexModel : PageModel
    {

        private readonly UnitOfWork _unitOfWork;

        public List<Manufacturer> objManufacturerList;
        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objManufacturerList = new List<Manufacturer>();
        }
        public IActionResult OnGet()
        {
            objManufacturerList = _unitOfWork.Manufacturer.GetAll().ToList();
            return Page();
        }
    }
}
