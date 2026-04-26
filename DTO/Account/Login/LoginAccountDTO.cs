using System.Security.Cryptography.X509Certificates;

namespace Cat_API_Project.DTO.Account.Login
{
    public class LoginAccountDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
