using System;
using System.Collections.Generic;
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
    public class StudentTeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentTeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

	
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Fakultetet()
		{
			return View(await _context.Fakultetet.ToListAsync());
		}


		
		[Authorize(Roles = "ADMIN , Teacher")]
		public async Task<IActionResult> Students(int id, int? page)
		{

            var pageNumber = page ?? 1;
            int pageSize = 10;
            var applicationDbContext = _context.Students.Include(x=>x.Fakulteti).Where(s => s.FakultetiId==id && s.StudentTeachers.Any(s => s.Student.Id == s.StudentId)).ToPagedList(pageNumber, pageSize);
            return View(applicationDbContext);
		}
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.StudentTeacher.Include(s => s.Student).Include(t => t.Teacher).Where(s=>s.Student.Id==id).ToList();
            return View(applicationDbContext);
        }

        // GET: StudentTeachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentTeacher == null)
            {
                return NotFound();
            }

            var studentTeacher = await _context.StudentTeacher
                .Include(s => s.Student)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentTeacher == null)
            {
                return NotFound();
            }

            return View(studentTeacher);
        }

		// GET: StudentTeachers/Create
		/* public IActionResult CreateFirst(int id)
                {
                    ViewData["StudentId"] = new SelectList(_context.Students.Where(s => s.FakultetiId == id), "Id", "Name");
                    ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(s => s.FakultetiId == id), "Id", "Name");
                    return View();
                }*/
		/*     public IActionResult CreateFirst()
			 {
				 ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
				 ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
				 return View();
			 }*/
		public IActionResult CreateFirst(int id)
		{
			ViewData["StudentId"] = new SelectList(_context.Students.Where(s=>s.FakultetiId==id), "Id", "Email");
			ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(s => s.FakultetiId == id), "Id", "Email");
			return View();
		}

		public IActionResult Create(int id)
        {
            var std = _context.Students.Where(s => s.Id.Equals(id)).ToList();
            var stds = _context.StudentTeacher.FirstOrDefault(s=>s.StudentId==id);
            ViewData["StudentId"] = new SelectList(std, "Id", "Email");
            var data = _context.Teachers.Include(x=>x.Fakulteti).Where(s => stds.Student.FakultetiId==s.Fakulteti.Id && s.StudentTeachers.All(t => t.Teacher.Id != stds.TeacherId && t.Student.Id != stds.StudentId)).ToList();
           // var tcs = _context.Teachers.Include(s => s.StudentTeachers).Where(c => c.Id != stds.TeacherId && c.);
            ViewData["TeacherId"] = new SelectList(data, "Id", "Email");
            return View();
        }

        // POST: StudentTeachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,TeacherId")] StudentTeacher studentTeacher)
        {
           
                _context.Add(studentTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Fakultetet));
    
        }

        // GET: StudentTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentTeacher == null)
            {
                return NotFound();
            }

            var studentTeacher = await _context.StudentTeacher.FindAsync(id);
            if (studentTeacher == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentTeacher.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", studentTeacher.TeacherId);
            return View(studentTeacher);
        }

        // POST: StudentTeachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,TeacherId")] StudentTeacher studentTeacher)
        {
            if (id != studentTeacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentTeacherExists(studentTeacher.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", studentTeacher.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", studentTeacher.TeacherId);
            return View(Fakultetet);
        }

        // GET: StudentTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentTeacher == null)
            {
                return NotFound();
            }

            var studentTeacher = await _context.StudentTeacher
                .Include(s => s.Student)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentTeacher == null)
            {
                return NotFound();
            }

            return View(studentTeacher);
        }

        // POST: StudentTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentTeacher == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentTeacher'  is null.");
            }
            var studentTeacher = await _context.StudentTeacher.FindAsync(id);
            if (studentTeacher != null)
            {
                _context.StudentTeacher.Remove(studentTeacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Fakultetet));
        }

        private bool StudentTeacherExists(int id)
        {
          return _context.StudentTeacher.Any(e => e.Id == id);
        }
    }
}
