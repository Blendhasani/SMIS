using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Teacher
	{
		[Key]
		public int Id { get; set; }
        [Display(Name = "Emri")]
        public string Name { get; set; }
        [Display(Name = "Mbiemri")]
        public string Surname { get; set; }
        [Display(Name = "Data e lindjes")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Specializimi")]
        public string Grade { get; set; }

        public string Email { get; set; }
        [Display(Name = "NrTel")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<SubjectTeacher> SubjectTeachers { get; set; }
		public List<StudentTeacher> StudentTeachers { get; set; }
	
	}
}
