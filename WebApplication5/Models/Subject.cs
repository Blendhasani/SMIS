using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

        [Display(Name = "Emërtimi")]
        public string Name { get; set; }
        [Display(Name = "Semestri")]
        public int Semester { get; set; }
        [Display(Name = "ECTS")]
        public int ECTS { get; set; }
		//shtesat
		[Display(Name = "Kodi")]
		public string Kodi { get; set; }
		[Display(Name = "Emërtimi anglisht")]
		public string NameTranslated { get; set; }
		[Display(Name = "Kohëzgjatja")]
		public string KoheZgjatja { get; set; }

		[Display(Name = "Kategoria")]
		public string Kategoria { get; set; }
		[Display(Name = "Gjuha e Ligjërimit")]
		public string GjuhaLigjerimit { get; set; }
        public List<SubjectTeacher> SubjectTeachers { get; set; }
		public List<Transkripta> Transkripta { get; set; }
	}
}
