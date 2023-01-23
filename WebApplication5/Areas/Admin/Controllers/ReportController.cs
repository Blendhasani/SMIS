using FastReport;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Areas.Admin.Controllers
{
	[Area("Admin")]
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
    }
}
