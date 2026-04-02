using Bogus;
using Cat_API_Project.Data;
using Cat_API_Project.Services;
using Cat_API_Project.Services.Interfaces;
using Cat_API_Project.Validators;
using Cat_API_Project.Models;

using Microsoft.EntityFrameworkCore;

namespace Cat_API_Project.Services
{
    public class SeedService : ISeedService
    {
        private readonly AppDbContext _context;

        public SeedService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SeedCatsAsync(int count)
        {
            var breeds = await _context.Breeds.ToListAsync(); // get all breeds from DB

            if(!breeds.Any()) // if no breeds found return 0
            {
                return 0;
            }

            var catFaker = new Faker<Cat>() //create a fake cat for model Cat
                .RuleFor(c => c.Name, f => f.Name.FirstName())  //generates a random name for Cat
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.BreedId, f => f.PickRandom(breeds).Id); // chooses a random breed from db and use it's real Id

            var fakerCats = catFaker.Generate(count); //generates cats

            await _context.Cats.AddRangeAsync(fakerCats); //add to db
            await _context.SaveChangesAsync();

            return fakerCats.Count;
        }
    }
}
