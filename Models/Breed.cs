using Microsoft.Extensions.Primitives;

namespace Cat_API_Project.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string ExternalBreedId { get; set; } = string.Empty;
        public string BreedName { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string LifeSpan { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Cat> Cats { get; set; } = new();
        public List<BreedFact> BreedFacts { get; set; } = new();
    }
}
