using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class IndexModel : PageModel
    {
        //local instance of the database
        private readonly UnitOfWork _unitOfWork;

        //front end to support looping through several categories
        public IEnumerable<Manufacturer> objManufacturerList;

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objManufacturerList = new List<Manufacturer>();
        }

        public IActionResult OnGet()
        //IActionResults returns
        //1. Server Status Code Results
        //2. #1 and Object Results
        //3. Redirection to another web page
        //4. File Results
        //5. Return Content Results - a Razor Page
        {
            objManufacturerList = _unitOfWork.Manufacturer.GetAll();
            return Page();
        }
    }
}