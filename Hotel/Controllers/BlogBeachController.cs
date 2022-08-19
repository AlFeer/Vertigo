using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogBeachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
