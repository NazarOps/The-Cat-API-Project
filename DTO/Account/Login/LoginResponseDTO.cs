namespace Cat_API_Project.DTO.Account.Login
{
    public class LoginResponseDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
