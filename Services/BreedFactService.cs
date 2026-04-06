using AutoMapper;
using Cat_API_Project.Data;
using Cat_API_Project.DTO;
using Cat_API_Project.Exceptions;
using Cat_API_Project.Models;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cat_API_Project.Services
{
    public class BreedFactService : IBreedFactService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BreedFactService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BreedFactDTO>> GetAllAsync()
        {
            var breedFacts = await _context.BreedFacts
                .Include(bf => bf.Breed)
                .ToListAsync();

            return _mapper.Map<List<BreedFactDTO>>(breedFacts);
        }

        public async Task<BreedFactDTO> GetByIdAsync(int id)
        {
            var breedFact = await _context.BreedFacts
                .Include(bf => bf.Breed)
                .FirstOrDefaultAsync(bf => bf.Id == id);

            if(breedFact == null)
            {
                throw new NotFoundException($"Breed fact with the id {id} was not found.");
            }

            return _mapper.Map<BreedFactDTO>(breedFact);
        }

        public async Task<BreedFactDTO> CreateAsync(CreateBreedFactDTO createBreedFactDTO)
        {
            var breedExists = await _context.Breeds
                .AnyAsync(b => b.Id == createBreedFactDTO.BreedId);

            if (!breedExists)
            {
                throw new NotFoundException($"Breed with id {createBreedFactDTO.BreedId} was not found.");
            }

            var breedFact = _mapper.Map<BreedFact>(createBreedFactDTO);

            await _context.BreedFacts.AddAsync(breedFact);
            await _context.SaveChangesAsync();

            var createdBreedFact = await _context.BreedFacts
                .Include(bf => bf.Breed)
                .FirstOrDefaultAsync(bf => bf.Id == breedFact.Id);

            if(createdBreedFact == null)
            {
                throw new Exception("Breed fact could not be created");
            }

            return _mapper.Map<BreedFactDTO>(breedFact);

        }

        public async Task UpdateBreedFactAsync(int id, UpdateBreedFactDTO updateBreedFactDTO)
        {
            var breedFact = await _context.BreedFacts.FindAsync(id);

            if(breedFact == null)
            {
                throw new NotFoundException($"Breed with id {updateBreedFactDTO.BreedId} was not found.");
            }

            breedFact.Title = updateBreedFactDTO.Title;
            breedFact.Fact = updateBreedFactDTO.Fact;
            breedFact.BreedId = updateBreedFactDTO.BreedId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBreedFactAsync(int id)
        {
            var breedFact = await _context.BreedFacts.FindAsync(id);

            if(breedFact == null)
            {
                throw new NotFoundException($"Fact with id {id} was not found");
            }

            _context.BreedFacts.Remove(breedFact);
            await _context.SaveChangesAsync();
        }
    }
}
