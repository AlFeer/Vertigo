using Business.Services;
using Hotel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderHomeService _sliderHomeService;
        private readonly IPromotionService _promotionService;
        private readonly ISomeHotelService _someHotelService;
        private readonly ISomeBlogService _someBlogService;


        public HomeController(ISliderHomeService sliderHomeService, 
                              IPromotionService promotionService,
                              ISomeBlogService someBlogService,
                              ISomeHotelService someHotelService)
        {
            _sliderHomeService = sliderHomeService;
            _promotionService = promotionService;
            _someHotelService = someHotelService;
            _someBlogService = someBlogService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.SlidersHome = await _sliderHomeService.GetAll();
            homeVM.Promotions = await _promotionService.GetAll();
            homeVM.SomeHotels = await _someHotelService.GetAll();
            homeVM.SomeBlogs = await _someBlogService.GetAll();

            return View(homeVM);
        }
    }
}
