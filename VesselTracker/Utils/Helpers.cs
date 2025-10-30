
using System.Globalization;
using System.Reflection;
using Microsoft.Data.SqlClient;
using DotNetEnv;

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

public static class DatabaseHelpers
{
    public static void InitializeDatabase(string connectionString)
    {
        var assembly = Assembly.GetExecutingAssembly();
        // var resourceName = "./scripts/create_vessels_table.sql";

        string resourceFileName = "create_vessels_table.sql";
        string resourceName = assembly.GetManifestResourceNames()
        .Where(name => name.Contains(resourceFileName))
        .FirstOrDefault();

        string sqlScript;
        
        try
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Embedded resource not found: {resourceName}");
                }
                using (var reader = new StreamReader(stream))
                {
                    sqlScript = reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading embedded SQL script: {ex.Message}");
            return;
        }

        using (var connection = new SqlConnection(connectionString))
        {
            using (var command = new SqlCommand(sqlScript, connection))
            {
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database initialization check complete.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database initialization failed: {ex.Message}");
                }
            }
        }
    }
}