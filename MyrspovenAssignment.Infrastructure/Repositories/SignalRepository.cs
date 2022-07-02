namespace MyrspovenAssignment.Infrastructure.Repositories
{
    public class SignalRepository : GenericRepository<Signal>
    {
        public SignalRepository(MyrspovenAssignmentContext context) : base(context)
        {
        }
    }
}
