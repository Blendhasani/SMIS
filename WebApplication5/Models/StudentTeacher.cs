using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class StudentTeacher
	{
		public int Id { get; set; }

		public int StudentId { get; set; }
		[ForeignKey("StudentId")]
		public Student Student { get; set; }


		public int TeacherId { get; set; }
		[ForeignKey("TeacherId")]
		public Teacher Teacher { get; set; }

	}
}
