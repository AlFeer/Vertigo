using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogDisneylandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
