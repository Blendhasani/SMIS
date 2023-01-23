using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
