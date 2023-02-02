using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Data.Services;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IAccountService _register;
        private readonly UserManager<ApplicationUser> _userManager;
        public TeachersController(ApplicationDbContext context,IAccountService register, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _register = register;
            _userManager = userManager;

        }

		// GET: Teachers
		/*    [Authorize(Roles = "ADMIN")]
			public async Task<IActionResult> Index()
			{
				  return View(await _context.Teachers.Include(s=>s.Fakulteti).ToListAsync());
			}*/
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Index(int? page)
		{
			var pageNumber = page ?? 1;
			int pageSize = 10;

			return View( _context.Teachers.Include(s => s.Fakulteti).ToPagedList(pageNumber, pageSize));
		}
	
		// GET: Teachers/Details/5
		[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.Include(s => s.Fakulteti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Teachers/Create
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
			ViewData["FakultetiId"] = new SelectList(_context.Fakultetet, "Id", "Emri");
			return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NrPersonal,Name,Surname,BirthDay,Email,TitulliShkencor,Angazhimi,Angazhuar,Gender,Phone,Password,FakultetiId")] Teacher teacher)
        {

            await _register.RegisterTeacher(teacher);
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
        }
        [Authorize(Roles = "ADMIN ,Teacher")]
        public async Task<IActionResult> MyStudents(string name)
        {
            var user = await GetCurrentUserAsync();
            name = user.FullName;
			var applicationDbContext = _context.StudentTeacher.Include(s => s.Student).Include(s => s.Teacher).Where(x=>x.Teacher.Name.Equals(name));
            return View(await applicationDbContext.ToListAsync());
        }
        /*public async Task<IActionResult> MyStudentsToGrade(string name)
        {
            var user = await GetCurrentUserAsync();
            name = user.FullName;
            var applicationDbContext = _context.StudentTeacher.Include(s => s.Student).Include(s => s.Teacher).ThenInclude(s => s.SubjectTeachers).ThenInclude(s => s.Subject).ThenInclude(s => s.Transkripta).Where(x => x.Teacher.Name == name);
            return View(await applicationDbContext.ToListAsync());
        }*/
        // GET: Teachers/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
			ViewData["FakultetiId"] = new SelectList(_context.Fakultetet, "Id", "Emri");
			if (id == null || _context.Teachers== null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NrPersonal,Name,Surname,BirthDay,Email,TitulliShkencor,Angazhimi,Angazhuar,Gender,Phone,Password,FakultetiId")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            
                try
                {
                var tr = new Teacher()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Surname = teacher.Surname,
                    BirthDay = teacher.BirthDay,
                    Email = teacher.Email,
                    Phone = teacher.Phone,
                    Password = teacher.Password,
                    NrPersonal=teacher.NrPersonal,
                    TitulliShkencor=teacher.TitulliShkencor,
                    Angazhimi=teacher.Angazhimi,
                    Angazhuar=teacher.Angazhuar,
                    Gender=teacher.Gender,
					FakultetiId=teacher.FakultetiId,
				};
                    _context.Update(tr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
        }

        // GET: Teachers/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["FakultetiId"] = new SelectList(_context.Fakultetet, "Id", "Emri");
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.Include(s => s.Fakulteti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["FakultetiId"] = new SelectList(_context.Fakultetet, "Id", "Emri");
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
            
            await _context.SaveChangesAsync();
			var user = await _userManager.FindByEmailAsync(teacher.Email);
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
					return View("~/Views/Home/Index.cshtml");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return RedirectToAction(nameof(Index));
		}

        private bool TeacherExists(int id)
        {
          return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
