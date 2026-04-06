using Cat_API_Project.DTO;

namespace Cat_API_Project.Services.Interfaces
{
    public interface IBreedFactService
    {
        Task<List<BreedFactDTO>> GetAllAsync();
        Task<BreedFactDTO> GetByIdAsync(int id);
        Task<BreedFactDTO> CreateAsync(CreateBreedFactDTO createBreedFactDTO);
        Task UpdateAsync(int id, UpdateBreedFactDTO updateBreedFactDTO);
        Task DeleteAsync(int id);
    }
}
