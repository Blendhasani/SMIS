using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Student
    {
		[Key]
		public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string ParentName { get; set; }

        public Char Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Residence { get; set; }

        public string Nationality { get; set; }

        public string State { get; set; }

        public string Phone { get; set; }
        [Display(Name="Profile Picture")]

        public string ImageUrl { get; set; }

        public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	
	
		public List<StudentTeacher> StudentTeachers { get; set; }
		public List<Transkripta> Transkripta { get; set; }
	}
}
