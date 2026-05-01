namespace Cat_API_Project.DTO
{
    public class UpdateUserCatDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BreedId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
