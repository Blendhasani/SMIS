using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class Teacher
	{
		[Key]
		public int Id { get; set; }
        [Display(Name = "Emri")]
        public string Name { get; set; }
        [Display(Name = "Mbiemri")]
        public string Surname { get; set; }
        [Display(Name = "Data e lindjes")]
        public DateTime BirthDay { get; set; }
   

        public string Email { get; set; }
        [Display(Name = "NrTel")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //pjeset e shtuara

        [Display(Name="Numri Personal")]
        public string NrPersonal { get; set; }

        [Display(Name="Titulli Shkencor")]
        public string TitulliShkencor { get; set; }

        [Display(Name="Angazhimi")]
        public string Angazhimi { get; set; }

        [Display(Name="Angazhuar në")]
        public string Angazhuar { get; set; }

        [Display(Name="Gjinia")]
        public Char Gender { get; set; }




        public List<SubjectTeacher> SubjectTeachers { get; set; }
		public List<StudentTeacher> StudentTeachers { get; set; }
		public int FakultetiId { get; set; }
		[ForeignKey("FakultetiId")]
		public Fakulteti Fakulteti { get; set; }
		public List<Java> Javet { get; set; }

	}
}
