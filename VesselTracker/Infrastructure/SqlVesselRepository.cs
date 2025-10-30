using System.Reflection;
using Microsoft.Data.SqlClient;

namespace VesselTracker;

public class SqlVesselRepository : IVesselRepository
{
    private static List<Vessel> vesselsList = new List<Vessel>();
    private static readonly string _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

    public void Add(Vessel vessel)
    {
        string sqlQuery = "INSERT INTO Vessels (Name, IMO, Flag, BuildYear) VALUES (@Name, @IMO, @Flag, @BuildYear)";

        int rowsAffected;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", vessel.Name);
                command.Parameters.AddWithValue("@IMO", vessel.IMO);
                command.Parameters.AddWithValue("@Flag", vessel.Flag);
                command.Parameters.AddWithValue("@BuildYear", vessel.BuildYear);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine("Vessel added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding vessel: {ex.Message}");
                }
            }
        }

    }

    public IEnumerable<Vessel> GetAll()
    {

        var vesselsList = new List<Vessel>();
        string sqlQuery = "SELECT Id, Name, IMO, Flag, BuildYear FROM Vessels";

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string Name = reader.GetString(reader.GetOrdinal("Name"));
                            string IMO = reader.GetString(reader.GetOrdinal("IMO"));
                            string Flag = reader.GetString(reader.GetOrdinal("Flag"));
                            int BuildYear = reader.GetInt32(reader.GetOrdinal("BuildYear"));

                            var vessel = new Vessel(Id, Name, IMO, Flag, BuildYear);

                            vesselsList.Add(vessel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving vessels: {ex.Message}");
                }
            }

        }
        IEnumerable<Vessel> vessels = vesselsList;

        return vessels;
    }

    public Vessel? GetByIMO(string imo)
    {
        return vesselsList.FirstOrDefault(vessel => vessel.IMO == imo);
    }
}