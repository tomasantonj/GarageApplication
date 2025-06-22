using GarageApplication.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GarageApplication.Garage
{
    // Garage class that uses IEnumerable for iteration and implements IGarage interface
    internal class Garage<T> : IGarage<T>, IEnumerable<T> where T : Vehicle
    {
        private readonly List<T> _vehicles;
        public int Capacity { get; }
        // Garage with a default capacity of 20, can be set to a different value
        public Garage(int capacity = 20)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

            Capacity = capacity;
            _vehicles = new List<T>(capacity);
        }

        public IReadOnlyCollection<T> Vehicles => _vehicles.AsReadOnly();
        // Adds a vehicle to the garage, throws if full
        public void AddVehicle(T vehicle)
        {
            if (Vehicles.Count >= Capacity)
                throw new InvalidOperationException("Garage is full.");
            _vehicles.Add(vehicle);
        }
        // Removes a vehicle from the garage, returns true if successful
        public bool RemoveVehicle(T vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            return _vehicles.Remove(vehicle);
        }
        // Finds a vehicle by its registration number, returns null if not found
        public T? FindVehicleByRegistration(string registrationNumber)
        {
            return _vehicles.Find(v => v.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        }
        // Returns an enumerator that iterates through vehicles
        public IEnumerator<T> GetEnumerator()
        {
            return _vehicles.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
