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
    public class SomeHotelController : Controller
    {
        private readonly ISomeHotelService _someHotelService;
        private readonly IWebHostEnvironment _env;

        public SomeHotelController(ISomeHotelService someHotelService,
                                    IWebHostEnvironment env)
        {
            _someHotelService = someHotelService;
            _env = env;
        }

        // GET: SliderHomeController
        public async Task<IActionResult> Index()
        {
            var data = await _someHotelService.GetAll();
            return View(data);
        }

        // GET: SliderHomeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var data = await _someHotelService.Get(id);
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
        public async Task<IActionResult> Create(SomeHotel someHotel)
        {
            if (someHotel.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!someHotel.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)someHotel.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = someHotel.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await someHotel.ImageFile.CopyToAsync(stream);
            }

            someHotel.ImageUrl = newFileName;
            someHotel.CreatedDate = DateTime.Now;
            await _someHotelService.Create(someHotel);
            return RedirectToAction("index", "someHotel");
        }

        // GET: SliderHomeController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var data = await _someHotelService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SomeHotel someHotel)
        {
            if (someHotel.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!someHotel.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)someHotel.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = someHotel.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await someHotel.ImageFile.CopyToAsync(stream);
            }


            var data = await _someHotelService.Get(someHotel.Id);


            someHotel.ImageUrl = newFileName;
            data.ImageUrl = someHotel.ImageUrl;
            data.Name = someHotel.Name;
            data.Place = someHotel.Place;
            data.Stars = someHotel.Stars;
            data.Price = someHotel.Price;
            someHotel.UpdatedDate = DateTime.Now;
            await _someHotelService.Update(data);
            return RedirectToAction("index", "someHotel");
        }

        // GET: SliderHomeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _someHotelService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SomeHotel someHotel)
        {
            await _someHotelService.Delete(someHotel.Id);

            return RedirectToAction("index", "someHotel");
        }
    }
}
