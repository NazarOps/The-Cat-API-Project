namespace Cat_API_Project.Services.Interfaces
{
    public interface ISeedService
    {
        Task<int> SeedCatsAsync(int count); // interface method that accepts the amount of cats you want to create and returns the amount created
    }
}
