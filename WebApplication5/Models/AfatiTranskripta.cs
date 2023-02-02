using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class AfatiTranskripta
    {
        public int Id { get; set; }
        public int AfatiId { get; set; }
        [ForeignKey("AfatiId")]
        public Afati Afati { get; set; }


        public int TranskriptaId { get; set; }
        [ForeignKey("TranskriptaId")]
        public Transkripta Transkripta { get; set; }
    }
}
