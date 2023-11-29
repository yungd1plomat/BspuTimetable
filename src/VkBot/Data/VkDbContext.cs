using Microsoft.EntityFrameworkCore;
using VkBot.Models;

namespace VkBot.Data
{
    public class VkDbContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public VkDbContext(DbContextOptions<VkDbContext> options)
            : base(options)
        {
        }
    }
}
