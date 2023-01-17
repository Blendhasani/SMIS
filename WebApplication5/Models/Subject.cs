using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

        [Display(Name = "Lenda")]
        public string Name { get; set; }
        [Display(Name = "Semestri")]
        public int Semester { get; set; }
        [Display(Name = "ECTS")]
        public int ECTS { get; set; }
        public List<SubjectTeacher> SubjectTeachers { get; set; }
		public List<Transkripta> Transkripta { get; set; }
	}
}
