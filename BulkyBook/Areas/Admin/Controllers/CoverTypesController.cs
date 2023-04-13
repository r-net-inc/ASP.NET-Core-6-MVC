using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CoverTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();

            return View(coverTypes);
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var coverType = new CoverType();
                return View(coverType);
            }
            else
            {
                var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

                if (coverTypeFromDb == null)
                {
                    return NotFound();
                }

                return View(coverTypeFromDb);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType.Id == 0)
                {
                    _unitOfWork.CoverType.Add(coverType);
                    TempData["success"] = "Cover Type created successfully!";
                }
                else
                {
                    _unitOfWork.CoverType.Update(coverType);
                    TempData["success"] = "Cover Type updated successfully!";
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(coverType);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var coverTypes = _unitOfWork.CoverType.GetAll();
            return Json(new { data = coverTypes });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Invalid Id!" });
            }

            var coverTypeInDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverTypeInDb == null)
            {
                return Json(new { success = false, message = "Cover Type not found!" });
            }

            _unitOfWork.CoverType.Remove(coverTypeInDb);
            TempData["success"] = "Cover Type deleted successfully!";
            _unitOfWork.Save();

            return Json(new { success = true, message = "Cover Type deleted successfully!" });
        }
        #endregion
    }
}
