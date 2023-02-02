using FastReport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplication5.Data;
using WebApplication5.Models;

using Microsoft.AspNetCore.Http;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Areas.Admin.Controllers
{
	[Area("Admin")]
	/*[Authorize(Roles = "ADMIN")]*/
	public class ReportController : Controller
	{
		public ApplicationDbContext _context;
		public ReportController(ApplicationDbContext context)
		{
			_context = context;
		}
		[Route("Report/Index")]
		public IActionResult Index()
		{

            ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name");
            return View();
		}
		[Route("Report/GenerateStudent")]
		public FileResult GenerateStudent()
		{
			FastReport.Utils.Config.WebMode = true;
			Report rep = new Report();
			string path = Path.Combine("Students.frx");
			rep.Load(path);
			var students = _context.Students.OrderBy(s=>s.Name).ToList();
		/*	rep.SetParameterValue("parm1", "This is first Parameter");
			rep.SetParameterValue("parm2", "This is second Parameter");*/
			rep.RegisterData(students, "StudentsRef");

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
				return File(ms, "application/pdf", "studentet.pdf");

			}
			else
			{
				return null;
			}
		}

        [Route("Report/GenerateTeacher")]
        public FileResult GenerateTeacher()
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            string path = Path.Combine("Teachers.frx");
            rep.Load(path);
            var teachers = _context.Teachers.OrderBy(s=>s.Name).ToList();
         /*   rep.SetParameterValue("parm1", "This is first Parameter");
            rep.SetParameterValue("parm2", "This is second Parameter");*/
            rep.RegisterData(teachers, "TeachersRef");

            if (rep.Report.Prepare())
            {
                FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Subject Report";
                pdfExport.Title = "Profesoret";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                rep.Report.Export(pdfExport, ms);
                pdfExport.Dispose();
                ms.Position = 0;
                return File(ms, "application/pdf", "profesoret.pdf");

            }
            else
            {
                return null;
            }
        }


        [Route("Report/GenerateStudentByEmail")]
        [Route("Report/GenerateStudentByEmail/{searchString?}")]
        public FileResult GenerateStudentByEmail(string searchString)
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            string path = Path.Combine("Vertetimi.frx");
            rep.Load(path);
            var students = _context.Students.Where(s => s.Email == searchString).ToList();
            /*	rep.SetParameterValue("parm1", "This is first Parameter");
                rep.SetParameterValue("parm2", "This is second Parameter");*/
            rep.RegisterData(students, "VertetimiRef");

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
                return File(ms, "application/pdf", "vertetimi.pdf");

            }
            else
            {
                return null;
            }
        }
		[Authorize(Roles = "User")]

		[Route("Report/GenerateStudentById")]
        [Route("Report/GenerateStudentById/{id?}")]
        public FileResult GenerateStudentById(int id)
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            string path = Path.Combine("Vertetimi.frx");
            rep.Load(path);
            var students = _context.Students.Where(s => s.Id == id).ToList();
            /*	rep.SetParameterValue("parm1", "This is first Parameter");
                rep.SetParameterValue("parm2", "This is second Parameter");*/
            rep.RegisterData(students, "VertetimiRef");

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
                return File(ms, "application/pdf", "vertetimi.pdf");

            }
            else
            {
                return null;
            }
        }
        /*		public IActionResult GenderChart()
				{
					//Retrieve the data from the teachers table
					var teachers = _context.Teachers.ToList();

					//Count the number of male and female teachers
					int maleCount = teachers.Count(t => t.Gender == 'M');
					int femaleCount = teachers.Count(t => t.Gender == 'F');

					//Calculate the percentages
					double malePercentage = (double)maleCount / teachers.Count * 100;
					double femalePercentage = (double)femaleCount / teachers.Count * 100;

					//Create the data for the pie chart
					var chartData = new ChartData
					{
						Labels = new string[] { "Male", "Female" },
						Datasets = new ChartDataset[]
						{
						new ChartDataset
						{
							Data = new double[] { malePercentage, femalePercentage },
							BackgroundColor = new string[] { "rgba(255, 99, 132, 0.2)", "rgba(54, 162, 235, 0.2)" },
							BorderColor = new string[] { "rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)" },
						}
						}
					};

					//Pass the chart data to the view
					ViewBag.ChartData = chartData;

					return View();
				}*/
        /*        [Route("Report/GeneratePieChart")]
                public async Task<IActionResult> GeneratePieChart()
                {
                    var data = await _context.Teachers
                        .GroupBy(x => x.Gender)
                        .Select(x => new { Gender = x.Key, Count = x.Count() })
                        .ToListAsync();

                    // Convert data to Highcharts series format
                    var series = data.Select(d => new { name = d.Gender, y = d.Count });

                    // Create a new Highcharts configuration object

                    var chart = new Highcharts.Highcharts("pie")
                        .SetTitle(new Title { Text = "Gender Pie Chart" }).SetSeries(new Series { Data = new Data(series.ToArray()) });

                    return View(chart);
                }*/
        [Route("Report/GetData")]
        public ActionResult GetData()
        {
            int male = _context.Teachers.Count(t => t.Gender == 'M');
            int female = _context.Teachers.Count(t => t.Gender == 'F');

            Ratio obj = new Ratio();
            obj.Male = male;
            obj.Female = female;

            var jsonSerializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };
            return new JsonResult(obj, jsonSerializerOptions);
        }
        public class Ratio
        {
            public int Male { get; set; }
            public int Female { get; set; }
            
        }








		[Route("Report/StudentetNeFakultet")]
		[Route("Report/StudentetNeFakultet/{searchString?}")]
		public async Task<IActionResult> StudentetNeFakultet(string searchString)
		{

			var allStudents = _context.Students.Include(s => s.Fakulteti).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = allStudents.Where(n => n.Fakulteti.Emri.ToLower().Contains(searchString)).ToList();

				return View("Filtered", filterResults);
			}
			return View("Filtered", allStudents);
		}

		[Route("Report/Kombesite")]
		[Route("Report/Kombesite/{searchString?}")]
		public async Task<IActionResult> Kombesite(string searchString)
		{

			var allStudents = _context.Students.Include(s => s.Nationality).Include(s=>s.Fakulteti).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = allStudents.Where(n => n.Nationality.Name.Contains(searchString)).ToList();

				return View("Filtered", filterResults);
			}
			return View("Filtered", allStudents);
		}

		[Route("Report/StudentByEmail")]
		[Route("Report/StudentByEmail/{searchString?}")]
		public async Task<IActionResult> StudentByEmail(string searchString)
		{

			var allStudents = _context.Students.Include(s => s.Fakulteti).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = allStudents.Where(n => n.Email.Contains(searchString)).ToList();

				return View("Filtered", filterResults);
			}
			return View("Filtered", allStudents);
		}


		[Route("Report/TeacherByEmail")]
		[Route("Report/TeacherByEmail/{searchString?}")]
		public async Task<IActionResult> TeacherByEmail(string searchString)
		{

			var teachers = _context.Teachers.Include(s => s.Fakulteti).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = teachers.Where(n => n.Email.Contains(searchString)).ToList();

				return View("FilteredTeachers", filterResults);
			}
			return View("FilteredTeachers", teachers);
		}

		[Route("Report/SubjectByName")]
		[Route("Report/SubjectByName/{searchString?}")]
		public async Task<IActionResult> SubjectByName(string searchString)
		{

			var subjects = _context.Subjects.Include(s => s.Fakulteti).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = subjects.Where(n => n.Name.ToLower().Contains(searchString) || n.NameTranslated.ToLower().Contains(searchString)).ToList();

				return View("FilteredSubjectsNames", filterResults);
			}
			return View("FilteredSubjectsNames", subjects);
		}

		[Route("Report/TranskriptaEStudentit")]
        [Route("Report/TranskriptaEStudentit/{searchString?}")]
        public async Task<IActionResult> TranskriptaEStudentit(string searchString)
        {

            var applicationDbContext = _context.Transkripta.Include(t => t.Student).Include(t => t.Subject);
          
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResults = applicationDbContext.Where(n => n.Student.Email.ToLower().Contains(searchString)).ToList();

                return View("FilteredTranskripta", filterResults);
            }
            return View("FilteredTranskripta", applicationDbContext);
        }

		[Route("Report/Angazhimet")]
		[Route("Report/Angazhimet/{searchString?}")]
		public async Task<IActionResult> Angazhimet(string searchString)
		{

            var applicationDbContext = _context.Teachers.Include(s=>s.Fakulteti);

			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = applicationDbContext.Where(n => n.Angazhimi==searchString).ToList();

				return View("FilteredAngazhimet", filterResults);
			}
			return View("FilteredAngazhimet", applicationDbContext);
		}


		[Route("Report/ProfStudentsByEmail")]
		[Route("Report/ProfStudentsByEmail/{searchString?}")]
		public async Task<IActionResult> ProfStudentsByEmail(string searchString)
		{
	
			var applicationDbContext = _context.StudentTeacher.Include(s => s.Student).Include(s => s.Teacher).Include(s=>s.Student.Fakulteti).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = applicationDbContext.Where(n => n.Teacher.Email.Equals(searchString)).ToList();

				return View("FilteredStudents", filterResults);
			}
			return View("FilteredStudents", applicationDbContext);
		}


		[Route("Report/ProfSubjectByEmail")]
		[Route("Report/ProfSubjectByEmail/{searchString?}")]
		public async Task<IActionResult> ProfSubjectByEmail(string searchString)
		{

			var applicationDbContext = _context.SubjectTeacher.Include(s => s.Subject).Include(s => s.Teacher).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResults = applicationDbContext.Where(n => n.Teacher.Email.Equals(searchString)).ToList();

				return View("FilteredSubjects", filterResults);
			}
			return View("FilteredSubjects", applicationDbContext);
		}
	}
}
