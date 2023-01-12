using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TranskriptasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
       Teacher t = new Teacher();
        public TranskriptasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Transkriptas
   
		[Authorize(Roles = "ADMIN ,Teacher")]
		public async Task<StudentTeacher> MyStudents(string name)
		{
			var user = await GetCurrentUserAsync();
			name = user.FullName;
			var applicationDbContext = _context.StudentTeacher.Include(s => s.Student).Include(s => s.Teacher).FirstOrDefault(x => x.Teacher.Name.Equals(name));
            var test = applicationDbContext.Teacher.Name;
            return applicationDbContext;
		}

		


		[Authorize(Roles = "ADMIN ,User")]
        public async Task<IActionResult> MyTranscript(string name)
        {
            var user = await GetCurrentUserAsync();
            name = user.FullName;
           

         
            var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Student.Name.Equals(name));
            return View(await applicationDbContext.ToListAsync());
        }

		[Authorize(Roles = "ADMIN ,Teacher")]
		public async Task<IActionResult> Index(string name)
		{
            var user = await GetCurrentUserAsync();
            name = user.FullName;


            //var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Student.StudentTeachers.Any(st => st.Teacher.Name == name));
			var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Subject.SubjectTeachers.Any(st => st.Teacher.Name == name));
			return View(await applicationDbContext.ToListAsync());
		}

        [Authorize(Roles = "ADMIN ,Teacher")]
        public async Task<IActionResult> Vleresimi(string name)
        {
            var user = await GetCurrentUserAsync();
            name = user.FullName;


            //var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Student.StudentTeachers.Any(st => st.Teacher.Name == name));
            var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Subject.SubjectTeachers.Any(st => st.Teacher.Name == name));
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "ADMIN ,Teacher ,User")]
        public async Task<IActionResult> ProvimetEParaqitura(string name)
        {
            var user = await GetCurrentUserAsync();
            name = user.FullName;

            var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Student.Name.Equals(name));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transkriptas/Details/5
        [Authorize(Roles = "ADMIN ,Teacher")]
        public async Task<IActionResult> Details(string name)
        {
           /* if (id == null || _context.Transkripta == null)
            {
                return NotFound();
            }*/
           

          
            var transkripta = await _context.Transkripta
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.Student.Name.Equals(name));
            if (transkripta == null)
            {
                return NotFound();
            }

            return View(transkripta);
        }


		// GET: Transkriptas/Create
		[Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
			var user = await GetCurrentUserAsync();
			var namee = user.FullName;
            var st = _context.Students.Single(t=>t.Name.Equals(namee));
			ViewData["StudentId"] = new SelectList(_context.Students, "Id", namee);
			var data = _context.Subjects.Where(s => s.Transkripta.All(t => t.Student.Id!=st.Id && t.SubjectId  == s.Id)).ToList();

			//var datas = _context.Subjects.Where();
			/* var subject = _context.Subjects.ToListAsync();

                var datas = _context.Transkripta.Include(s=>s.Subject).Where(t=>t.SubjectId!= t.Subject.Id);

                var test = _context.Subjects.Where(datas.All(t => t.Student.Name == namee));*/

			/*       var query =
					   from transkripta in _context.Transkripta
					   join subject in _context.Subjects on transkripta.SubjectId equals subject.Id
					   select subject.Id;
				   var subs = from subject in _context.Subjects select subject.Id;
				   var list = query.Where(subs.)*/
			/*	var data = _context.Subjects.Where(s => s.Transkripta.All(t => t.Student.Name == namee && datas)).ToList();*/
			ViewData["SubjectId"] = new SelectList(data, "Id", "Name");
			/* ViewData["SubjectId"] = new SelectList(_context.Subjects
				 .Include(t => t.SubjectTeachers)
				 .ThenInclude(s => s.Teacher)
				 .ThenInclude(s => s.StudentTeachers)
				 .ThenInclude(s => s.Student)
				 .Where(s => s.Transkripta.Any(s => s.Nota == snull)), "Id", "Name");*/

			//var data = _context.Subjects.Where(s => s.Transkripta.All(t => t.Student.Name == namee && t.SubjectId != subjects.Id)).ToList();
			/*  ViewData["SubjectId"] = new SelectList(_context.Subjects.Include(s=>s.Transkripta).Where(s=>s.Transkripta.Any(s=>s.Nota==null && s.StudentId!=null)), "Id", "Name");*/
			// profa 			var data = _context.Subjects.Where(s => s.Transkripta.All(t => t.Student.Name == namee && t.SubjectId != s.Id)).ToList();


			return View();
        }
   

        // POST: Transkriptas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("Id,Nota,StudentId,SubjectId")] Transkripta transkripta)
        {
            var user = await GetCurrentUserAsync();
            var namee = user.FullName;
           var student = _context.Students.Where(s=>s.Name.Equals(namee)).FirstOrDefault();

          
            
            var tr = new Transkripta()
            {
                Id = transkripta.Id,
                Nota = transkripta.Nota,
                StudentId= student.Id,
                SubjectId= transkripta.SubjectId,

            };
          
                _context.Add(tr);
                await _context.SaveChangesAsync();
               // return RedirectToAction(nameof(Create));
            return View("~/Views/Home/Index.cshtml");
        }

        // GET: Transkriptas/Edit/5
        [Authorize(Roles = "User ,Teacher ,ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transkripta == null)
            {
                return NotFound();
            }

            var transkripta = await _context.Transkripta.FindAsync(id);
            if (transkripta == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", transkripta.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", transkripta.SubjectId);
            return View(transkripta);
        }
        /*      //ketu kom mbet
              public async Task<IActionResult> MyStudentsToGrade(int id)
              {

                  var user = await GetCurrentUserAsync();
                  var namee = user.FullName;

                  var applicationDbContext = _context.Transkripta
                      .Include(t => t.Student)
                      .Include(t => t.Subject)
                      .ThenInclude(t=>t.SubjectTeachers)
                      .ThenInclude(t=>t.Teacher)
                      .ThenInclude(t=>t.StudentTeachers).
                      ThenInclude(t=>t.TeacherId);
                  if (applicationDbContext == id)
                  {
                      return View(await applicationDbContext.ToListAsync());
                  }


                  return View("NotFound");



              }*/

        // POST: Transkriptas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User ,Teacher ,ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nota,StudentId,SubjectId")] Transkripta transkripta)
        {
            if (id != transkripta.Id)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(transkripta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranskriptaExists(transkripta.Id))
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

        // GET: Transkriptas/Delete/5
        [Authorize(Roles = "User ,Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transkripta == null)
            {
                return NotFound();
            }

            var transkripta = await _context.Transkripta
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transkripta == null)
            {
                return NotFound();
            }

            return View(transkripta);
        }

        // POST: Transkriptas/Delete/5
        [Authorize(Roles = "User ,Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transkripta == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transkripta'  is null.");
            }
            var transkripta = await _context.Transkripta.FindAsync(id);
            if (transkripta != null)
            {
                _context.Transkripta.Remove(transkripta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyTranscript));
        }

        private bool TranskriptaExists(int id)
        {
          return _context.Transkripta.Any(e => e.Id == id);
        }
    }
}
