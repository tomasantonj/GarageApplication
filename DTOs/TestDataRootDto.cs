using System.Collections.Generic;

namespace GarageApplication.DTOs
{
    // function that fetches test data from file and returns a Data Transfer Object
    public class TestDataRootDto
    {
        public GarageImportDto Garage { get; set; }
        public List<VehicleImportDto> Vehicles { get; set; }
    }
}