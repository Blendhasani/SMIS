using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
		[Display(Name = "Shteti")]
		public string Name { get; set; }
        public string Kodi { get; set; }
       
        public List<Student> Students { get; set; }

        

    }
}
