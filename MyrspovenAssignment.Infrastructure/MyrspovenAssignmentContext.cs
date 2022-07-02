using Microsoft.EntityFrameworkCore;

namespace MyrspovenAssignment.Infrastructure
{
    public class MyrspovenAssignmentContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }
        // public DbSet<Signal> Signals { get; set; }
        public DbSet<SignalData> SignalData { get; set; }
        public MyrspovenAssignmentContext(DbContextOptions<MyrspovenAssignmentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>()
            .Property(building => building.Id).ValueGeneratedNever();

            modelBuilder.Entity<SignalData>()
           .HasKey(t => new { t.BuildingId, t.Value, t.ReadUtc });
            base.OnModelCreating(modelBuilder);
        }


    }
}