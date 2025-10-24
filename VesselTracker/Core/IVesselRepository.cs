public interface IVesselRepository
{
    void Add(Vessel vessel);
    IEnumerable<Vessel> GetAll();
    Vessel? GetByIMO(string imo);
}