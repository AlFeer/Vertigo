using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogUKController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
