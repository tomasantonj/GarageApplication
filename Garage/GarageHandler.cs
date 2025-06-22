using System;
using System.Collections.Generic;
using System.Linq;
using GarageApplication.Enums;
using GarageApplication.Vehicles;

namespace GarageApplication.Garage
{
    internal class GarageHandler
    {
        private Garage<Vehicle> _garage;

        public GarageHandler(int capacity = 20)
        {
            _garage = new Garage<Vehicle>(capacity);
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));
            try
            {
                _garage.AddVehicle(vehicle);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false; // Garage is full
            }
        }

        public bool RemoveVehicle(string registrationNumber)
        {
            var vehicle = FindVehicle(registrationNumber);
            if (vehicle == null) return false;
            return _garage.RemoveVehicle(vehicle);
        }

        public Vehicle? FindVehicle(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber)) return null;
            return _garage.FindVehicleByRegistration(registrationNumber);
        }

        public IEnumerable<Vehicle> ListVehicles(Type? vehicleType = null)
        {
            if (vehicleType == null || vehicleType == typeof(Vehicle))
                return _garage.Vehicles;

            return _garage.Vehicles.Where(v => v.GetType() == vehicleType);
        }

        /// <summary>
        /// Finds vehicles matching the given properties. If vehicleType is specified, only vehicles of that type (or derived type) are returned.
        /// </summary>
        /// <param name="color">Color to match (case-insensitive), or null to ignore.</param>
        /// <param name="wheelAmount">Number of wheels to match, or null to ignore.</param>
        /// <param name="fuelType">Fuel type to match, or null to ignore.</param>
        /// <param name="numberOfSeats">Number of seats to match, or null to ignore.</param>
        /// <param name="vehicleType">Type of vehicle to match (e.g., typeof(Motorcycle)), or null to ignore.</param>
        /// <returns>IEnumerable of matching vehicles.</returns>
        public IEnumerable<Vehicle> FindVehicleByVehicleProperties(
            string? color = null,
            int? wheelAmount = null,
            FuelType? fuelType = null,
            int? numberOfSeats = null,
            Type? vehicleType = null)
        {
            return _garage.Vehicles.Where(v =>
                (vehicleType == null || vehicleType.IsAssignableFrom(v.GetType())) &&
                (color == null || v.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&
                (wheelAmount == null || v.WheelAmount == wheelAmount) &&
                (fuelType == null || v.Fueltype == fuelType) &&
                (numberOfSeats == null || v.NumberOfSeats == numberOfSeats)
            );
        }

        // Function that searches the garage for vehicles based on type and returns a list
        public List<(string Type, int Count)> GetVehicleType()
        {
            return _garage.Vehicles
                .GroupBy(v => v.GetType().Name)
                .Select(g => (g.Key, g.Count()))
                .ToList();
        }

        // SetCapacity method allows changing the garage's capacity 
        public bool SetCapacity(int newCapacity)
        {
            // Prevent changing capacity if vehicles are present
            if (_garage.Vehicles.Count > 0)
                return false; 

            // Recreate the garage with the new capacity
            _garage = new Garage<Vehicle>(newCapacity);
            return true;
        }
    }
}
