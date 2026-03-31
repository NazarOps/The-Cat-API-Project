using Cat_API_Project.Data;
using Cat_API_Project.DTO.External;
using Cat_API_Project.Models;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cat_API_Project.Services
{
    public class BreedImportService : IBreedImportService
    {
        private readonly AppDbContext _context;
        private readonly ITheCatApiService _theCatApiService;

        public BreedImportService(AppDbContext context, ITheCatApiService theCatApiService)
        {
            _context = context;
            _theCatApiService = theCatApiService;
        }

        public async Task<int> ImportBreedsAsync()
        {
            var importBreeds = await _theCatApiService.GetBreedsAsync(); // get a list of breeds from the api

            int importedCount = 0;

            foreach (var breeds in importBreeds)    //loop through all the imported breeds from cat api
            {
                var alreadyExists = await _context.Breeds
                    .AnyAsync(b => b.ExternalBreedId == breeds.Id); //compare external api breed with database breed if it already exists

                if (alreadyExists)       //if exists, skip and map DTO to api breed
                {
                    continue;
                }

                var breed = new Breed
                {
                    ExternalBreedId = breeds.Id,
                    BreedName = breeds.Name,
                    Origin = breeds.Origin,
                    LifeSpan = breeds.LifeSpan,
                    Description = breeds.Description
                };

                await _context.Breeds.AddAsync(breed); //add breed in EF change tracker
                importedCount++;
            }

            await _context.SaveChangesAsync();
            return importedCount;
            
        }
    }
}
