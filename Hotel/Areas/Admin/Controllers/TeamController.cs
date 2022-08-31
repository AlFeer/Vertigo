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
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IWebHostEnvironment _env;

        public TeamController(ITeamService teamService,
                                    IWebHostEnvironment env)
        {
            _teamService = teamService;
            _env = env;
        }

        // GET: SliderHomeController
        public async Task<IActionResult> Index()
        {
            var data = await _teamService.GetAll();
            return View(data);
        }

        // GET: SliderHomeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var data = await _teamService.Get(id);
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
        public async Task<IActionResult> Create(Team team)
        {
            if (team.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!team.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)team.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = team.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await team.ImageFile.CopyToAsync(stream);
            }

            team.ImageUrl = newFileName;
            team.CreatedDate = DateTime.Now;
            await _teamService.Create(team);
            return RedirectToAction("index", "team");
        }

        // GET: SliderHomeController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            var data = await _teamService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Team team)
        {
            if (team.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image cannot be null");
                return View();
            }

            if (!team.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be only image");
                return View();
            }

            decimal size = (decimal)team.ImageFile.Length / 1024 / 1024;

            if (size > 3)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 3mb");
                return View();
            }

            var fileName = team.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            var newFileName = Guid.NewGuid().ToString() + fileName;
            var path = Path.Combine(_env.WebRootPath, "assets", "uploads", "images", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await team.ImageFile.CopyToAsync(stream);
            }


            var data = await _teamService.Get(team.Id);


            team.ImageUrl = newFileName;
            data.ImageUrl = team.ImageUrl;
            data.Fullname = team.Fullname;
            data.Job = team.Job;
            team.UpdatedDate = DateTime.Now;
            await _teamService.Update(data);
            return RedirectToAction("index", "team");
        }

        // GET: SliderHomeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _teamService.Get(id);
            return View(data);
        }

        // POST: SliderHomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Team team)
        {
            await _teamService.Delete(team.Id);

            return RedirectToAction("index", "team");
        }
    }
}
