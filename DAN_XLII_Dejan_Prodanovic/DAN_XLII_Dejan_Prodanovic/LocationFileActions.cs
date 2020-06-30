using DAN_XLII_Dejan_Prodanovic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic
{
    class LocationFileActions
    {
        static ILocationService service = new LocationService();

        public static List<string> ReadLocationsFromFile()
        {
            List<string> locations = new List<string>();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("../../Lokacije.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        locations.Add(line);

                    }
                }
                return locations;
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void AddLocationsToDatabase()
        {
            tblLOCATION location = new tblLOCATION();
            List<string> locations = ReadLocationsFromFile();
            foreach (var l in locations)
            {
                string[] locationData = l.Split(',');

                location.Adress = locationData[0];
                location.Place = locationData[1];
                location.Country = locationData[2];

                service.AddLocation(location);

            }

        }
    }
}
