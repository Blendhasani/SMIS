using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<StudentTeacher> StudentTeacher { get; set; }
		public DbSet<Transkripta> Transkripta { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<SubjectTeacher> SubjectTeacher { get; set; }
	}
}