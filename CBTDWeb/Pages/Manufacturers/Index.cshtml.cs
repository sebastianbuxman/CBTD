using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public List<Manufacturer> objManufacturerList;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
            objManufacturerList = new List<Manufacturer>();
        }
        public IActionResult OnGet()
        {
            objManufacturerList = _db.Manufacturers.ToList();
            return Page();
        }
    }
}
