using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //Product Product = new();

            //IEnumerable<SelectListItem> CategoriesList = _unitOfWork.Category.GetAll().Select(
            //    c => new SelectListItem
            //    {
            //        Text = c.Name,
            //        Value = c.Id.ToString()
            //    });

            //IEnumerable<SelectListItem> CoverTypesList = _unitOfWork.CoverType.GetAll().Select(
            //    c => new SelectListItem
            //    {
            //        Text = c.Name,
            //        Value = c.Id.ToString()
            //    });

            var productVM = new ProductViewModel()
            {
                Product = new(),
                CategoriesList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypesList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                // Create Product
                //ViewBag.Title = "Create Product";
                //ViewBag.CategoriesList = CategoriesList;
                //ViewData["CoverTypesList"] = CoverTypesList;
                return View(productVM);
            }
            else
            {
                // Update Product
                //ViewBag.Title = "Edit Product";
                return View(productVM);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVM, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            //_db.Categories.Update(category);
            //_unitOfWork.Product.Update(productVM.Product);
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
