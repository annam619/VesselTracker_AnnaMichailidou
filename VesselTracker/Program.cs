using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Linq;

namespace VesselTracker
{
    public class Vessel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IMO { get; set; }
        public string Flag { get; set; }
        public int BuildYear { get; set; }

        public Vessel(int Id, string Name, string IMO, string Flag, int BuildYear)
        {
            this.Id = Id;
            this.Name = Name;
            this.IMO = IMO;
            this.Flag = Flag;
            this.BuildYear = BuildYear;
        }
    }

    public static class VesselsManager
    {
        private static List<Vessel> vessels = new List<Vessel>();

        public static void AddVessel(int Id, string Name, string IMO, string Flag, int BuildYear)
        {
            vessels.Add(new Vessel(Id, Name, IMO, Flag, BuildYear));

            Console.WriteLine("Vessel added successfully!");
        }

        public static List<Vessel> GetAllVessels()
        {
            return vessels;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add a new vessel");
            Console.WriteLine("2. List all vessels");
            Console.WriteLine("3. Exit");
            Console.Write("\r\nSelect option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    addVesselMenu();
                    return true;
                case "2":
                    listVesselsMenu();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

        private static void addVesselMenu()
        {
            Console.Clear();

            List<Vessel> vessels = VesselsManager.GetAllVessels();

            int Id = vessels.Count;

            Console.Write("Enter vessel Name: ");
            string Name = Console.ReadLine();

            string IMO = handleUniqueIMOInput(vessels);

            Console.Write("Enter Flag: ");
            string Flag = Console.ReadLine();

            int BuildYear = handleBuildYearInput();

            VesselsManager.AddVessel(Id, Name, IMO, Flag, BuildYear);

            Console.WriteLine("\nPress any key to return to the menu.");

            Console.ReadKey();

        }

        private static int handleBuildYearInput()
        {
            string BuildYear;
            while (true)
            {
                Console.Write("Enter build year: ");
                BuildYear = Console.ReadLine();
                CultureInfo enUS = new CultureInfo("en-US");
                DateTime dateValue;
                bool BuildYearIsValid = DateTime.TryParseExact(
                    Convert.ToString(BuildYear),
                    "yyyy",
                    enUS,
                    DateTimeStyles.None,
                    out dateValue
                );
                if (BuildYearIsValid) break;
                else Console.WriteLine("Invalid build year format!");
            }

            return Convert.ToInt32(BuildYear);
        }

        private static string handleUniqueIMOInput(List<Vessel> vessels)
        {
            string IMO;
            while (true)
            {
                Console.Write("Enter IMO: ");
                IMO = Console.ReadLine();

                bool IMOIsUnique = true;
                foreach (Vessel v in vessels)
                {
                    if (v.IMO == IMO)
                    {
                        Console.WriteLine("Vessel with this IMO already exists!");
                        IMOIsUnique = false;
                    }
                }
                if (IMOIsUnique)
                {
                    break;
                }
            }

            return IMO;
        }

        private static void listVesselsMenu()
        {
            Console.Clear();
            List<Vessel> vessels = VesselsManager.GetAllVessels();

            if (vessels.Count == 0)
            {
                Console.WriteLine("No vessels have been added yet.");
            }
            else
            {
                foreach (var vessel in vessels)
                {
                    Console.WriteLine($"Id: {vessel.Id} | Name: {vessel.Name} | IMO: {vessel.IMO} | Flag: {vessel.Flag} | Year: {vessel.BuildYear}");
                }
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();

        }
    }
}
