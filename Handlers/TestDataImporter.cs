using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GarageApplication.DTOs;

namespace GarageApplication.Handlers
{
    // function that imports test data from a JSON file and returns a TestDataRootDto object
    // the file path is optional, if not provided it defaults to "Testdata/testdata.json"
    public static class TestDataImporter
    {
        private const string DefaultPath = "Testdata/testdata.json";

        public static TestDataRootDto Import(string filePath = null)
        {
            var path = string.IsNullOrWhiteSpace(filePath) ? DefaultPath : filePath;
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<TestDataRootDto>(json);
        }
    }
}