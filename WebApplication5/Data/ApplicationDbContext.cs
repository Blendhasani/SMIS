using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Migrations;
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
			foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
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
		public DbSet<AfatiTranskripta> AfatiTranskripta { get; set; }
        public DbSet<Fakulteti> Fakultetet { get; set; }

		public DbSet<State> States { get; set; }
		//public DbSet<Residence> Residences { get; set; }

		public DbSet<Nationality> Nationalities { get; set; }
		public DbSet<Grupi> Grupet { get; set; }
		public DbSet<GrupiStudenti> GrupiStudentet { get; set; }
		public DbSet<Java> Javet { get; set; }
		public DbSet<WebApplication5.Models.GrupiLenda>? GrupiLenda { get; set; }
	

	}
}