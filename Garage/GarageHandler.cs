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
        // Constructor that initializes the garage with a specified capacity, default is 20
        public GarageHandler(int capacity = 20)
        {
            _garage = new Garage<Vehicle>(capacity);
        }
        // Allows adding a vehicle to the garage, returns true if successful, false if garage full
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
                return false; 
            }
        }
        // method that removes a vehicle from the garage by its registration number
        public bool RemoveVehicle(string registrationNumber)
        {
            var vehicle = FindVehicle(registrationNumber);
            if (vehicle == null) return false;
            return _garage.RemoveVehicle(vehicle);
        }
        // method that finds vehicle in garage by its registration number
        public Vehicle? FindVehicle(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber)) return null;
            return _garage.FindVehicleByRegistration(registrationNumber);
        }
        // method that lists all vehicles in the garage, optionally filtered by type
        public IEnumerable<Vehicle> ListVehicles(Type? vehicleType = null)
        {
            if (vehicleType == null || vehicleType == typeof(Vehicle))
                return _garage.Vehicles;

            return _garage.Vehicles.Where(v => v.GetType() == vehicleType);
        }

    
        // Finds vehicles matching the given properties. If vehicleType is specified, only vehicles of that type are returned
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
