using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Transkripta
	{
		[Key]

		public int Id { get; set; }

		public int? Nota { get; set; }

		public int StudentId { get; set; }
		[ForeignKey("StudentId")]

		public Student Student { get; set; }

		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public Subject Subject { get; set; }

		public DateTime CreatedDate { get; set; }



		

	}
}
