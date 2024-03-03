using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTDWeb.Pages.Products
{
    public class UpsertModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Product objProduct { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            objProduct = new Product();
            CategoryList = new List<SelectListItem>();
            ManufacturerList = new List<SelectListItem>();
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            //populate our SelectListItems
            CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ManufacturerList = _unitOfWork.Manufacturer.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            //Are we in create mode
            if (id == null || id == 0)
            {
                return Page();
            }
            // edit mode
            if (id != 0)
            {
                objProduct = _unitOfWork.Product.GetById(id);
            }

            if (objProduct == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            //determine root path of wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
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

                    //create variable to hold a path to the images/products folder
                    var uploads = Path.Combine(webRootPath, @"images\products\");

                    //get and preserve the extension type
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
            // the item exists already, so we're updating it
            else
            {
                //get the product again from the DB because binding is on, and we need to process the
                // physical image separately from the binded property holding the URL string

                objProduct = _unitOfWork.Product.Get(p => p.Id == objProduct.Id);

                //was there an image uploaded?
                if (files.Count > 0)
                {
                    //create a unique identifier for image name
                    string fileName = Guid.NewGuid().ToString();

                    //create variable to hold a path to the images/products folder
                    var uploads = Path.Combine(webRootPath, @"images\products\");

                    //get and preserve the extension type
                    var extension = Path.GetExtension(files[0].FileName);

                    if (objProduct.ImageURL != null)
                    {
                        var imagePath = Path.Combine(webRootPath, objProduct.ImageURL.TrimStart('\\'));

                        //if the image exists physically
                        if (System.IO.File.Exists(imagePath))
                        {
                            //delete it
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
                else // we're trying to add image for the 1st time
                {
                    objProduct.ImageURL = objProduct.ImageURL;
                }
                //update the existing product
                _unitOfWork.Product.Update(objProduct);
            }
            //Save Changes to Database
            _unitOfWork.Commit();

            //redirect to the products page
            return RedirectToPage("./Index");
        }
    }
}