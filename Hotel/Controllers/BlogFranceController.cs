using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogFranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
