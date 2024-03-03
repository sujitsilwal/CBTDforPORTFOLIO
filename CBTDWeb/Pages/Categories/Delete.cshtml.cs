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
            if (id != 0)
            {
                objCategory = _unitOfWork.Category.GetById(id);
            }

            if (objCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Error unable to connect to unitOfWork";
                return Page();
            }
            else
            {
                _unitOfWork.Category.Delete(objCategory);
                TempData["success"] = "Category successfully deleted";
            }

            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}