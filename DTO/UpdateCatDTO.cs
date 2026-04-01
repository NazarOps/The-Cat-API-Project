namespace Cat_API_Project.DTO
{
    public class UpdateCatDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BreedId { get; set; }
    }
}
