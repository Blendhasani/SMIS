using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubjectsController(ApplicationDbContext context,UserManager<ApplicationUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Subjects
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Subjects.ToListAsync());
        }

		// GET: Subjects/Details/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

		// GET: Subjects/Create
		[Authorize(Roles = "ADMIN")]
		public IActionResult Create()
        {
            return View();
        }

		// POST: Subjects/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "ADMIN")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kodi,Name,NameTranslated,ECTS,KoheZgjatja,GjuhaLigjerimit,Semester,Kategoria")] Subject subject)
        {
            
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
        }

		// GET: Subjects/Edit/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }
		/*    public async Task<IActionResult> MySubjects(string name)
			{
				var user = await GetCurrentUserAsync();
				name = user.FullName;

				return View(await _context.Subjects.Include(x=>x.Transkripta).ThenInclude(x=>x.Student).Where(x=>Student. && x.Name == name)).ToListAsync();
			}*/

		// POST: Subjects/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "ADMIN")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kodi,Name,NameTranslated,ECTS,KoheZgjatja,GjuhaLigjerimit,Semester,Kategoria")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
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

		// GET: Subjects/Delete/5
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }
		[Authorize(Roles = "ADMIN")]
		// POST: Subjects/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subjects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Subjects'  is null.");
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
          return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
