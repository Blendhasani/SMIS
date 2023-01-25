using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
	public class Student
    {
		[Key]
		public int Id { get; set; }
        [Display(Name ="Emri")]
        public string Name { get; set; }
        [Display(Name = "Mbiemri")]
        public string Surname { get; set; }
        [Display(Name = "Emri i prindit")]
        public string ParentName { get; set; }
        [Display(Name = "Gjinia")]
        public Char Gender { get; set; }
        [Display(Name = "Data e lindjes")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Vendlindja")]
        public string Residence { get; set; }
        [Display(Name = "Kombësia")]
        public string Nationality { get; set; }
        [Display(Name = "Shteti")]
        public string State { get; set; }
        [Display(Name = "NrTel")]
        public string Phone { get; set; }
        [Display(Name="Profile Picture")]

        public string ImageUrl { get; set; }

        public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]

        public string Password { get; set; }

	
	
		public List<StudentTeacher> StudentTeachers { get; set; }
		public List<Transkripta> Transkripta { get; set; }
        
        public int FakultetiId { get; set; }
        [ForeignKey("FakultetiId")]
        public Fakulteti Fakulteti { get; set; }
    }
}
