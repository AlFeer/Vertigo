using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogPoolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
