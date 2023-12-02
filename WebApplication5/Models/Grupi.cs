using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class Grupi
	{
		public int Id { get; set; }

		public string Emri { get; set; }


		public List<GrupiLenda> GrupiLenda { get; set; }
		public List<GrupiStudenti> GrupiStudenti { get; set; }


	}
}
