using Cat_API_Project.DTO.External;

namespace Cat_API_Project.Services.Interfaces
{
    public interface ITheCatApiService
    {
        // a list of breeds from the api, task means asynchronus, basically tells the program to keep running and later i will give the list
        Task<List<TheCatApiBreedDTO>> GetBreedsAsync();
    }
}
