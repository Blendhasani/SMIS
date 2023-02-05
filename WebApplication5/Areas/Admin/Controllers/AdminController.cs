using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication5.Areas.Admin.Models;
using WebApplication5.Data;
using WebApplication5.Data.Static;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "ADMIN")]
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
        public async Task<IActionResult> Admins(int? page)
		{
			
			var pageNumber = page ?? 1;
            int pageSize = 10;
            // Get a list of users in the role
            var usersWithPermission = _userManager.GetUsersInRoleAsync("ADMIN").Result;

			// Then get a list of the ids of these users
			var idsWithPermission = usersWithPermission.Select(u => u.Id);

			// Now get the users in our database with the same ids
			var users = _context.Users.Where(u => idsWithPermission.Contains(u.Id)).ToList();

			return View(users.ToPagedList(pageNumber, pageSize));


		}
		[Route("Admin/Users")]
		public async Task<IActionResult> Users(int? page)

		{
			var pageNumber = page ?? 1;
			int pageSize = 10;
			// Get a list of users in the role
			var usersWithPermission = _userManager.GetUsersInRoleAsync("User").Result;

			// Then get a list of the ids of these users
			var idsWithPermission = usersWithPermission.Select(u => u.Id);

			// Now get the users in our database with the same ids
			var users = _context.Users.Where(u => idsWithPermission.Contains(u.Id)).ToList();

			return View(users.ToPagedList(pageNumber, pageSize));


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

			//return View("~/Report/Index.cshtml");
			return RedirectToAction(nameof(Admins));
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
			return RedirectToAction(nameof(UserRoless));
		}


		[Route("Admin/UserRoless")]

		public async Task<IActionResult> UserRoless(int? page)
		{
			var users = await _userManager.Users.ToListAsync();
			var userRoles = new List<UsersRoles>();
            var pageNumber = page ?? 1;
            int pageSize = 10;
            foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				userRoles.Add(new UsersRoles()
				{
					User = user,
					Roles = roles
				});
			}

			return View(userRoles.ToPagedList(pageNumber, pageSize));
		}
		[Route("Admin/DeleteUserRole")]
		[Route("Admin/DeleteUserRole/{id?}")]
		[HttpPost]
		public async Task<IActionResult> DeleteUserRole(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				foreach (var role in roles)
				{
					await _userManager.RemoveFromRoleAsync(user, role);
				}
				await _userManager.DeleteAsync(user);
			}
            return RedirectToAction(nameof(UserRoless));
        }

		[Route("Admin/Notat")]
		public async Task<IActionResult> Notat()
		{
			var notat = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t=>t.Nota==5).ToList();
			return View(notat);
		}

		[Route("Admin/DeleteNoten/{id?}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transkripta = await _context.Transkripta
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            var afatitranskripta = await _context.AfatiTranskripta.FirstOrDefaultAsync(m => m.TranskriptaId == id);
            if (transkripta == null)
            {
                return NotFound();
            }
            else
            {
                _context.AfatiTranskripta.Remove(afatitranskripta);
                await _context.SaveChangesAsync();
                _context.Transkripta.Remove(transkripta);




            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Notat));
        }
    }

}
