using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
