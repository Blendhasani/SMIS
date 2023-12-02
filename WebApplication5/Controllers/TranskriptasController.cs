using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using System.Linq.Expressions;
using WebApplication5.Migrations;
using FastReport;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using FastReport.Utils;
using Highsoft.Web.Mvc.Charts;
using System.Composition;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace WebApplication5.Controllers
{
	public class TranskriptasController : Controller
	{

		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		Teacher t = new Teacher();
		public TranskriptasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
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
			return View(await applicationDbContext.OrderBy(s => s.CreatedDate).ToListAsync());
		}

	

		[Authorize(Roles = "Teacher")]
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




		// GET: Transkriptas/Create origjinal
			[Authorize(Roles = "User")]
			public async Task<IActionResult> Create()
			{
				var a = _context.Afati.FirstOrDefault(a => a.Emri.Equals("Afati Shkurt"));
				var user = await GetCurrentUserAsync();
				var namee = user.FullName;

				var st = _context.Students.Include(s => s.Fakulteti).Single(t => t.Name.Equals(namee));
				if (a.Hapur == true)
				{
					ViewData["StudentId"] = new SelectList(_context.Students, "Id", namee);

					var data = _context.Subjects.Include(f => f.Fakulteti).Where(s => s.FakultetiId == st.FakultetiId && s.Transkripta.All(t => t.Student.Id != st.Id && t.SubjectId == s.Id)).ToList();


					ViewData["SubjectId"] = new SelectList(data, "Id", "Name");
				}
				else
				{
					ViewData["message"] = "Afati nuk është hapur akoma";
				}



				return View();
			}
/*
		//test
		[Authorize(Roles = "User")]
		public async Task<IActionResult> Create()
		{
			var a = _context.Afati.Skip(1).FirstOrDefault() ?? _context.Afati.FirstOrDefault();

			var user = await GetCurrentUserAsync();
			var namee = user.FullName;

			var st = _context.Students.Include(s => s.Fakulteti).Single(t => t.Name.Equals(namee));
			if (a.Hapur == true)
			{
				ViewData["StudentId"] = new SelectList(_context.Students, "Id", namee);

				var data = _context.Subjects.Include(f => f.Fakulteti).Where(s => s.FakultetiId == st.FakultetiId && s.Transkripta.All(t => t.Student.Id != st.Id && t.SubjectId == s.Id)).ToList();


				ViewData["SubjectId"] = new SelectList(data, "Id", "Name");
			}
			else if(a.Hapur==false)
			{
				ViewData["message"] = "Afati nuk është hapur akoma";
			}



			return View();
		}
*/
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> Create([Bind("Id,Nota,StudentId,SubjectId")] Transkripta transkripta)
		{
			var user = await GetCurrentUserAsync();
			var namee = user.FullName;
			var student = _context.Students.Where(s => s.Name.Equals(namee)).FirstOrDefault();



		
			//var afatii = _context.Afati.FirstOrDefault(s => s.Emri == "Afati Prill" && s.VitiAkademik=="2022/2023");
			var afatii = _context.Afati.FirstOrDefault(s => s.Hapur==true && s.VitiAkademik=="2022/2023");
			var tr = new Transkripta()
			{
				Id = transkripta.Id,
				Nota = transkripta.Nota,
				StudentId = student.Id,
				SubjectId = transkripta.SubjectId,
				CreatedDate = DateTime.Now,


			};

			

			_context.Add(tr);
            var afatitranskriptat = _context.AfatiTranskripta.Include(t => t.Transkripta).Where(t => t.Transkripta.Student.Email == student.Email && t.AfatiId==afatii.Id).ToList();
            if (afatii.Hapur == true && afatitranskriptat.Count < afatii.NrProvimeve)
			{
				await _context.SaveChangesAsync();
			}

            var at = new AfatiTranskripta()
			{
				AfatiId = afatii.Id,
				TranskriptaId = tr.Id
			};
			_context.Add(at);
			
			


			if(afatii.Hapur==true && afatitranskriptat.Count < afatii.NrProvimeve)
			{
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["error"] = "Nuk mund te paraqitni me shume provime";
            }
            return RedirectToAction(nameof(Create));
		


		}

		



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

			var data = _context.Students.Where(s => s.Transkripta.Any(t => s.Id == t.Student.Id && t.Id == id)).ToList();
			var dataS = _context.Subjects.Where(s => s.Transkripta.Any(t => s.Id == t.Subject.Id && t.Id == id)).ToList();
			ViewData["StudentId"] = new SelectList(data, "Id", "Name", transkripta.StudentId);
			ViewData["SubjectId"] = new SelectList(dataS, "Id", "Name", transkripta.SubjectId);

			return View(transkripta);
		}

		// POST: Transkriptas/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "User ,Teacher ,ADMIN")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Nota,StudentId,SubjectId,CreatedDate")] Transkripta transkripta)
		{
			/*  if (id != transkripta.Id)
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
			  return RedirectToAction(nameof(Index));*/
			if (id != transkripta.Id)
			{
				return NotFound();
			}

		
			var transkript = new Transkripta()
			{
				Id = transkripta.Id,
				Nota = transkripta.Nota,
				StudentId = transkripta.StudentId,
				SubjectId = transkripta.SubjectId,
				CreatedDate = DateTime.Now,

			};


			var stId = transkript.StudentId;
			var students = _context.Students.FirstOrDefault(s => s.Id == stId);

			var sbId = transkript.SubjectId;
			var subjectt = _context.Subjects.FirstOrDefault(s => s.Id == sbId);

			_context.Update(transkript);
			await _context.SaveChangesAsync();

			try
			{
				using (MailMessage mail = new MailMessage())
				{
					mail.From = new MailAddress("mygganbu@gmail.com");
					mail.To.Add(students.Email);
                    CultureInfo albanianCulture = new CultureInfo("sq-AL");
                    TextInfo textInfo = albanianCulture.TextInfo;
                    string formattedDate = transkript.CreatedDate.ToString("d MMMM yyyy", albanianCulture);
                    string capitalizedDate = textInfo.ToTitleCase(formattedDate);
                    mail.Subject = "Nota për lëndën " + subjectt.Name;
					mail.Body = "<div>\r\n<span style=\"font-family:Arial;font-size:10pt\">\r\nI/E nderuar <strong>" + students.Name + " " + students.Surname + "</strong>,\r\n<br><br>\r\nMe datën <strong>" + capitalizedDate + "</strong> në <span class=\"il\">SMIS</span> është regjistruar nota për lëndën <strong>" + subjectt.Kodi + " "+ subjectt.Name + "</strong>" +
						".<br>\r\nNota e regjistruar për këtë lëndë është <strong>" + transkript.Nota + "</strong>.<br><br>\r\n\r\n" +
						"Për më shumë informata vizitoni profilin tuaj në <span class=\"il\">SMIS</span>, ndërsa për detaje shtesë rreth notës kontaktoni profesorin.<br><br>" +
						"\r\n\r\n<strong>Vërejtje:</strong> Përmes sistemit <span class=\"il\">SMIS</span> ju keni mundësi ta refuzoni notën deri në <strong>48 orë </strong>" +
						"pas vendosjes së notës në sistem.<br><br>\r\n\r\n\r\nJu lutem, mos ktheni përgjigje në këtë email.<br><br>\r\n</span>\r\n<div style=\"border-top:3px solid #023164\">&nbsp;</div>\r\n© <span class=\"il\">SMIS</span> -  Student Management Information System\r\n\r\n</div>";
					mail.IsBodyHtml = true;
					using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
					{
						smtp.Credentials = new System.Net.NetworkCredential("mygganbu@gmail.com", "koirjottsbxsybss");
						smtp.EnableSsl = true;
						smtp.Send(mail);

					}
				}


			}
			catch (Exception ex)
			{
				throw ex;
			}
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Lendet));
		}
	


        [Authorize(Roles = "ADMIN , Teacher")]
        public async Task<IActionResult> Lendet(string name)
        {
            var currentUser = await GetCurrentUserAsync();
             name = currentUser.FullName;
          
            var applicationDbContext = _context.Subjects.Where(s => s.SubjectTeachers.Any(s => s.Teacher.Name == name)).ToList();
            return View(applicationDbContext);
        }
        [Authorize(Roles = "ADMIN ,Teacher")]
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject).Where(t => t.Subject.SubjectTeachers.Any(st => st.Subject.Id==id));
            return View(await applicationDbContext.ToListAsync());
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
			var afatitranskripta = await _context.AfatiTranskripta.FirstOrDefaultAsync(m=>m.TranskriptaId == id);
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
            
         
            if (User.IsInRole("User"))
            {
                return RedirectToAction(nameof(MyTranscript));
            }
            return RedirectToAction(nameof(Index));
        }

		
	




		private bool TranskriptaExists(int id)
        {
          return _context.Transkripta.Any(e => e.Id == id);
        }
    }
}
