using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogBrazilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
