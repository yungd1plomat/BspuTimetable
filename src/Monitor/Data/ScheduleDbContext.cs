using Microsoft.EntityFrameworkCore;
using Parser.Models;

namespace Parser.Data
{
    public class ScheduleDbContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
            : base(options)
        {

        }
    }
}
