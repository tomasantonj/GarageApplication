using GarageApplication.Enums;

// Subclass for Boat, inheriting from Vehicle and adding BoatType property only for boats.
namespace GarageApplication.Vehicles
{
    public class Boat : Vehicle
    {
        public string BoatType { get; set; }

        public Boat(
            string registrationNumber,
            string color,
            int wheelAmount,
            int numberOfEngines,
            double cylinderVolume,
            FuelType fueltype,
            int numberOfSeats,
            double length,
            double weight,
            string boatType)
            : base(registrationNumber, color, wheelAmount, numberOfEngines, cylinderVolume, fueltype, numberOfSeats, length, weight)
        {
            BoatType = boatType;
        }
    }
}