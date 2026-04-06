using Cat_API_Project.DTO;

namespace Cat_API_Project.Services.Interfaces
{
    public interface IBreedFactService
    {
        Task<List<BreedFactDTO>> GetAllAsync(BreedFactQueryParametersDTO queryParameters);
        Task<BreedFactDTO> GetByIdAsync(int id);
        Task<BreedFactDTO> CreateAsync(CreateBreedFactDTO createBreedFactDTO);
        Task UpdateBreedFactAsync(int id, UpdateBreedFactDTO updateBreedFactDTO);
        Task DeleteBreedFactAsync(int id);
    }
}
