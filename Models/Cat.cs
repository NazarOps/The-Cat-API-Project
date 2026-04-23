namespace Cat_API_Project.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BreedId { get; set; }
        public Breed Breed { get; set; } = null!;
        public Account? Account { get; set; }
        public string? ImageUrl { get; set; }
    }
}
