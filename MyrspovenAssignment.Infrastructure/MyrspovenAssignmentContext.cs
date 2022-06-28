using Microsoft.EntityFrameworkCore;

namespace MyrspovenAssignment.Infrastructure
{
    public class MyrspovenAssignmentContext : DbContext
    {

        public MyrspovenAssignmentContext(DbContextOptions<MyrspovenAssignmentContext> options) : base(options)
        {
        }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<SignalData> SignalData { get; set; }
    }
}