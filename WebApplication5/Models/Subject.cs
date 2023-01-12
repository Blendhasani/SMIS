using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

        public int Semester { get; set; }

        public int ECTS { get; set; }
        public List<SubjectTeacher> SubjectTeachers { get; set; }
		public List<Transkripta> Transkripta { get; set; }
	}
}
