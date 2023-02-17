using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebApplication5.Data;
using WebApplication5.Data.Services;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /*     private readonly IAccountService _register;*/
        public IAccountService _register;
        private readonly UserManager<ApplicationUser> _userManager;



        public StudentsController(ApplicationDbContext context, IAccountService register, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _register = register;
            _userManager = userManager;
        }


		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Fakultetet()
		{
			return View(await _context.Fakultetet.ToListAsync());
		}

		// GET: Students
		private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


		[Authorize(Roles = "ADMIN , Teacher")]
		public async Task<IActionResult> Index(int id , int? page)
        {
			var pageNumber = page ?? 1;
			int pageSize = 10;
			return View( _context.Students.Include(s => s.Fakulteti).Where(s => s.FakultetiId == id).ToPagedList(pageNumber, pageSize));
		}


		[Authorize(Roles = "User")]
		public async Task<IActionResult> MyProfile(string name, string email)
		{
			var user = await GetCurrentUserAsync();
			name = user.FullName;
			email = user.Email;
			return View(await _context.Students.Include(s=>s.Fakulteti).Include(t=>t.State).Include(x=>x.Nationality).Where(x => x.Name.Equals(name) && x.Email.Equals(email)).ToListAsync());
		}

		// GET: Students/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.Fakulteti).Include(s=>s.Nationality).Include(s=>s.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

		// GET: Students/Create
		
		
		public IActionResult Create()
		{
		


			ViewData["FakultetiId"] = new SelectList(_context.Fakultetet, "Id", "Emri");

	
			ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
                

			ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name");
		
			return View();
		}

	
		public IActionResult RegisterCompleted()
        {
            return View();
        }

     

		// POST: Students/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,ParentName,Gender,Birthday,Residence,NationalityId,StateId,Phone,ImageUrl,Email,Password,ConfirmPassword,FakultetiId")] Student student)
        {
			
			/*_context.Add(student);*/
			await _register.Register(student);
            _context.Students.Add(student);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RegisterCompleted));
           
        

        }


		public JsonResult CheckEmailAvailability(string userdata)
		{
			System.Threading.Thread.Sleep(200);


			var SeachData = _context.Students.Where(x => x.Email == userdata).SingleOrDefault();
			if (SeachData != null)
			{
				return Json(1);
			}
			else
			{
				return Json(0);
			}

		}

		// GET: Students/Edit/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Edit(int? id)
        {
        
            ViewData["FakultetiId"] = new SelectList(_context.Fakultetet, "Id", "Emri");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
            ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name");
     
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "ADMIN")]
     
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,ParentName,Gender,Birthday,Residence,NationalityId,StateId,Phone,ImageUrl,Email,Password,ConfirmPassword,FakultetiId")] Student student)
        {

            if (id != student.Id)
            {
                return NotFound();
            }

            var st = new Student()
            {
                Id = student.Id,
				 Name= student.Name,
				Surname = student.Surname,
                ParentName = student.ParentName,
                Gender = student.Gender,
                Birthday = student.Birthday,
                Residence = student.Residence,
                NationalityId = student.NationalityId,
                StateId = student.StateId,
                Phone = student.Phone,
                ImageUrl = student.ImageUrl,
                Email = student.Email,
				Password = student.Password,
                ConfirmPassword= student.ConfirmPassword,
				FakultetiId =student.FakultetiId,





			};
           
                try
            {
         
                _context.Update(st);
              
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Fakultetet));
          
        }

		// GET: Students/Delete/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s=>s.Fakulteti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

		// POST: Students/Delete/5
		[Authorize(Roles = "ADMIN")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
           
            await _context.SaveChangesAsync();
            var user = await _userManager.FindByEmailAsync(student.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Fakultetet));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
           
             return RedirectToAction(nameof(Index));
        }
      

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
