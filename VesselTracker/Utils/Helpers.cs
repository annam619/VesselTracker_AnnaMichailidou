
using System.Globalization;

namespace VesselTracker;
public static class InputHelpers
{
    public static int handleBuildYearInput()
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

    public static string handleUniqueIMOInput(IEnumerable<Vessel> vessels)
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
}