using Business.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class RoomController : Controller
    {
        public IRoomService _roomService { get; set; }
        private readonly IWebHostEnvironment _env;

        public RoomController(IRoomService roomService, IWebHostEnvironment env)
        {
            _roomService = roomService;
            _env = env;
        }

        // GET: SliderHomeController
        public async Task<IActionResult> Index()
        {
            var data = await _roomService.GetAll();
            return View(data);
        }

        // GET: SliderHomeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var data = await _roomService.Get(id);
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
        public async Task<IActionResult> Create(Room room)
        {
            if (room.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!room.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)room.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = room.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await room.ImageFile.CopyToAsync(stream);
            }

            room.ImageUrl = newFileName;
            room.CreatedDate = DateTime.Now;
            await _roomService.Create(room);
            return RedirectToAction("index", "room");
        }

        // GET: SliderHomeController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var data = await _roomService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Room room)
        {
            if (room.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!room.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)room.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = room.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await room.ImageFile.CopyToAsync(stream);
            }


            var data = await _roomService.Get(room.Id);


            room.ImageUrl = newFileName;
            data.ImageUrl = room.ImageUrl;
            data.Title = room.Title;
            data.Location= room.Location;
            data.Description = room.Description;
            room.UpdatedDate = DateTime.Now;
            await _roomService.Update(data);
            return RedirectToAction("index", "room");
        }

        // GET: SliderHomeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _roomService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Room room)
        {
            await _roomService.Delete(room.Id);

            return RedirectToAction("index", "room");
        }
    }
}
