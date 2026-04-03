using Cat_API_Project.DTO;

namespace Cat_API_Project.Services.Interfaces
{
    public interface IBreedService
    {
        Task<List<BreedDTO>> GetAllAsync();
        Task<BreedDTO> GetByIdAsync(int id);
        Task<BreedDTO> CreateAsync(CreateBreedDTO createBreedDTO);
        Task<bool> UpdateAsync(int id, UpdateBreedDTO updateBreedDTO);
        Task<bool> DeleteAsync(int id);
    }
}
