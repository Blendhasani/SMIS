using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class Residence
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Vendbanimi")]
        public string Name { get; set; }
        public string Kodi { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]

        public State State { get; set; }

        public List<Student> Students { get; set; }


    }
}
