using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Areas.Admin.Models
{
	public class UsersRoles
	{
		public IdentityUser User { get; set; }
		public IList<string> Roles { get; set; }
	}
}
