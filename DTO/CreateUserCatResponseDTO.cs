namespace Cat_API_Project.DTO
{
    public class CreateUserCatResponseDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int BreedId { get; set; }
    }
}
