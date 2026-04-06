using Cat_API_Project.Models;

namespace Cat_API_Project.DTO
{
    public class BreedFactDTO
    {
        public int Id { get; set; }
        public string Fact { get; set; } = string.Empty;
        public string? Title { get; set; }
        public int BreedId { get; set; }
        public string BreedName { get; set; } = string.Empty;
        
    }
}
