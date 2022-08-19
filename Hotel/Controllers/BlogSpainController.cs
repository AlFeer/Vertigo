using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogSpainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
