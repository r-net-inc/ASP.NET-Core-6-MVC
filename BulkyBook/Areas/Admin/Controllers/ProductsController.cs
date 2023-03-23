using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        //public CategoriesController(ApplicationDbContext db)
        public ProductsController(IUnitOfWork unitOfWork)
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
            Product product = new();
            if (id == null || id == 0)
            {
                // Create Product
                return View(product);
            }
            else
            {
                // Update Product
                return View(product);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CoverType coverType)
        {
            if (!ModelState.IsValid)
            {
                return View(coverType);
            }

            //_db.Categories.Update(category);
            _unitOfWork.CoverType.Update(coverType);
            //_db.SaveChanges();
            _unitOfWork.Save();
            TempData["success"] = "Cover Type updated successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeInDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeInDb == null)
            {
                return NotFound();
            }

            //_db.Categories.Remove(categoryInDb);
            _unitOfWork.CoverType.Remove(coverTypeInDb);
            //_db.SaveChanges();
            _unitOfWork.Save();
            TempData["success"] = "Cover Type deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
