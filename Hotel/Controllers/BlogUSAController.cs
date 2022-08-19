using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BlogUSAController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
