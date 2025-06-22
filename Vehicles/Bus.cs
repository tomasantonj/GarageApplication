using GarageApplication.Enums;

namespace GarageApplication.Vehicles
{
    // Subclass for Bus, inheriting from Vehicle and adding IsDoubleDecker property specific for buses.
    public class Bus : Vehicle
    {
        public bool IsDoubleDecker { get; set; }

        public Bus(
            string registrationNumber,
            string color,
            int wheelAmount,
            int numberOfEngines,
            double cylinderVolume,
            FuelType fueltype,
            int numberOfSeats,
            double length,
            double weight,
            bool isDoubleDecker)
            : base(registrationNumber, color, wheelAmount, numberOfEngines, cylinderVolume, fueltype, numberOfSeats, length, weight)
        {
            IsDoubleDecker = isDoubleDecker;
        }
    }
}