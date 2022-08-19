using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogNYCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
