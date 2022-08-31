using Business.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hotel.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SliderHomeController : Controller
    {
        private readonly ISliderHomeService _sliderHomeService;
        private readonly IWebHostEnvironment _env;

        public SliderHomeController(ISliderHomeService sliderHomeService,
                                    IWebHostEnvironment env)
        {
            _sliderHomeService = sliderHomeService;
            _env = env;
        }

        // GET: SliderHomeController
        public async Task<IActionResult> Index()
        {
            var data = await _sliderHomeService.GetAll();
            return View(data);
        }

        // GET: SliderHomeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var data = await _sliderHomeService.Get(id);
            return View(data);
        }

        // GET: SliderHomeController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: SliderHomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderHome sliderHome)
        {
            if (sliderHome.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!sliderHome.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)sliderHome.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = sliderHome.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await sliderHome.ImageFile.CopyToAsync(stream);
            }

            sliderHome.ImageUrl = newFileName;
            sliderHome.CreatedDate = DateTime.Now;
            await _sliderHomeService.Create(sliderHome);
            return RedirectToAction("index", "sliderHome");
        }

        // GET: SliderHomeController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var data = await _sliderHomeService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SliderHome sliderHome)
        {
            if (sliderHome.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!sliderHome.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)sliderHome.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = sliderHome.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await sliderHome.ImageFile.CopyToAsync(stream);
            }


            var data = await _sliderHomeService.Get(sliderHome.Id);


            sliderHome.ImageUrl = newFileName;
            data.ImageUrl = sliderHome.ImageUrl;
            data.Title = sliderHome.Title;
            data.Subtitle = sliderHome.Subtitle;
            sliderHome.UpdatedDate = DateTime.Now;
            await _sliderHomeService.Update(data);
            return RedirectToAction("index", "sliderHome");
        }

        // GET: SliderHomeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _sliderHomeService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SliderHome sliderHome)
        {
            await _sliderHomeService.Delete(sliderHome.Id);

            return RedirectToAction("index", "sliderHome");
        }
    }
}
