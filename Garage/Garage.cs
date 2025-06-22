using GarageApplication.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GarageApplication.Garage
{
    internal class Garage<T> : IGarage<T>, IEnumerable<T> where T : Vehicle
    {
        private readonly List<T> _vehicles;
        public int Capacity { get; }

        public Garage(int capacity = 20)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

            Capacity = capacity;
            _vehicles = new List<T>(capacity);
        }

        public IReadOnlyCollection<T> Vehicles => _vehicles.AsReadOnly();

        public void AddVehicle(T vehicle)
        {
            if (Vehicles.Count >= Capacity)
                throw new InvalidOperationException("Garage is full.");
            _vehicles.Add(vehicle);
        }

        public bool RemoveVehicle(T vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            return _vehicles.Remove(vehicle);
        }

        public T? FindVehicleByRegistration(string registrationNumber)
        {
            return _vehicles.Find(v => v.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        }

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
