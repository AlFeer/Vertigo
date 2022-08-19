using Business.Services;
using DAL.Models;
using Hotel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Hotel.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHostelService _hotelsService;

        public HotelsController(IHostelService hotelsService)
        {
            _hotelsService = hotelsService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            
            List<Hotels> hotel = (await _hotelsService.GetAll());
            return View(hotel.ToPagedList(page, 8));

        }


    }
}
