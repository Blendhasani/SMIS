
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
		[Display(Name = "Kombësia")]
		public string Name { get; set; }
        public string Kodi { get; set; }
     
        public List<Student> Students { get; set; }
    }
}
