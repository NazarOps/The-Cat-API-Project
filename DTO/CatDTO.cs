namespace Cat_API_Project.DTO
{
    public class CatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BreedId { get; set; }
        public string? ImageUrl { get; set; }
        public string BreedName { get; set; } = string.Empty;
    }
}
