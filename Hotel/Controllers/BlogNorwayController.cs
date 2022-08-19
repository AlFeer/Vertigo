using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogNorwayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
