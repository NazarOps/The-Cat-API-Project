using Cat_API_Project.DTO;

namespace Cat_API_Project.Services.Interfaces
{
    public interface IBreedService
    {
        Task<PagedResultDTO<BreedDTO>> GetAllAsync(BreedQueryParametersDTO queryParameters);
        Task<BreedDTO> GetByIdAsync(int id);
        Task<BreedDTO> CreateAsync(CreateBreedDTO createBreedDTO);
        Task UpdateBreedAsync(int id, UpdateBreedDTO updateBreedDTO);
        Task DeleteBreedAsync(int id);
    }
}
