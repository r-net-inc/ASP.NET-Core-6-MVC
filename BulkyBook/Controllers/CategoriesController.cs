using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
