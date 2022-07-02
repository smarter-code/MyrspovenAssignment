namespace MyrspovenAssignment.ViewModels
{
    public class BuildingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string DataSetStartDate { get; set; }
        public int squaredMeters { get; set; }
        public ICollection<SignalDataViewModel> SignalsData { get; set; }
        public ICollection<SignalViewModel> Signals { get; set; }

    }
}