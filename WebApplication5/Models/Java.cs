using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class Java
	{
		public int Id { get; set; }


		public int GrupiLendaId { get; set; }
		[ForeignKey("GrupiLendaId")]
		public GrupiLenda GrupiLenda {get;set;}
		public int TeacherId { get; set; }
		[ForeignKey("TeacherId")]
        public Teacher Teacher {get;set;}
        public int JavaNumri { get; set; }
		public string Pjesemarrja { get; set; }

		public DateTime Data { get; set; }
	}
}
