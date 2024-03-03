﻿using DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBTDWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public IEnumerable<Product> objProductList;
        public IEnumerable<Category> objCategoryList;

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objProductList = new List<Product>();
            objCategoryList = new List<Category>();
        }

        public IActionResult OnGet()
        {
            objProductList = _unitOfWork.Product.GetAll(null, includes: "Category,Manufacturer");
            objCategoryList = _unitOfWork.Category.GetAll(null, c => c.DisplayOrder, null);
            return Page();
        }
    }
}