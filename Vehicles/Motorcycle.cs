using GarageApplication.Enums;

namespace GarageApplication.Vehicles
{
    // Subclass for Motorcycle, inheriting from Vehicle and adding HasSidecar property specific for motorcycles.
    public class Motorcycle : Vehicle
    {
        public bool HasSidecar { get; set; }

        public Motorcycle(
            string registrationNumber,
            string color,
            int wheelAmount,
            int numberOfEngines,
            double cylinderVolume,
            FuelType fueltype,
            int numberOfSeats,
            double length,
            double weight,
            bool hasSidecar)
            : base(registrationNumber, color, wheelAmount, numberOfEngines, cylinderVolume, fueltype, numberOfSeats, length, weight)
        {
            HasSidecar = hasSidecar;
        }
    }
}