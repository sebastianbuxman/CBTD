using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTDWeb.Pages.Products
{
    public class UpsertModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Product objProduct { get;set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            objProduct = new Product();
            CategoryList = new List<SelectListItem>();
            ManufacturerList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //populate our SelectListItems
            CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            ManufacturerList = _unitOfWork.Manufacturer.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

            //Are we in create mode
            if (id==null || id == 0)
            {
                return Page();
            }
            
                if (id != 0)
                {
                    objProduct = _unitOfWork.Product.GetById(id);
                }

                if(objProduct == null) //maybe nothing was returned from database.
                {
                    return NotFound();
                }
                return Page();
        }
        public IActionResult OnPost(int? id)
        {
            //determine root path of wwwroot
            string webRootPath = _webHostEnvironment.WebRootPath;
            //retrieve the files
            var files = HttpContext.Request.Form.Files;
            //if the product is new (create)
            if (objProduct.Id == 0)
            {
                //was there an image uploaded?
                if (files.Count > 0)
                {
                    //create a unique identifier for image name
                    string fileName = Guid.NewGuid().ToString();
                    //create variable to hold a path to images/product folder
                    var uploads = Path.Combine(webRootPath, @"images\products\");
                    //get and preserve the extesnion type
                    var extension = Path.GetExtension(files[0].FileName);
                    //create the full path of the item to stream
                    var fullPath = uploads + fileName + extension;
                    //stream the binary files to server
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    //associate the actual real URL path and save to DB
                    objProduct.ImageURL = @"\images\products\" + fileName + extension;
                }
                //add this new Product internally
                _unitOfWork.Product.Add(objProduct);
            }
            //item exists already
            else
            {
                //get product again from the database because binding is on and need to process the physical image seperately
                //from the binded property holding the url string.
                //var objProductFromDb = _unitOfWork.Product.Get(p => p.Id == objProduct.Id);
                objProduct = _unitOfWork.Product.Get(p => p.Id == objProduct.Id);
                //was there an image uploaded
                if (files.Count > 0)
                {
                    //create a unique identifier for image name
                    string fileName = Guid.NewGuid().ToString();
                    //create variable to hold a path to images/product folder
                    var uploads = Path.Combine(webRootPath, @"images\products\");
                    //get and preserve the extesnion type
                    var extension = Path.GetExtension(files[0].FileName);
                    
                    if(objProduct.ImageURL != null)
                    {
                        var imagePath = Path.Combine(webRootPath,objProduct.ImageURL.TrimStart('\\'));
                        //if image exists physically 
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    //create the full path of the item to stream
                    var fullPath = uploads + fileName + extension;
                    //stream the binary files to server
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    //associate the actual real URL path and save to DB
                    objProduct.ImageURL = @"\images\products\" + fileName + extension;
                }
                else //trying add file for 1st time
                {
                    objProduct.ImageURL = objProduct.ImageURL;
                }
                //update existing product
                _unitOfWork.Product.Update(objProduct);
            }
            _unitOfWork.Commit();

            //redirect to products page
            return RedirectToPage("./Index");
        }
    }
}
