using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication5.Data;
using WebApplication5.Data.Services;
using WebApplication5.Data.Static;
using WebApplication5.Data.ViewModel;
using WebApplication5.Models;
/*using WebApplication5.Views.Account;*/

namespace WebApplication5.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _service;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context,IAccountService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _service = service;
        }


        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login()
        {
            if(!User.Identity.IsAuthenticated){ 

                return View(new LoginVM());
            }
			return View("~/Views/Home/Index.cshtml");
		}
	

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if (!ModelState.IsValid) return View(loginVM);

			var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
				if (passwordCheck)
				{
					var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
					if (result.Succeeded)
					{

						var role = await _userManager.GetRolesAsync(user);
						if (role.Contains("ADMIN"))
						{
							return RedirectToAction("Home", "Admin", new { area = "Admin" });
						}
						else if (role.Contains("User"))
						{
							return RedirectToAction("MyProfile", "Students");
                        }
                        else
                        {
							return RedirectToAction("MyStudents", "Teachers");
						}



					}
				}
				TempData["Error"] = "Wrong credentials. Please, try again!";
				return View(loginVM);
			}


			TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }



    



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult AccessDenied(string ReturnURL)
        {
            return View();
        }
    }
}
