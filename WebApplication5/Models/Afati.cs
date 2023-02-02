

using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Afati
    {
        [Key]
        public int Id { get; set; }
        public string Emri { get; set; }
        public bool Hapur { get; set; }
        public int NrProvimeve { get; set; }

        public string VitiAkademik { get; set; }
/*        public List<Transkripta> Transkriptas { get; set; }*/
    }
}
