using System.Runtime.CompilerServices;
// this allows the garage application to be accessible by the tests project
// Was having issues with "two Main() method being run" when I had the tests inside the GarageApplication project
// So I moved them to a separate project called GarageApplication.tests 
[assembly: InternalsVisibleTo("GarageApplication.tests")]