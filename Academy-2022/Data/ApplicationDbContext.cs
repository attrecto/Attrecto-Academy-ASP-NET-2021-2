using Academy_2022.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy_2022.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Courses)
                .WithMany(u => u.Students)
                .UsingEntity<CourseUser>(
                    j => j
                        .HasOne(uc => uc.Course)
                        .WithMany(c => c.CourseUsers)
                        .HasForeignKey(uc => uc.CourseId),
                    j => j
                        .HasOne(uc => uc.User)
                        .WithMany(u => u.CourseUsers)
                        .HasForeignKey(uc => uc.UserId),
                    j => j.HasKey(t => new { t.UserId, t.CourseId })
                );
        }
    }
}
