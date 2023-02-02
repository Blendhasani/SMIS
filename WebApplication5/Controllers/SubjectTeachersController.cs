using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class SubjectTeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectTeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: SubjectTeachers
		/*      public async Task<IActionResult> Index()
			  {
				  var applicationDbContext = _context.SubjectTeacher.Include(s => s.Subject).Include(s => s.Teacher);
				  return View(await applicationDbContext.ToListAsync());
			  }*/


		/*        [Authorize(Roles = "ADMIN , Teacher")]
				public async Task<IActionResult> Students()
				{

					var applicationDbContext = _context.Students.Where(s => s.StudentTeachers.Any(s => s.Student.Id == s.StudentId)).ToList();
					return View(applicationDbContext);
				}
				public async Task<IActionResult> Index(int id)
				{
					var applicationDbContext = _context.StudentTeacher.Include(s => s.Student).Include(t => t.Teacher).Where(s => s.Student.Id == id).ToList();
					return View(applicationDbContext);
				}*/

		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Fakultetet()
		{
			return View(await _context.Fakultetet.ToListAsync());
		}
		[Authorize(Roles = "ADMIN , Teacher")]
		public async Task<IActionResult> Teachers(int id, int? page)
		{
			var pageNumber = page ?? 1;
			int pageSize = 10;

			var applicationDbContext = _context.Teachers.Include(x => x.Fakulteti).Where(s => s.Fakulteti.Id==id && s.SubjectTeachers.Any(s => s.Teacher.Id == s.TeacherId)).ToPagedList(pageNumber, pageSize);
			return View(applicationDbContext);
		}
		public async Task<IActionResult> Index(int id)
		{
			var applicationDbContext = _context.SubjectTeacher.Include(s => s.Subject).Include(s => s.Teacher).Where(s => s.Teacher.Id == id).ToList();
			return View(applicationDbContext);
		}

		// GET: SubjectTeachers/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubjectTeacher == null)
            {
                return NotFound();
            }

            var subjectTeacher = await _context.SubjectTeacher
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectTeacher == null)
            {
                return NotFound();
            }

            return View(subjectTeacher);
        }

        // GET: SubjectTeachers/Create
        public IActionResult CreateFirst()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
            return View();
        }
        /*public IActionResult CreateFirst(int id)
		{
			ViewData["SubjectId"] = new SelectList(_context.Subjects.Where(s=>s.FakultetiId==id), "Id", "Name");
			ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(s => s.FakultetiId == id), "Id", "Name");
			return View();
		}*/
        /*public IActionResult Create(int id)
        {
            var tcs = _context.Teachers.Where(s => s.Id.Equals(id)).ToList();
            var tcss = _context.SubjectTeacher.FirstOrDefault(s => s.TeacherId == id);
          
            var data = _context.Subjects.Where(s => s.SubjectTeachers.All(t => t.Teacher.Id != tcss.TeacherId && t.Subject.Id != tcss.SubjectId)).ToList();
      
            ViewData["SubjectId"] = new SelectList(data, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(tcs, "Id", "Name");
            return View();
        }*/

        public IActionResult Create(int id)
		{
			var tcs = _context.Teachers.Where(s => s.Id.Equals(id)).ToList();
			var tcss = _context.SubjectTeacher.FirstOrDefault(s => s.Teacher.Id == id);

			var data = _context.Subjects.Include(x => x.Fakulteti).Where(s => s.Fakulteti.Id==tcss.Teacher.FakultetiId && s.SubjectTeachers.All(t => t.Teacher.Id != tcss.TeacherId && t.Subject.Id != tcss.SubjectId)).ToList();

			ViewData["SubjectId"] = new SelectList(data, "Id", "Name");
			ViewData["TeacherId"] = new SelectList(tcs, "Id", "Name");
			return View();
		}





		// POST: SubjectTeachers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,TeacherId")] SubjectTeacher subjectTeacher)
        {
         
                _context.Add(subjectTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Teachers));
          

        }

        // GET: SubjectTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubjectTeacher == null)
            {
                return NotFound();
            }

            var subjectTeacher = await _context.SubjectTeacher.FindAsync(id);
            if (subjectTeacher == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", subjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // POST: SubjectTeachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId,TeacherId")] SubjectTeacher subjectTeacher)
        {
            if (id != subjectTeacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectTeacherExists(subjectTeacher.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", subjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // GET: SubjectTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubjectTeacher == null)
            {
                return NotFound();
            }

            var subjectTeacher = await _context.SubjectTeacher
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectTeacher == null)
            {
                return NotFound();
            }

            return View(subjectTeacher);
        }

        // POST: SubjectTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubjectTeacher == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubjectTeacher'  is null.");
            }
            var subjectTeacher = await _context.SubjectTeacher.FindAsync(id);
            if (subjectTeacher != null)
            {
                _context.SubjectTeacher.Remove(subjectTeacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectTeacherExists(int id)
        {
          return _context.SubjectTeacher.Any(e => e.Id == id);
        }
    }
}
