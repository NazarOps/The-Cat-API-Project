using System.Globalization;

namespace Cat_API_Project.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public List<Cat> Cats { get; set; } = new();
    }
}
