using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
