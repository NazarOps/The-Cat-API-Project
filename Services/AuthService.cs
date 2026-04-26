using Cat_API_Project.Data;
using Cat_API_Project.DTO;
using Cat_API_Project.DTO.Account.Auth;
using Cat_API_Project.DTO.Account.Login;
using Cat_API_Project.Exceptions;
using Cat_API_Project.Models;
using Cat_API_Project.Services.Interfaces.IAuth;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Cat_API_Project.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccountResponseDTO> RegisterAsync(RegisterAccountDTO registerAccountDTO)
        {
            var emailExists = await _context.Accounts
                .AnyAsync(a => a.Email == registerAccountDTO.Email);

            if (emailExists)
            {
                throw new Exception("Email is already registered.");
            }

            var usernameExists = await _context.Accounts
                .AnyAsync(u => u.Username == registerAccountDTO.Username);

            if (usernameExists)
            {
                throw new Exception("Username is already taken.");
            }

            //password gets hashed so that it can be safely saved in database
            CreatePasswordHash(registerAccountDTO.Password, out string passwordHash, out byte[] passwordSalt);

            var account = new Account
            {
                FirstName = registerAccountDTO.FirstName,
                LastName = registerAccountDTO.LastName,
                Username = registerAccountDTO.Username,
                Email = registerAccountDTO.Email,
                DateOfBirth = registerAccountDTO.DateOfBirth,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return new AccountResponseDTO
            {
                AccountId = account.AccountId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Username = account.Username,
                Email = account.Email,
                DateOfBirth = account.DateOfBirth
            };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginAccountDTO loginAccountDTO)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == loginAccountDTO.Email);

            if(account == null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var isPasswordValid = VerifyPasswordHash(
                loginAccountDTO.Password,
                account.PasswordHash,
                account.PasswordSalt
            );

            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }

            return new LoginResponseDTO
            {
                Token = "fake-jwt-token-for-now",
                Username = account.Username,
                Email = account.Email
            };
        }

        private static void CreatePasswordHash(string password, out string passwordHash, out byte[] passwordSalt)
        {
            //here is where the password gets hashed = password + salt = hash
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;

            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            passwordHash = Convert.ToBase64String(hashBytes);
        }

        private static bool VerifyPasswordHash(string password, string storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);

            var computedHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHash = Convert.ToBase64String(computedHashBytes);

            return computedHash == storedHash;
        }
    }
}
