using Cat_API_Project.DTO;
using Cat_API_Project.DTO.Auth;

namespace Cat_API_Project.Services.Interfaces.IAuth
{
    public interface IAuthService
    {
        Task<AccountResponseDTO> RegisterAsync(RegisterAccountDTO registerAccountDTO);
    }
}
