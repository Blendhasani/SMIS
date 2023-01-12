using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Data.Services;
using WebApplication5.Models;

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

        // GET: Students
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize(Roles ="ADMIN , Teacher")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Students.ToListAsync());
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyProfile(string name,string email)
        {
            var user = await GetCurrentUserAsync();
            name = user.FullName;
            email =user.Email;
            return View(await _context.Students.Where(x=>x.Name.Equals(name) && x.Email.Equals(email)).ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
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
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,ParentName,Gender,Birthday,Residence,Nationality,State,Phone,ImageUrl,Email,Password")] Student student)
        {

             /*_context.Add(student);*/
         await _register.Register(student);
            _context.Students.Add(student);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RegisterCompleted));
           
        

        }

		// GET: Students/Edit/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Edit(int? id)
        {
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
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,ParentName,Gender,Birthday,Residence,Nationality,State,Phone,ImageUrl,Email")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(student);
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
                return RedirectToAction(nameof(Index));
          
        }

		// GET: Students/Delete/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
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
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
