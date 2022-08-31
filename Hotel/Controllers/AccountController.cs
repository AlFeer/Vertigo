using DAL.Models;
using Hotel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Hotel.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(SignInManager<AppUser> signInManager,
								 UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM register)
		{
			if (!ModelState.IsValid)
			{
				return View(register);
			}
			AppUser newUser = new AppUser()
			{
				Fullname = register.Fullname,
				Email = register.Email,
				UserName = register.Username
			};

			IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}

			await _signInManager.SignInAsync(newUser, false);
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM login)
		{

			if (!ModelState.IsValid)
			{
				return View(login);
			}
			AppUser user = await _userManager.FindByEmailAsync(login.Email);
			SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);

			return RedirectToAction("Index", "Home");
		}
	}
}
