using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypesController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        //public CoverTypeController(ApplicationDbContext db)
        public CoverTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _db.CoverType;
            IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();

            return View(coverTypes);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (!ModelState.IsValid)
            {
                return View(coverType);
            }

            //_db.CoverType.Add(coverType);
            _unitOfWork.CoverType.Add(coverType);
            //_db.SaveChanges();
            _unitOfWork.Save();
            TempData["success"] = "Cover Type created successfully";

            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var coverTypeFromDb = _db.Categories.Find(id);
            //var coverTypeFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var coverTypeFromDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
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

            //_db.CoverType.Update(coverType);
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

            //var categoryInDb = _db.Categories.Find(id);
            var coverTypeInDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeInDb == null)
            {
                return NotFound();
            }

            //_db.CoverType.Remove(coverTypeInDb);
            _unitOfWork.CoverType.Remove(coverTypeInDb);
            //_db.SaveChanges();
            _unitOfWork.Save();
            TempData["success"] = "Cover Type deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
