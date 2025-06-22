using GarageApplication.Enums;

namespace GarageApplication.Vehicles
{
    // Subclass for Car, inheriting from Vehicle and adding Model and Brand properties specific for cars.
    public class Car : Vehicle
    {
        public string Model { get; set; }
        public string Brand { get; set; }

        public Car(
            string registrationNumber,
            string color,
            int wheelAmount,
            int numberOfEngines,
            double cylinderVolume,
            FuelType fueltype,
            int numberOfSeats,
            double length,
            double weight,
            string model,
            string brand)
            : base(registrationNumber, color, wheelAmount, numberOfEngines, cylinderVolume, fueltype, numberOfSeats, length, weight)
        {
            Model = model;
            Brand = brand;
        }
    }
}