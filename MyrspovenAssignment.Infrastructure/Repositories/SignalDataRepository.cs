namespace MyrspovenAssignment.Infrastructure.Repositories
{
    public class SignalDataRepository : GenericRepository<SignalData>
    {
        public SignalDataRepository(MyrspovenAssignmentContext context) : base(context)
        {
        }
    }
}
