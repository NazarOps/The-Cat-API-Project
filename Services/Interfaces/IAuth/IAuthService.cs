using Cat_API_Project.DTO;
using Cat_API_Project.DTO.Account.Auth;
using Cat_API_Project.DTO.Account.Login;

namespace Cat_API_Project.Services.Interfaces.IAuth
{
    public interface IAuthService
    {
        Task<AccountResponseDTO> RegisterAsync(RegisterAccountDTO registerAccountDTO);
        Task<LoginResponseDTO> LoginAsync(LoginAccountDTO loginAccountDTO);
    }
}
