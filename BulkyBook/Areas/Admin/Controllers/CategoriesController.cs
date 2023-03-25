﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        //public CategoriesController(ApplicationDbContext db)
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _db.Categories;
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();

            return View(categories);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name cannot exactly match the Display Order.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //_db.Categories.Add(category);
            _unitOfWork.Category.Add(category);
            //_db.SaveChanges();
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name cannot exactly match the Display Order.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //_db.Categories.Update(category);
            _unitOfWork.Category.Update(category);
            //_db.SaveChanges();
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    //var categoryInDb = _db.Categories.Find(id);
        //    var categoryInDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

        //    if (categoryInDb == null)
        //    {
        //        return NotFound();
        //    }

        //    //_db.Categories.Remove(categoryInDb);
        //    _unitOfWork.Category.Remove(categoryInDb);
        //    //_db.SaveChanges();
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category deleted successfully";

        //    return RedirectToAction("Index");
        //}

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

            //_db.Categories.Remove(categoryInDb);
            _unitOfWork.Category.Remove(categoryInDb);
            TempData["success"] = "Category deleted successfully!";
            //_db.SaveChanges();
            _unitOfWork.Save();

            return Json(new { success = true, message = "Category deleted successfully!" });
        }
        #endregion
    }
}
