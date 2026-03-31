namespace Cat_API_Project.Services.Interfaces
{
    public interface IBreedImportService
    {
        Task<int> ImportBreedsAsync(); // returns an int of breeds, meaning amount of breeds
    }
}
