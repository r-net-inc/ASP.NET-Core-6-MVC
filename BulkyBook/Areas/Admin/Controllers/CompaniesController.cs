using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CompaniesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Company> companies = _unitOfWork.Company.GetAll();

            return View(companies);
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var company = new Company();
                return View(company);
            }
            else
            {
                var companyFromDb = _unitOfWork.Company.GetFirstOrDefault(p => p.Id == id);

                if (companyFromDb == null)
                {
                    return NotFound();
                }

                return View(companyFromDb);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    TempData["success"] = "Company created successfully!";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["success"] = "Company updated successfully!";
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _unitOfWork.Company.GetAll();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            var companyInDb = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);

            if (companyInDb == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.Company.Remove(companyInDb);
            TempData["success"] = "Company deleted successfully!";
            _unitOfWork.Save();

            return Json(new { success = true, message = "Company deleted successfully!" });
        }
        #endregion
    }
}