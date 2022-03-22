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
    }
}
