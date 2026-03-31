using System.Text.Json.Serialization;
using Cat_API_Project.DTO.External;

namespace Cat_API_Project.DTO.External
{
    public class TheCatApiBreedDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("origin")]
        public string Origin { get; set; } = string.Empty;

        // maps to json field life_span from cat api
        [JsonPropertyName("life_span")]
        public string LifeSpan { get; set; } = string.Empty; //string.Empty = never null instead its empty

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
