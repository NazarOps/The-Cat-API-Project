namespace Cat_API_Project.DTO.Auth
{
    public class AccountResponseDTO
    {
        //returns successful account creation response as DTO
        public int AccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }

        //No passwordhash, salt and refresh token in response DTO because it will be hashed
    }
}
