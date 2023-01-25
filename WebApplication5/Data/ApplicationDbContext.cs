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
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
			{
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}
		
		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<StudentTeacher> StudentTeacher { get; set; }
		public DbSet<Transkripta> Transkripta { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<SubjectTeacher> SubjectTeacher { get; set; }
		public DbSet<Afati> Afati { get; set; }
		public DbSet<Fakulteti> Fakultetet { get; set; }


	}
}