using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class GrupiStudenti
	{
		public int Id { get; set; }

		[ForeignKey("GrupiId")]
		public int GrupiId { get; set; }
        public Grupi Grupi { get; set; }

        [ForeignKey("StudentId")]
		public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
