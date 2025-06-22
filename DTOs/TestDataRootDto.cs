using System.Collections.Generic;

namespace GarageApplication.DTOs
{
    public class TestDataRootDto
    {
        public GarageImportDto Garage { get; set; }
        public List<VehicleImportDto> Vehicles { get; set; }
    }
}