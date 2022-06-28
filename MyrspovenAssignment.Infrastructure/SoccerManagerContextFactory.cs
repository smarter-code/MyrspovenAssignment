using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyrspovenAssignment.Infrastructure
{
    public class SoccerManagerContextFactory : IDesignTimeDbContextFactory<MyrspovenAssignmentContext>
    {

        public MyrspovenAssignmentContext CreateDbContext(string[] args)
        {

            var connectionString = ConfigurationHelper.ReadJsonConfig("DefaultConnection",
                Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var optionsBuilder = new DbContextOptionsBuilder<MyrspovenAssignmentContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MyrspovenAssignmentContext(optionsBuilder.Options);
        }
    }
}
