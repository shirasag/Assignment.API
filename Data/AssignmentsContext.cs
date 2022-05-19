using Microsoft.EntityFrameworkCore;

namespace Assignment.API
{
    public class AssignmentsContext : DbContext
    {
        public AssignmentsContext(DbContextOptions<AssignmentsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }       
        public DbSet<Assignment> Assignments { get; set; }

    }
}
