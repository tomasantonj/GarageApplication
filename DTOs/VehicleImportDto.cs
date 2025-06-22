namespace GarageApplication.DTOs
{
    public class VehicleImportDto
    {
        // Import common Vehicle properties
        public string Type { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int WheelAmount { get; set; }
        public int NumberOfEngines { get; set; }
        public double CylinderVolume { get; set; }
        public string Fueltype { get; set; }
        public int NumberOfSeats { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        // Importan Subclass-specific properties
        public string Model { get; set; }
        public string Brand { get; set; }
        public bool? HasSidecar { get; set; }
        public double? WingSpan { get; set; }
        public bool? HasJetEngines { get; set; }
        public bool? IsDoubleDecker { get; set; }
        public string BoatType { get; set; }
    }
}