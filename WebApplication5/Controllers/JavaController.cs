using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;
using FastReport;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Data.ViewModel;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class JavaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public JavaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Merr listen e grupeve
        public async Task<IActionResult> Index(int id)
        {
            return _context.GrupiLenda != null ?
                        View(await _context.GrupiLenda.Include(x=>x.Grupi).Include(x=>x.Subject).Where(y=>y.SubjectId == id).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Grupet'  is null.");
        }

        //Merr listen e javeve
        public async Task<IActionResult> Javet(int id)
        {
            return _context.Javet != null ?
                        View(await _context.Javet.Where(y => y.GrupiLendaId == id).OrderBy(x=>x.JavaNumri).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Grupet'  is null.");
        }

        //Gjeneron listen e caktuar ne format PDF
        public FileResult GjeneroJaven(int id)
        {

            var currentWeek =  _context.Javet.Include(s => s.GrupiLenda).ThenInclude(s => s.Grupi).Where(x => x.Id == id).FirstOrDefault();

            List<Student> students = new List<Student>();

            // Krijo nje regex qe perputhet me shifrat
            string pattern = @"\d+";

            // Merr id pa space
            string pjesemarrja = Regex.Replace(currentWeek.Pjesemarrja, @"\s+", "");

            // Merr te gjitha shifrat
            MatchCollection digitMatches = Regex.Matches(pjesemarrja, pattern);
            List<string> digitsList = digitMatches.Cast<Match>().Select(m => m.Value).ToList();

            // Rfid jane 10 shifra
            int rfidLength = 10;

            // Itero ne cdo set te shifrave ne listen digits 
            foreach (string digits in digitsList)
            {
                int numIterations = digits.Length / rfidLength;

                // Itero ne cdo shifer te 10 
                for (int i = 0; i < numIterations; i++)
                {
                    // Llogarit indeksin fillestar te rfid aktuale ne vargun e shifrave
                    int startIndex = i * rfidLength;

                    //Ekstraktoni rfid aktuale 10 shifrore nga vargu i shifrave
                    string rfid = digits.Substring(startIndex, rfidLength);

                    // Merrni studentet qe e kane rfid te barabarte me rfid
                    var student = _context.Students.FirstOrDefault(x => x.Rfid == rfid);

                    if (student != null)
                    {
                        students.Add(student);
                    }
                }
            }

            List<DetailsViewModel> detailsList = students.Select(s => new DetailsViewModel
            {
                Name = s.Name,
                Surname = s.Surname,
                Rfid = s.Rfid,
                Data = currentWeek.Data.ToShortDateString(),
                Grupi = currentWeek.GrupiLenda.Grupi.Emri,
            }).DistinctBy(s => s.Rfid).OrderBy(x => x.Name).ToList();

            if (detailsList.Select(x => x.Grupi).FirstOrDefault() == "G1a")
            {
				List<Student> studentet = detailsList.Select(s => new Student
				{
					Name = s.Name,
					Surname = s.Surname,
					Rfid = s.Rfid,
				}).ToList();
				FastReport.Utils.Config.WebMode = true;
				Report rep = new Report();
				string path = Path.Combine("./Students.frx");
				rep.Load(path);


				rep.RegisterData(studentet, "StudentsRef");

				if (rep.Report.Prepare())
				{
					FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
					pdfExport.ShowProgress = false;
					pdfExport.Subject = "Subject Report";
					pdfExport.Title = "Report Title";
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					rep.Report.Export(pdfExport, ms);
					pdfExport.Dispose();
					ms.Position = 0;
					return File(ms, "application/pdf", "g1a.pdf");

				}
				else
				{
					return null;
				}
            }
            else if(detailsList.Select(x => x.Grupi).FirstOrDefault() == "G1b")
            {
				List<Student> studentet = detailsList.Select(s => new Student
				{
					Name = s.Name,
					Surname = s.Surname,
					Rfid = s.Rfid,
				}).ToList();
				FastReport.Utils.Config.WebMode = true;
				Report rep = new Report();
				string path = Path.Combine("./G1b.frx");
				rep.Load(path);


				rep.RegisterData(studentet, "StudentsRef");

				if (rep.Report.Prepare())
				{
					FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
					pdfExport.ShowProgress = false;
					pdfExport.Subject = "Subject Report";
					pdfExport.Title = "Report Title";
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					rep.Report.Export(pdfExport, ms);
					pdfExport.Dispose();
					ms.Position = 0;
					return File(ms, "application/pdf", "g1b.pdf");

				}
				else
				{
					return null;
				}
			}
			else if (detailsList.Select(x => x.Grupi).FirstOrDefault() == "G2a")
			{
				List<Student> studentet = detailsList.Select(s => new Student
				{
					Name = s.Name,
					Surname = s.Surname,
					Rfid = s.Rfid,
				}).ToList();
				FastReport.Utils.Config.WebMode = true;
				Report rep = new Report();
				string path = Path.Combine("./G2a.frx");
				rep.Load(path);


				rep.RegisterData(studentet, "StudentsRef");

				if (rep.Report.Prepare())
				{
					FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
					pdfExport.ShowProgress = false;
					pdfExport.Subject = "Subject Report";
					pdfExport.Title = "Report Title";
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					rep.Report.Export(pdfExport, ms);
					pdfExport.Dispose();
					ms.Position = 0;
					return File(ms, "application/pdf", "g2a.pdf");

				}
				else
				{
					return null;
				}
			}
			else if (detailsList.Select(x => x.Grupi).FirstOrDefault() == "G2b")
			{
				List<Student> studentet = detailsList.Select(s => new Student
				{
					Name = s.Name,
					Surname = s.Surname,
					Rfid = s.Rfid,
				}).ToList();
				FastReport.Utils.Config.WebMode = true;
				Report rep = new Report();
				string path = Path.Combine("./G2b.frx");
				rep.Load(path);


				rep.RegisterData(studentet, "StudentsRef");

				if (rep.Report.Prepare())
				{
					FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
					pdfExport.ShowProgress = false;
					pdfExport.Subject = "Subject Report";
					pdfExport.Title = "Report Title";
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					rep.Report.Export(pdfExport, ms);
					pdfExport.Dispose();
					ms.Position = 0;
					return File(ms, "application/pdf", "g2b.pdf");

				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}

		}

        //Merr detajet e javes se caktuar (studentet)
        public async Task<IActionResult> Details(int id)
        {
            var currentWeek = await _context.Javet.Include(s=>s.GrupiLenda).ThenInclude(s=>s.Grupi).Where(x => x.Id == id).FirstOrDefaultAsync();

            List<Student> students = new List<Student>();
        
       
            string pattern = @"\d+";

      
            string pjesemarrja = Regex.Replace(currentWeek.Pjesemarrja, @"\s+", "");


            MatchCollection digitMatches = Regex.Matches(pjesemarrja, pattern);
            List<string> digitsList = digitMatches.Cast<Match>().Select(m => m.Value).ToList();

     
            int rfidLength = 10;

          
            foreach (string digits in digitsList)
            {
                int numIterations = digits.Length / rfidLength;

                for (int i = 0; i < numIterations; i++)
                {
     
                    int startIndex = i * rfidLength;

                 
                    string rfid = digits.Substring(startIndex, rfidLength);

                 
                    var student = _context.Students.FirstOrDefault(x => x.Rfid == rfid);

                    if (student != null)
                    {
                        students.Add(student);
                    }
                }
            }

            List<DetailsViewModel> detailsList = students.Select(s => new DetailsViewModel
            {
                Name = s.Name,
                Surname = s.Surname,
                Rfid = s.Rfid,
                Data = currentWeek.Data.ToShortDateString(),
                Grupi = currentWeek.GrupiLenda.Grupi.Emri,
            }).DistinctBy(s=>s.Rfid).OrderBy(x=>x.Name).ToList();


            return View(detailsList);
        }


       //Merr pjesemarrjen totale dhe perqindjen per cdo student

        public async Task<IActionResult> GetPjesemarrjaTotale(int id)
        {
            var javet = await _context.Javet.Where(x => x.GrupiLendaId == id).ToListAsync();

            List<Student> students = new List<Student>();

      
            foreach (var java in javet)
            {
      
                string pattern = @"\d+";

                string pjesemarrja = Regex.Replace(java.Pjesemarrja, @"\s+", "");

              
                MatchCollection digitMatches = Regex.Matches(pjesemarrja, pattern);
                List<string> digitsList = digitMatches.Cast<Match>().Select(m => m.Value).ToList();

               
                int rfidLength = 10;

                HashSet<string> uniqueRfids = new HashSet<string>();

                
                foreach (string digits in digitsList)
                {
                    int numIterations = digits.Length / rfidLength;

                    
                    for (int i = 0; i < numIterations; i++)
                    {
                     
                        int startIndex = i * rfidLength;

                       
                        string rfid = digits.Substring(startIndex, rfidLength);

                       
                        if (!uniqueRfids.Contains(rfid))
                        {
                           
                            var student = _context.Students.FirstOrDefault(x => x.Rfid == rfid);

                            if (student != null)
                            {
                                students.Add(student);
                                // Shto rfid tek uniqueRfids
                                uniqueRfids.Add(rfid);
                            }
                        }
                    }
                }
            }

            List<PjesemarrjaTotaleVM> detailsList = students
                .GroupBy(s => new { s.Name, s.Surname, s.Rfid })
                .Select(g => new PjesemarrjaTotaleVM
                {
                    Name = g.Key.Name,
                    Surname = g.Key.Surname,
                    Rfid = g.Key.Rfid,
                    TotalAttendance = g.Count(),
                    Percentage = ((float)g.Count() / 12) * 100 // Kalkulo perqindjen ne baze te 12 jave
                })
				.OrderBy(x => x.Name)
				.ToList();

            return View(detailsList);
        }


        // GET: Merr lendet e mesimdhenesit te caktuar

        public async Task<IActionResult> Lendet(string name)
        {
            var currentUser = await GetCurrentUserAsync();
            name = currentUser.FullName;

            var applicationDbContext = _context.Subjects.Where(s => s.SubjectTeachers.Any(s => s.Teacher.Name == name)).ToList();
            return View(applicationDbContext);
        }
        // GET: Java/Create
      
        public IActionResult Create(int id)
        {



            // var grupet = _context.GrupiLenda.Where(gl => gl.Id == id).Select(gl => new { Value = gl.Grupi.Id, Text = $"{gl.Grupi.Emri} - {gl.Subject.Name}" });
            // var grupet = _context.GrupiLenda.Select(gl => new { Value = gl.Grupi.Id, Text = $"{gl.Grupi.Emri} - {gl.Subject.Name}" }).FirstOrDefault();
            /*  var grupet = _context.GrupiLenda.Select(x => new
              {
                  Value = $"{x.Id}",
                  Text = $"{x.Grupi.Emri} - {x.Subject.Name}"
              }).ToList();*/

         
            var grupet = _context.GrupiLenda.Where(x=>x.Id==id).Select(x => new
            {
                Value = $"{x.Id}",
                Text = $"{x.Grupi.Emri} - {x.Subject.Name}"
            }).ToList();
            ViewData["GrupiLendaList"] = new SelectList(grupet , "Value", "Text");



            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");


            return View();
        }
        //Krijo javen per here te pare
        public async Task<IActionResult> CreateFirst()
        {
			/*var subjectName = _context.Subjects.Where(s => _context.GrupiLenda.Any(gl => gl.SubjectId == s.Id && gl.Id==id)).Select(s=>s.Name).SingleOrDefault();*/

			var currentUser = await GetCurrentUserAsync();
			 var name = currentUser.FullName;
            var teacherSubject = _context.SubjectTeacher.Where(x => x.Teacher.Name == name).FirstOrDefault();
			/*	var grupet = _context.GrupiLenda.Where(x => x.Subject.Name == teacherSubject.Subject.Name).Select(x => new
				{
					Value = $"{x.Id}",
					Text = $"{x.Grupi.Emri} - {x.Subject.Name}"
				}).ToList();*/
			var grupet = _context.GrupiLenda
		.Select(x => new
		{
			Value = $"{x.Id}",
			Text = $"{x.Grupi.Emri} - {x.Subject.Name}"
		})
		.ToList();


			ViewData["GrupiLendaList"] = new SelectList(grupet, "Value", "Text");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
            return View();
        }
        //Krijo javen jo per here te pare
        // POST: Java/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddJavaVM javaVM)
        {

            var currentUser = await GetCurrentUserAsync();
            var email = currentUser.Email;
            if (!ModelState.IsValid)
            {
                return View(javaVM);
             
            }
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Email.Equals(email));
            /* var teacherId = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == javaVM.TeacherId);*/
        
            var grupiLendaId = await _context.GrupiLenda.FirstOrDefaultAsync(x => x.Id == javaVM.GrupiLendaId);

            var java = new Java()
            {
                JavaNumri = javaVM.JavaNumri,
                Data = DateTime.Now,
                Pjesemarrja = javaVM.Pjesemarrja,
                TeacherId = teacher.Id,
                GrupiLendaId = grupiLendaId.Id,
            };

            _context.Add(java);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lendet));
        }

        // GET: Java/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var currentUser = await GetCurrentUserAsync();
            var email = currentUser.Email;
            var week = await _context.Javet.FirstOrDefaultAsync(x=>x.Id== id);  
            var grupet = _context.GrupiLenda.Where(x=>x.Id==week.GrupiLendaId).Select(x => new
            {
                Value = $"{x.Id}",
                Text = $"{x.Grupi.Emri} - {x.Subject.Name}"
            }).ToList();
            ViewData["TeacherId"] = new SelectList(_context.Teachers.Where(x=>x.Email==email), "Id", "Email");
            ViewData["GrupiLendaList"] = new SelectList(grupet, "Value", "Text");
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if (id == null || _context.Javet == null)
            {
                return NotFound();
            }

            var java = await _context.Javet.FindAsync(id);
            if (java == null)
            {
                return NotFound();
            }
            return View(java);
        }

        // POST: Java/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditJavaVM java)
        {
            if (id != java.Id)
            {
                return NotFound();
            }
            var javaa = await _context.Javet.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    javaa.Id = java.Id;
                    javaa.GrupiLendaId= java.GrupiLendaId;
                    javaa.TeacherId = java.TeacherId;
                    javaa.Pjesemarrja = java.Pjesemarrja;
                    javaa.Data = DateTime.Now;
                    _context.Update(javaa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JavaExists(java.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Lendet));

			}
            return View(java);
        }

        // GET: Java/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Javet == null)
            {
                return NotFound();
            }

            var java = await _context.Javet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (java == null)
            {
                return NotFound();
            }

            return View(java);
        }

        // POST: Java/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Javet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Javet'  is null.");
            }
            var java = await _context.Javet.FindAsync(id);
            if (java != null)
            {
                _context.Javet.Remove(java);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lendet));
        }

       
        
        private bool JavaExists(int id)
        {
            return (_context.Javet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
