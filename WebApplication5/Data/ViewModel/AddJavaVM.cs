using System.ComponentModel.DataAnnotations.Schema;
using WebApplication5.Models;

namespace WebApplication5.Data.ViewModel
{
    public class AddJavaVM
    {
       


        public int GrupiLendaId { get; set; }
  

        public int TeacherId { get; set; }
        public int JavaNumri { get; set; }
        public string Pjesemarrja { get; set; }

        public DateTime Data { get; set; }
    }
}
