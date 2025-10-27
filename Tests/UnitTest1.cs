using System.Linq;
using Xunit;
using VesselTracker;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void TestAddVessels()
    {

        IVesselRepository inMemoryVessel = new InMemoryVesselRepository();
        Vessel vessel1 = new Vessel(0, "Vessel-1", "1234567", "Greece", 1999);
        Vessel vessel2 = new Vessel(1, "Vessel-2", "7654321", "Greece", 2011);
        inMemoryVessel.Add(vessel1);
        inMemoryVessel.Add(vessel2);


        IEnumerable<Vessel> allVessels = inMemoryVessel.GetAll();

        int numberOfVessels = allVessels.ToList().Count;

        Assert.Equal(numberOfVessels, 2);
    }
}
