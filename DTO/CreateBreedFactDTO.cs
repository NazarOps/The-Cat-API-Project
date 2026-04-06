namespace Cat_API_Project.DTO
{
    public class CreateBreedFactDTO
    {
        public string Fact { get; set; } = string.Empty;
        public string? Title { get; set; }
        public int BreedId { get; set; }
    }
}
