using AutoMapper;
using Cat_API_Project.Data;
using Cat_API_Project.DTO;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cat_API_Project.Models;
using Cat_API_Project.Exceptions;

namespace Cat_API_Project.Services
{
    public class BreedService : IBreedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BreedService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BreedDTO>> GetAllAsync()
        {
            var breeds = await _context.Breeds.ToListAsync();
            return _mapper.Map<List<BreedDTO>>(breeds);
        }

        public async Task<BreedDTO?> GetByIdAsync(int id)
        {
            var breed = await _context.Breeds.FirstOrDefaultAsync(b => b.Id == id);

            if(breed == null)
            {
                throw new NotFoundException($"Breed with id {id} was not found.");
            }

            return _mapper.Map<BreedDTO>(breed);
        }

        public async Task<BreedDTO> CreateAsync(CreateBreedDTO createBreedDTO)
        {
            var breed = _mapper.Map<Breed>(createBreedDTO);

            await _context.Breeds.AddAsync(breed);
            await _context.SaveChangesAsync();

            return _mapper.Map<BreedDTO>(breed);
        }

        public async Task<bool> UpdateAsync(int id, UpdateBreedDTO updateBreedDTO)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if(breed == null)
            {
                return false;
            }

            breed.BreedName = updateBreedDTO.BreedName;
            breed.Origin = updateBreedDTO.Origin;
            breed.LifeSpan = updateBreedDTO.LifeSpan;
            breed.Description = updateBreedDTO.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if(breed == null)
            {
                return false;
            }

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
