namespace MyrspovenAssignment.Infrastructure.Repositories
{
    public class BuildingRepository : GenericRepository<Building>
    {
        public BuildingRepository(MyrspovenAssignmentContext context) : base(context)
        {
        }
    }
}
