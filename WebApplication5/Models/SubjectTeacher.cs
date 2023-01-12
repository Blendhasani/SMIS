using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class SubjectTeacher
	{
		public int Id { get; set; }

		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public Subject Subject { get; set; }


		public int TeacherId { get; set; }
		[ForeignKey("TeacherId")]
		public Teacher Teacher { get; set; }

	
	}
}
