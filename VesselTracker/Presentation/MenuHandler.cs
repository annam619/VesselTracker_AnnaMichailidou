public class Menu()
{
    public static bool MainMenu()
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

        string IMO = InputHelpers.handleUniqueIMOInput(vessels);

        Console.Write("Enter Flag: ");
        string Flag = Console.ReadLine();

        int BuildYear = InputHelpers.handleBuildYearInput();

        VesselsManager.AddVessel(Id, Name, IMO, Flag, BuildYear);

        Console.WriteLine("\nPress any key to return to the menu.");

        Console.ReadKey();

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