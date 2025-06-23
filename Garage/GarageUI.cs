using System;
using System.IO;
using GarageApplication.Enums;
using GarageApplication.Vehicles;
using GarageApplication.DTOs;
using GarageApplication.Handlers;

namespace GarageApplication.Garage
{
    internal class GarageUI
    {
        private readonly GarageHandler _garageHandler;

        public GarageUI(int capacity = 20)
        {
            _garageHandler = new GarageHandler(capacity);
        }
        // main method that runs the garage UI, displaying the menu and handling user input
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Garage Menu ---");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Remove Vehicle");
                Console.WriteLine("3. Find Vehicle by Registration Number");
                Console.WriteLine("4. List All Vehicles");
                Console.WriteLine("5. Find Vehicles by Properties");
                Console.WriteLine("6. Import Test Data");
                Console.WriteLine("7. Show Vehicle types");
                Console.WriteLine("8. Create New Garage");
                Console.WriteLine("9. Reset Garage (Remove All Vehicles)");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddVehicleUI();
                        break;
                    case "2":
                        RemoveVehicleUI();
                        break;
                    case "3":
                        FindVehicleUI();
                        break;
                    case "4":
                        ListVehiclesUI();
                        break;
                    case "5":
                        FindVehiclesByPropertiesUI();
                        break;
                    case "6":
                        ImportTestDataUI();
                        break;
                    case "7":
                        ShowVehicleTypes();
                        break;
                    case "8":
                        CreateNewGarageUI();
                        break;
                    case "9":
                        ResetGarageUI();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        // UI function for adding vehicles to the garage
        private void AddVehicleUI()
        {
            Console.WriteLine("\n--- Add Vehicle ---");
            Console.WriteLine("Select vehicle type:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Airplane");
            Console.WriteLine("4. Bus");
            Console.WriteLine("5. Boat");

            Console.Write("Choice: ");
            var typeChoice = Console.ReadLine();

            // Common properties
            Console.Write("Registration Number: ");
            var reg = Console.ReadLine() ?? "";
            Console.Write("Color: ");
            var color = Console.ReadLine() ?? "";
            Console.Write("Wheel Amount: ");
            int.TryParse(Console.ReadLine(), out int wheels);
            Console.Write("Number of Engines: ");
            int.TryParse(Console.ReadLine(), out int engines);
            Console.Write("Cylinder Volume: ");
            double.TryParse(Console.ReadLine(), out double cylVol);
            Console.Write("Fuel Type (Gasoline/Diesel): ");
            var fuelInput = Console.ReadLine() ?? "";
            FuelType fuel = fuelInput.Equals("Diesel", StringComparison.OrdinalIgnoreCase) ? FuelType.Diesel : FuelType.Gasoline;
            Console.Write("Number of Seats: ");
            int.TryParse(Console.ReadLine(), out int seats);
            Console.Write("Length: ");
            double.TryParse(Console.ReadLine(), out double length);
            Console.Write("Weight: ");
            double.TryParse(Console.ReadLine(), out double weight);

            Vehicle vehicle = null;
            // If a specific vehicle type is chosen, we allow to set the subclass-specific properties
            switch (typeChoice)
            {
                case "1": // Car
                    Console.Write("Model: ");
                    var model = Console.ReadLine() ?? "";
                    Console.Write("Brand: ");
                    var brand = Console.ReadLine() ?? "";
                    vehicle = new Car(reg, color, wheels, engines, cylVol, fuel, seats, length, weight, model, brand);
                    break;
                case "2": // Motorcycle
                    Console.Write("Has Sidecar (yes/no): ");
                    var sidecarInput = Console.ReadLine() ?? "";
                    bool hasSidecar = sidecarInput.Equals("yes", StringComparison.OrdinalIgnoreCase);
                    vehicle = new Motorcycle(reg, color, wheels, engines, cylVol, fuel, seats, length, weight, hasSidecar);
                    break;
                case "3": // Airplane
                    Console.Write("Wing Span: ");
                    double.TryParse(Console.ReadLine(), out double wingSpan);
                    Console.Write("Has Jet Engines (yes/no): ");
                    var jetInput = Console.ReadLine() ?? "";
                    bool hasJetEngines = jetInput.Equals("yes", StringComparison.OrdinalIgnoreCase);
                    vehicle = new Airplane(reg, color, wheels, engines, cylVol, fuel, seats, length, weight, wingSpan, hasJetEngines);
                    break;
                case "4": // Bus
                    Console.Write("Is Double Decker (yes/no): ");
                    var doubleDeckerInput = Console.ReadLine() ?? "";
                    bool isDoubleDecker = doubleDeckerInput.Equals("yes", StringComparison.OrdinalIgnoreCase);
                    vehicle = new Bus(reg, color, wheels, engines, cylVol, fuel, seats, length, weight, isDoubleDecker);
                    break;
                case "5": // Boat
                    Console.Write("Boat Type: ");
                    var boatType = Console.ReadLine() ?? "";
                    vehicle = new Boat(reg, color, wheels, engines, cylVol, fuel, seats, length, weight, boatType);
                    break;
                default:
                    Console.WriteLine("Invalid vehicle type selected.");
                    return;
            }

            if (_garageHandler.AddVehicle(vehicle))
                Console.WriteLine("Vehicle added successfully.");
            else
                Console.WriteLine("Failed to add vehicle (garage may be full).");
        }
        // UI function that removes a vehicle from the garage by its registration number
        private void RemoveVehicleUI()
        {
            Console.Write("\nEnter registration number to remove: ");
            var reg = Console.ReadLine() ?? "";
            if (_garageHandler.RemoveVehicle(reg))
                Console.WriteLine("Vehicle removed.");
            else
                Console.WriteLine("Vehicle not found.");
        }
        // UI function that finds a vehicle by its registration number and prints its details
        private void FindVehicleUI()
        {
            Console.Write("\nEnter registration number to find: ");
            var reg = Console.ReadLine() ?? "";
            var vehicle = _garageHandler.FindVehicle(reg);
            if (vehicle != null)
                PrintVehicle(vehicle);
            else
                Console.WriteLine("Vehicle not found.");
        }
        // UI function that lists all vehicles in the garage, printing their details
        private void ListVehiclesUI()
        {
            Console.WriteLine("\n--- All Vehicles ---");
            foreach (var vehicle in _garageHandler.ListVehicles())
            {
                PrintVehicle(vehicle);
            }
        }
        // UI function that allows users to find vehicles based on various properties
        private void FindVehiclesByPropertiesUI()
        {
            Console.WriteLine("\n--- Find Vehicles by Properties ---");
            Console.Write("Color (leave blank to ignore): ");
            var color = Console.ReadLine();
            color = string.IsNullOrWhiteSpace(color) ? null : color;

            Console.Write("Wheel Amount (leave blank to ignore): ");
            var wheelInput = Console.ReadLine();
            int? wheels = int.TryParse(wheelInput, out int w) ? w : null;

            Console.Write("Fuel Type (Gasoline/Diesel, leave blank to ignore): ");
            var fuelInput = Console.ReadLine();
            FuelType? fuel = null;
            if (!string.IsNullOrWhiteSpace(fuelInput))
            {
                fuel = fuelInput.Equals("Diesel", StringComparison.OrdinalIgnoreCase) ? FuelType.Diesel : FuelType.Gasoline;
            }

            Console.Write("Number of Seats (leave blank to ignore): ");
            var seatInput = Console.ReadLine();
            int? seats = int.TryParse(seatInput, out int s) ? s : null;

            Console.Write("Vehicle Type (e.g., Vehicle, leave blank to ignore): ");
            var typeInput = Console.ReadLine();
            Type? vehicleType = null;
            if (!string.IsNullOrWhiteSpace(typeInput))
            {
                vehicleType = Type.GetType($"GarageApplication.{typeInput}", false, true);
            }

            var results = _garageHandler.FindVehicleByVehicleProperties(color, wheels, fuel, seats, vehicleType);
            foreach (var vehicle in results)
            {
                PrintVehicle(vehicle);
            }
        }
        // UI Function that imports test data from a JSON file and adds vehicles to the garage
        private void ImportTestDataUI()
        {
            Console.Write("Enter path to test data file (press Enter for default: Testdata/testdata.json): ");
            var path = Console.ReadLine();

            try
            {
                // If user presses Enter, path will be null or empty, so default will be used
                var testData = TestDataImporter.Import(path);

                // Set garage capacity to the imported value (default 20)
                if (!_garageHandler.SetCapacity(testData.Garage?.Capacity ?? 20))
                {
                    Console.WriteLine("Cannot set garage capacity. Please remove all vehicles first.");
                    return;
                }

                int imported = 0;
                foreach (var dto in testData.Vehicles)
                {
                    Vehicle vehicle = dto.Type switch
                    {
                        "Car" => new Car(dto.RegistrationNumber, dto.Color, dto.WheelAmount, dto.NumberOfEngines, dto.CylinderVolume,
                                         Enum.Parse<FuelType>(dto.Fueltype, true), dto.NumberOfSeats, dto.Length, dto.Weight, dto.Model, dto.Brand),
                        "Motorcycle" => new Motorcycle(dto.RegistrationNumber, dto.Color, dto.WheelAmount, dto.NumberOfEngines, dto.CylinderVolume,
                                                       Enum.Parse<FuelType>(dto.Fueltype, true), dto.NumberOfSeats, dto.Length, dto.Weight, dto.HasSidecar ?? false),
                        "Airplane" => new Airplane(dto.RegistrationNumber, dto.Color, dto.WheelAmount, dto.NumberOfEngines, dto.CylinderVolume,
                                                   Enum.Parse<FuelType>(dto.Fueltype, true), dto.NumberOfSeats, dto.Length, dto.Weight, dto.WingSpan ?? 0, dto.HasJetEngines ?? false),
                        "Bus" => new Bus(dto.RegistrationNumber, dto.Color, dto.WheelAmount, dto.NumberOfEngines, dto.CylinderVolume,
                                         Enum.Parse<FuelType>(dto.Fueltype, true), dto.NumberOfSeats, dto.Length, dto.Weight, dto.IsDoubleDecker ?? false),
                        "Boat" => new Boat(dto.RegistrationNumber, dto.Color, dto.WheelAmount, dto.NumberOfEngines, dto.CylinderVolume,
                                           Enum.Parse<FuelType>(dto.Fueltype, true), dto.NumberOfSeats, dto.Length, dto.Weight, dto.BoatType),
                        _ => null
                    };

                    if (vehicle != null && _garageHandler.AddVehicle(vehicle))
                        imported++;
                }
                Console.WriteLine($"{imported} vehicles imported successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to import test data: {ex.Message}");
            }
        }
        // UI Function that prints the details of a vehicle, including ASCII art based on the vehicle type
        private void PrintVehicle(Vehicle v)
        {
            Console.WriteLine(new string('-', 40));
            // Display the type of vehicle as the first output
            Console.WriteLine($"Type: {v.GetType().Name}");

            // Display ASCII art based on vehicle type
            string asciiArtFile = null;
            switch (v.GetType().Name)
            {
                case "Car":
                    asciiArtFile = "AsciiArt/Car.txt";
                    break;
                case "Motorcycle":
                    asciiArtFile = "AsciiArt/Motorcycle.txt";
                    break;
                case "Airplane":
                    asciiArtFile = "AsciiArt/Airplane.txt";
                    break;
                case "Bus":
                    asciiArtFile = "AsciiArt/Bus.txt";
                    break;
                case "Boat":
                    asciiArtFile = "AsciiArt/Boat.txt";
                    break;
            }

            if (asciiArtFile != null)
            {
                try
                {
                    string asciiArt = File.ReadAllText(asciiArtFile);
                    Console.WriteLine(asciiArt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Could not load {v.GetType().Name.ToLower()} ASCII art: {ex.Message}]");
                }
            }

            // Print base vehicle properties
            Console.WriteLine($"Reg: {v.RegistrationNumber}, Color: {v.Color}, Wheels: {v.WheelAmount}, Engines: {v.NumberOfEngines}, " +
                              $"CylVol: {v.CylinderVolume}, Fuel: {v.Fueltype}, Seats: {v.NumberOfSeats}, Length: {v.Length}, Weight: {v.Weight}");

            // Print subclass-specific properties
            switch (v)
            {
                case Car car:
                    Console.WriteLine($"Model: {car.Model}, Brand: {car.Brand}");
                    break;
                case Motorcycle mc:
                    Console.WriteLine($"Has Sidecar: {mc.HasSidecar}");
                    break;
                case Airplane plane:
                    Console.WriteLine($"Wing Span: {plane.WingSpan}, Has Jet Engines: {plane.HasJetEngines}");
                    break;
                case Bus bus:
                    Console.WriteLine($"Is Double Decker: {bus.IsDoubleDecker}");
                    break;
                case Boat boat:
                    Console.WriteLine($"Boat Type: {boat.BoatType}");
                    break;
            }
            Console.WriteLine(new string('-', 40));
        }

        // UI Function that shows the counts of each vehicle type in the garage
        private void ShowVehicleTypes()
        {
            int total = _garageHandler.ListVehicles().Count();
            var typeCounts = _garageHandler.GetVehicleType();
            Console.WriteLine($"\nTotal vehicles: {total}");
            foreach (var (type, count) in typeCounts)
            {
                Console.WriteLine($"{type}: {count}");
            }
            Console.WriteLine(new string('-', 40));
        }

        // UI Function to create a new garage with a specified capacity
        private void CreateNewGarageUI()
        {
            Console.Write("Enter new garage capacity: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int newCapacity) && newCapacity > 0)
            {
                if (_garageHandler.SetCapacity(newCapacity))
                {
                    Console.WriteLine($"New garage created with capacity {newCapacity}.");
                }
                else
                {
                    Console.WriteLine("Cannot create new garage: Please remove all vehicles first.");
                }
            }
            else
            {
                Console.WriteLine("Invalid capacity. Please enter a positive number.");
            }
        }

        // UI function to reset the garage (removes all vehicles from garage)
        private void ResetGarageUI()
        {
            Console.Write("Are you sure you want to remove all vehicles from the garage? (yes/no): ");
            var input = Console.ReadLine();
            if (input?.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase) == true)
            {
                _garageHandler.ResetGarage();
                Console.WriteLine("All vehicles have been removed from the garage.");
            }
            else
            {
                Console.WriteLine("Garage reset cancelled.");
            }
        }
    }
}
