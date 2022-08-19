using Business.Services;
using Hotel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotel.Controllers
{
    public class AboutController : Controller
    {
        private readonly ISliderAboutService _sliderAboutService;
        private readonly ITeamService _teamService;

        public AboutController(ISliderAboutService sliderAboutService, 
                               ITeamService teamService)
        {
            _sliderAboutService = sliderAboutService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            AboutVM aboutVM = new AboutVM();
            aboutVM.SliderAbouts = await _sliderAboutService.GetAll();
            aboutVM.Teams = await _teamService.GetAll();
            return View(aboutVM);
        }
    }
}
