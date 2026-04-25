namespace Cat_API_Project.Models
{
    public class BreedFact
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Fact { get; set; } = string.Empty;
        public int BreedId { get; set; }
        public Breed Breed { get; set; } = null!;
        public bool IsUserGenerated { get; set; }
    }
}
