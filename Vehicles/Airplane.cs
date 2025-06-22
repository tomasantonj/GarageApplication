using GarageApplication.Enums;

namespace GarageApplication.Vehicles
{
    // Subclass for Airplane, inheriting from Vehicle adding WingSpan and HasJetEngines properties specific for airplanes.
    public class Airplane : Vehicle
    {
        public double WingSpan { get; set; }
        public bool HasJetEngines { get; set; }

        public Airplane(
            string registrationNumber,
            string color,
            int wheelAmount,
            int numberOfEngines,
            double cylinderVolume,
            FuelType fueltype,
            int numberOfSeats,
            double length,
            double weight,
            double wingSpan,
            bool hasJetEngines)
            : base(registrationNumber, color, wheelAmount, numberOfEngines, cylinderVolume, fueltype, numberOfSeats, length, weight)
        {
            WingSpan = wingSpan;
            HasJetEngines = hasJetEngines;
        }
    }
}