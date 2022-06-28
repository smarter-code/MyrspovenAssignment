namespace MyrspovenAssignment.Infrastructure
{
    public class SignalData
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public double Value { get; set; }
        public DateTime? DataUtc { get; set; }
        public DateTime? ReadUtc { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
    }
}