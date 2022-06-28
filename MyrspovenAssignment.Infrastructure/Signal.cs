namespace MyrspovenAssignment.Infrastructure
{
    public class Signal
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public int ExternalBuildingId { get; set; }
        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public string Key { get; set; }
        public string Rw { get; set; }
        public string Bms { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
    }
}