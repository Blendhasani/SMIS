using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication5.Data;
using WebApplication5.Data.Static;
using WebApplication5.Models;

namespace WebApplication5.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;


		public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
    
		}
		[Route("Admin/Home")]
		public IActionResult Home()
		{
			return View();
		}
        [Route("Admin/Admins")]
        public async Task<IActionResult> Admins()
		{
			// Get a list of users in the role
			var usersWithPermission = _userManager.GetUsersInRoleAsync("ADMIN").Result;

			// Then get a list of the ids of these users
			var idsWithPermission = usersWithPermission.Select(u => u.Id);

			// Now get the users in our database with the same ids
			var users = _context.Users.Where(u => idsWithPermission.Contains(u.Id)).ToList();

			return View(users);


		}
		[Route("Admin/Users")]
		public async Task<IActionResult> Users()
		{
			// Get a list of users in the role
			var usersWithPermission = _userManager.GetUsersInRoleAsync("User").Result;

			// Then get a list of the ids of these users
			var idsWithPermission = usersWithPermission.Select(u => u.Id);

			// Now get the users in our database with the same ids
			var users = _context.Users.Where(u => idsWithPermission.Contains(u.Id)).ToList();

			return View(users);


		}
		[Route("Admin/CreateAdmin")]
		public IActionResult CreateAdmin()
		{
			return View();
		}
		[Route("Admin/CreateAdmin")]
		[Route("Admin/CreateAdmin/{id?}")]
		[HttpPost]
		public async Task<IActionResult> CreateAdmin(AdminUser auser)
		{


			var newUser = new ApplicationUser()
			{
				FullName = auser.Name,
				Email = auser.Email,
				UserName = auser.Email
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, auser.Password);

			if (newUserResponse.Succeeded)
				await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
			await _context.SaveChangesAsync();

			return View("~/Report/Index.cshtml");

			/*   return newUser;*/
		}


    

        [Route("Admin/DeleteUser")]
        [Route("Admin/DeleteUser/{id?}")]
        [HttpPost]

        public async Task<IActionResult> DeleteUser (string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if(user == null)
			{
				ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
				return View("NotFound");
			}
			else
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Admins");
				}
				foreach(var error in result.Errors)
				{
					ModelState.AddModelError("",error.Description);
				}
			}
			return View("Admins");
		}



    }
}
