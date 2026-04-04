using Cat_API_Project.DTO;

namespace Cat_API_Project.Services.Interfaces
{
    public interface ICatService
    {
        Task<List<CatDTO>> GetAllCatsAsync(); // make an async method that collects all cats from DTO and returns it as a list
        Task<CatDTO> GetCatByIdAsync(int id); // get cat by id, if no cat exists then null
        Task<CatDTO> CreateCatAsync(CreateCatDTO createCatDTO); //create and return the created cat
        Task UpdateCatAsync(int id, UpdateCatDTO updateCatDTO); // return true if the update succeeded else false
        Task DeleteCatAsync(int id);  // return true if deletion succeeded else return false
    }
}
