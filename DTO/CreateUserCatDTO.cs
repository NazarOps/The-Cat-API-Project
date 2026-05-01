namespace Cat_API_Project.DTO
{
    public class CreateUserCatDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int BreedId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
