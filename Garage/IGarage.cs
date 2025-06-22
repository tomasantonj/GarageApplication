using GarageApplication.Vehicles;
using System.Collections.Generic;

namespace GarageApplication.Garage
{
    // Public interface for a generic garage that can hold vehicles of type T, where T is a subclass of Vehicle.
    public interface IGarage<T> where T : Vehicle
    {
        int Capacity { get; }
        IReadOnlyCollection<T> Vehicles { get; }
        void AddVehicle(T vehicle);
        bool RemoveVehicle(T vehicle);
        T? FindVehicleByRegistration(string registrationNumber);
    }
}