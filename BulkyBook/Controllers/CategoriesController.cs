using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;

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
        public IActionResult Create(Category cat)
        {
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name cannot exactly match the DisplayOrder.");
            }

            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            _db.Categories.Add(cat);
            _db.SaveChanges();
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
            var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name cannot exactly match the DisplayOrder.");
            }

            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            _db.Categories.Update(cat);
            _db.SaveChanges();
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
            var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFromDbFirst);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
