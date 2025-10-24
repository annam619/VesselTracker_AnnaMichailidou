public class InMemoryVesselRepository: IVesselRepository
{
    private static List<Vessel> vesselsList = new List<Vessel>();
    public void Add(Vessel vessel)
    {
        vesselsList.Add(vessel);

        Console.WriteLine("Vessel added successfully!");
    }

    public IEnumerable<Vessel> GetAll()
    {

        IEnumerable<Vessel> vessels = vesselsList;
        
        return vessels;
    }

    public Vessel? GetByIMO(string imo)
    {
        return vesselsList.FirstOrDefault(vessel => vessel.IMO == imo);
    }
}