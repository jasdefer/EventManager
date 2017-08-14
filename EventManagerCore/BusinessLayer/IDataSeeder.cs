namespace BusinessLayer
{
    public interface IDataSeeder
    {
        RegionManager RegionManager { get; }
        VisitorManager VisitorManager { get; }
        void SeedData();
    }
}
