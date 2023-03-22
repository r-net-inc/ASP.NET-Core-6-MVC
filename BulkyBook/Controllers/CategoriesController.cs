using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyBook.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly ICategoryRepository _db;

        //public CategoriesController(ApplicationDbContext db)
        public CategoriesController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _db.Categories;
            IEnumerable<Category> categories = _db.GetAll();

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
                ModelState.AddModelError("name", "The Name cannot exactly match the DisplayOrder.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //_db.Categories.Add(category);
            _db.Add(category);
            //_db.SaveChanges();
            _db.Save();
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
            var categoryFromDbFirst = _db.GetFirstOrDefault(c => c.Id == id);

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
                ModelState.AddModelError("name", "The Name cannot exactly match the DisplayOrder.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //_db.Categories.Update(category);
            _db.Update(category);
            //_db.SaveChanges();
            _db.Save();
            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryInDb = _db.Categories.Find(id);
            var categoryInDb = _db.GetFirstOrDefault(u => u.Id == id);

            if (categoryInDb == null)
            {
                return NotFound();
            }

            //_db.Categories.Remove(categoryInDb);
            _db.Remove(categoryInDb);
            //_db.SaveChanges();
            _db.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
