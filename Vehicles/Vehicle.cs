using GarageApplication.Enums;
using System;

namespace GarageApplication.Vehicles
{
    public class Vehicle
    {
        private string _registrationNumber = string.Empty;

        // Validation for RegistrationNumber to follow ABC:123 format
        public string RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                if (value == null || value.Length != 6)
                    throw new ArgumentException("Registration number must be 6 characters long (3 letters followed by 3 digits).", nameof(RegistrationNumber));

                // Check first 3 are letters
                for (int i = 0; i < 3; i++)
                {
                    if (!char.IsLetter(value[i]))
                        throw new ArgumentException("Registration number must start with 3 letters.", nameof(RegistrationNumber));
                }
                // Check last 3 are digits
                for (int i = 3; i < 6; i++)
                {
                    if (!char.IsDigit(value[i]))
                        throw new ArgumentException("Registration number must end with 3 digits.", nameof(RegistrationNumber));
                }

                _registrationNumber = value.ToUpper();
            }
        }
        public string Color { get; set; }
        public int WheelAmount { get; set; }
        public int NumberOfEngines { get; set; }
        public double CylinderVolume { get; set; }
        public FuelType Fueltype { get; set; }
        public int NumberOfSeats { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }

        public Vehicle(
            string registrationNumber,
            string color,
            int wheelAmount,
            int numberOfEngines,
            double cylinderVolume,
            FuelType fueltype,
            int numberOfSeats,
            double length,
            double weight)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            WheelAmount = wheelAmount;
            NumberOfEngines = numberOfEngines;
            CylinderVolume = cylinderVolume;
            Fueltype = fueltype;
            NumberOfSeats = numberOfSeats;
            Length = length;
            Weight = weight;
        }
    }
}