namespace Cat_API_Project.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int accountId, string username, string email);
    }
}
