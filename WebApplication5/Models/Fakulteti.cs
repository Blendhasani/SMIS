using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Fakulteti
	{
		[Key]
		public int Id { get; set; }

		
		[Display(Name = "Kodi")]
		public string Kodi { get; set; }

		
		[Display(Name = "Fakulteti")]
		public string Emri { get; set; }

		
		[Display(Name = "Shkurtesa")]
		public string Shkurtesa { get; set; }

		public List<Student> Students { get; set; }

		public List<Teacher> Teachers { get; set; }

		public List<Subject> Subjects { get; set; }


	}
}
