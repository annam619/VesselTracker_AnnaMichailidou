using DotNetEnv;
using System;

namespace VesselTracker;

public class Program
{
    static void Main(string[] args)
    {
        Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "VesselTracker/.env"));
        string _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        
        DatabaseHelpers.InitializeDatabase(_connectionString);

        if (!string.IsNullOrEmpty(_connectionString))
        {
            Console.WriteLine($"The PATH variable is: {_connectionString}");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("The PATH variable was not found.");
            Console.ReadKey();
        }

        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu.MainMenu();
        }
    }
}

