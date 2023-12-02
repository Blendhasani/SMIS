using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class GrupiLenda
	{
		public int Id { get; set; }

		[ForeignKey("GrupiId")]
		public int GrupiId { get; set; }
		public Grupi Grupi { get; set; }

		[ForeignKey("SubjectId")]
		public int SubjectId { get; set; }

        public Subject Subject { get; set; }


		public List<Java> Javet { get; set; }
	}
}
