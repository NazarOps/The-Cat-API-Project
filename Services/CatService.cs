using Cat_API_Project.DTO;
using Cat_API_Project.Data;
using Cat_API_Project.Models;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Cat_API_Project.Services
{
    public class CatService : ICatService
    {
        private readonly AppDbContext _context;

        public CatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CatDTO>> GetAllCatsAsync()
        {
            return await _context.Cats
                .Include(c => c.Breed) // include breed that the cat belongs to
                .Select(c => new CatDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    BreedId = c.BreedId,
                    BreedName = c.Breed.BreedName // if we didn't include breed then this would be null
                })
                .ToListAsync();  
        }

        public async Task<CatDTO?> GetCatByIdAsync(int id) // get one cat, if it does not exist return null 404
        {
            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(cat == null)
            {
                return null;
            }

            return new CatDTO
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                BreedId = cat.BreedId,
                BreedName = cat.Breed.BreedName
            };
        }

        public async Task<CatDTO?> CreateCatAsync(CreateCatDTO createCatDTO)
        {
            var breedExists = await _context.Breeds
                .AnyAsync(b => b.Id == createCatDTO.BreedId); // checks if the breed exists that the user is trying to connect to

            if(!breedExists) // if breed does not exist, return null and do not create object
            {
                return null;
            }

            var cat = new Cat // if breed exists, initialize an object
            {
                Name = createCatDTO.Name,
                Description = createCatDTO.Description,
                BreedId = createCatDTO.BreedId
            };

            await _context.Cats.AddAsync(cat); // save object
            await _context.SaveChangesAsync();

            var createdCat = await _context.Cats
                .Include(c => c.BreedId)
                .FirstOrDefaultAsync(c => c.Id == cat.Id);

            if(createdCat == null)
            {
                return null;
            }

            return new CatDTO
            {
                Id = createdCat.Id,
                Name = createdCat.Name,
                Description = createdCat.Description,
                BreedId = createdCat.BreedId,
                BreedName = createdCat.Breed.BreedName
            };
        }

        public async Task<bool> UpdateCatAsync(int id, UpdateCatDTO updateCatDTO) // checks if cat and breed exists, if it does then update all the fields
        {
            var cat = await _context.Cats.FindAsync(id);

            if(cat == null)
            {
                return false;
            }

            var breedExists = await _context.Breeds.AnyAsync(b => b.Id == updateCatDTO.BreedId);

            if(!breedExists)
            {
                return false;
            }

            cat.Name = updateCatDTO.Name;
            cat.Description = updateCatDTO.Description;
            cat.BreedId = updateCatDTO.BreedId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCatAsync(int id) // find cat by id, if it exists delete if its null return false does not exist
        {
            var cat = await _context.Cats.FindAsync(id);

            if(cat == null)
            {
                return false;
            }

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
