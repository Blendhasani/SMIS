using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Teacher
	{
		[Key]
		public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDay { get; set; }

        public string Grade { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<SubjectTeacher> SubjectTeachers { get; set; }
		public List<StudentTeacher> StudentTeachers { get; set; }
	
	}
}
