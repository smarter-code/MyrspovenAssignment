namespace MyrspovenAssignment.Infrastructure
{
    public class Building
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public DateTime DataSetStartDate { get; set; }
        public int squaredMeters { get; set; }
        public ICollection<SignalData> SignalData { get; set; }
    }
}