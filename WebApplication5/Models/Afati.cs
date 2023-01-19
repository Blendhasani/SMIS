

using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Afati
    {
        [Key]
        public int Id { get; set; }
        public bool Hapur { get; set; }
        public string Rregullt { get; set; }
        public List<Transkripta> Transkriptas { get; set; }
    }
}
