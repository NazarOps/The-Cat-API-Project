using System.Text.Json;
using Cat_API_Project.DTO.External;
using Cat_API_Project.Services.Interfaces;

namespace Cat_API_Project.Services.External
{
    public class TheCatApiService : ITheCatApiService
    {
        private readonly HttpClient _httpClient; // can't change it, but you can use it

        public TheCatApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("TheCatApi");
        }

        public async Task<List<TheCatApiBreedDTO>> GetBreedsAsync() // get list of breeds asynchronous
        {
            var response = await _httpClient.GetAsync("breeds"); // baseURL = https://api.thecatapi.com/v1/ await = vänta på svaret utan att blockera tråd
            response.EnsureSuccessStatusCode(); // svara tillbaka med status 200 ok

            var json = await response.Content.ReadAsStringAsync();

            var breeds = JsonSerializer.Deserialize<List<TheCatApiBreedDTO>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return breeds ?? new List<TheCatApiBreedDTO>();
        }
    }
}
