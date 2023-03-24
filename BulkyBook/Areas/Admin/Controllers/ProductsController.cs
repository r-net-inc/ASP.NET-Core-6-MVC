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
        private readonly IWebHostEnvironment _hostEnvironment;

        //public CategoriesController(ApplicationDbContext db)
        public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll();

            return View(products);
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
                CategoriesList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                CoverTypesList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
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
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
                return View(productVM);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploadPath = Path.Combine(wwwRootPath, @"images\products");
                    var fileExtension = Path.GetExtension(file.FileName);

                    if (productVM.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploadPath, fileName + fileExtension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    productVM.Product.ImageUrl = @"\images\products\" + fileName + fileExtension;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                //_db.Categories.Update(category);
                //_unitOfWork.Product.Add(productVM.Product);
                //_db.SaveChanges();
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            return View(productVM);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productsList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productsList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            var productTypeInDb = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);

            if (productTypeInDb == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, productTypeInDb.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            //_db.Categories.Remove(categoryInDb);
            _unitOfWork.Product.Remove(productTypeInDb);
            //_db.SaveChanges();
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product deleted successfully!" });
        }
        #endregion
    }
}
