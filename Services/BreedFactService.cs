using AutoMapper;
using Cat_API_Project.Data;
using Cat_API_Project.DTO;
using Cat_API_Project.Exceptions;
using Cat_API_Project.Models;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

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

        public async Task<PagedResultDTO<BreedFactDTO>> GetAllAsync(BreedFactQueryParametersDTO queryParametersDTO)
        {
            var query = _context.BreedFacts
                .Include(bf => bf.Breed)
                .AsQueryable();     //build a query that will ask the database to get all breedfacts

            if(queryParametersDTO.BreedId.HasValue)
            {
                query = query.Where(bf => bf.BreedId == queryParametersDTO.BreedId.Value); //if user sends breedid=46 then list will filter
            }

            if(!string.IsNullOrWhiteSpace(queryParametersDTO.SortBy))
            {
                var sortBy = queryParametersDTO.SortBy.ToLower();
                var sortOrder = queryParametersDTO.SortOrder?.ToLower() == "desc" ? "desc" : "asc";

                query = (sortBy, sortOrder) switch
                {
                    ("title", "asc") => query.OrderBy(bf => bf.Title),
                    ("title", "desc") => query.OrderByDescending(bf => bf.Title),

                    ("fact", "asc") => query.OrderBy(bf => bf.Fact),
                    ("fact", "desc") => query.OrderByDescending(bf => bf.Fact),

                    ("breedname", "asc") => query.OrderBy(bf => bf.Breed.BreedName),
                    ("breedname", "desc") => query.OrderByDescending(bf => bf.Breed.BreedName),

                    _ => query.OrderBy(bf => bf.Id)
                };
            }

            else
            {
                query = query.OrderBy(bf => bf.Id);
            }

            // pagination
            var pageNumber = queryParametersDTO.PageNumber < 1 ? 1 : queryParametersDTO.PageNumber;
            var pageSize = queryParametersDTO.PageSize < 1 ? 10 : queryParametersDTO.PageSize;
            
            if(pageSize > 50)
            {
                pageSize = 50;
            }

            var totalCount = await query.CountAsync();

            var breedFacts = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var breedFactDTOs = _mapper.Map<List<BreedFactDTO>>(breedFacts);

            return new PagedResultDTO<BreedFactDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = breedFactDTOs
            };
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

            return _mapper.Map<BreedFactDTO>(createBreedFactDTO);

        }

        public async Task UpdateBreedFactAsync(int id, UpdateBreedFactDTO updateBreedFactDTO)
        {
            var breedFact = await _context.BreedFacts.FindAsync(id);

            if(breedFact == null)
            {
                throw new NotFoundException($"Breed with id {updateBreedFactDTO.BreedId} was not found.");
            }

            _mapper.Map(updateBreedFactDTO, breedFact);
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
