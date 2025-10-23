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
