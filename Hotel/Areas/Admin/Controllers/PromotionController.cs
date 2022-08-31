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
    public class PromotionController : Controller
    {
        private readonly IPromotionService _promotionService;
        private readonly IWebHostEnvironment _env;

        public PromotionController(IPromotionService promotionService,
                                    IWebHostEnvironment env)
        {
            _promotionService = promotionService;
            _env = env;
        }

        // GET: SliderHomeController
        public async Task<IActionResult> Index()
        {
            var data = await _promotionService.GetAll();
            return View(data);
        }

        // GET: SliderHomeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var data = await _promotionService.Get(id);
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
        public async Task<IActionResult> Create(Promotion promotion)
        {
            if (promotion.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!promotion.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)promotion.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = promotion.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await promotion.ImageFile.CopyToAsync(stream);
            }

            promotion.ImageUrl = newFileName;
            promotion.SecImageUrl = newFileName;
            promotion.CreatedDate = DateTime.Now;
            await _promotionService.Create(promotion);
            return RedirectToAction("index", "promotion");
        }

        // GET: SliderHomeController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var data = await _promotionService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Promotion promotion)
        {
            if (promotion.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!promotion.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)promotion.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = promotion.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await promotion.ImageFile.CopyToAsync(stream);
            }


            var data = await _promotionService.Get(promotion.Id);


            promotion.ImageUrl = newFileName;
            promotion.SecImageUrl = newFileName;
            data.ImageUrl = promotion.ImageUrl;
            data.SecImageUrl = promotion.SecImageUrl;
            promotion.UpdatedDate = DateTime.Now;
            await _promotionService.Update(data);
            return RedirectToAction("index", "promotion");
        }

        // GET: SliderHomeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _promotionService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Promotion promotion)
        {
            await _promotionService.Delete(promotion.Id);

            return RedirectToAction("index", "promotion");
        }
    }
}
