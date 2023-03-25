using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();

            return View(categories);
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var category = new Category();
                return View(category);
            }
            else
            {
                var category = _unitOfWork.Category.GetFirstOrDefault(p => p.Id == id);
                return View(category);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                    TempData["success"] = "Category created successfully!";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    TempData["success"] = "Category updated successfully!";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _unitOfWork.Category.GetAll();
            return Json(new { data = categories });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            var categoryInDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryInDb == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.Category.Remove(categoryInDb);
            TempData["success"] = "Category deleted successfully!";
            _unitOfWork.Save();

            return Json(new { success = true, message = "Category deleted successfully!" });
        }
        #endregion
    }
}