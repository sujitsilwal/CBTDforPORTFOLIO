using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Products
{
    public class Index_OriginalModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        public IEnumerable<Product> objProductList { get; set; }

        public Index_OriginalModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objProductList = new List<Product>();
        }

        public IActionResult OnGet()

        {
            objProductList = _unitOfWork.Product.GetAll();

            return Page();
        }
    }
}