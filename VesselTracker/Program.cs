namespace VesselTracker;

public class Program
{
    static void Main(string[] args)
    {
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu.MainMenu();
        }
    }
}

