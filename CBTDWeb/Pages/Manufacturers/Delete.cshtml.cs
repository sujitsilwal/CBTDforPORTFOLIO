using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Manufacturer objManufacturer { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objManufacturer = new Manufacturer();
        }

        public IActionResult OnGet(int? id)
        {
            if (id != 0)
            {
                objManufacturer = _unitOfWork.Manufacturer.GetById(id);
            }

            if (objManufacturer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Error unable to connect to DB";
                return Page();
            }
            else
            {
                _unitOfWork.Manufacturer.Delete(objManufacturer);
                TempData["success"] = "Manufacturer successfully deleted";
            }

            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}