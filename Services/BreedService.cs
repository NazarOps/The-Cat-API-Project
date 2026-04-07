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

        public async Task<PagedResultDTO<BreedDTO>> GetAllAsync(BreedQueryParametersDTO queryParametersDTO)
        {
            var query = _context.Breeds.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParametersDTO.Origin))
            {
                var origin = queryParametersDTO.Origin.Trim().ToLower();
                query = query.Where(b => b.Origin.ToLower() == origin);
            }

            if (!string.IsNullOrWhiteSpace(queryParametersDTO.SortBy))
            {
                var sortBy = queryParametersDTO.SortBy.ToLower();
                var sortOrder = queryParametersDTO.SortOrder?.ToLower() == "desc" ? "desc" : "asc";

                query = (sortBy, sortOrder) switch
                {
                    ("breedname", "asc") => query.OrderBy(b => b.BreedName),
                    ("breedname", "desc") => query.OrderByDescending(b => b.BreedName),

                    ("origin", "asc") => query.OrderBy(o => o.Origin),
                    ("origin", "desc") => query.OrderByDescending(o => o.Origin),

                    ("lifespan", "asc") => query.OrderBy(b => b.LifeSpan),
                    ("lifespan", "desc") => query.OrderByDescending(b => b.LifeSpan),

                    _ => query.OrderBy(b => b.Id)
                };   
            }

            else
            {
                query = query.OrderBy(b => b.Id);
            }

            //pagination
            var pageNumber = queryParametersDTO.PageNumber < 1 ? 1 : queryParametersDTO.PageNumber;
            var pageSize = queryParametersDTO.PageSize < 1 ? 10 : queryParametersDTO.PageSize;

            if(pageSize > 50)
            {
                pageSize = 50;
            }

            var totalCount = await query.CountAsync();

            var breeds = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var breedDTOs = _mapper.Map<List<BreedDTO>>(breeds);

            return new PagedResultDTO<BreedDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = breedDTOs
            };
        }

        public async Task<BreedDTO> GetByIdAsync(int id)
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

        public async Task UpdateBreedAsync(int id, UpdateBreedDTO updateBreedDTO)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if(breed == null)
            {
                throw new NotFoundException($"Breed with id {id} was not found");
            }

            breed.BreedName = updateBreedDTO.BreedName;
            breed.Origin = updateBreedDTO.Origin;
            breed.LifeSpan = updateBreedDTO.LifeSpan;
            breed.Description = updateBreedDTO.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBreedAsync(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if(breed == null)
            {
                throw new NotFoundException($"Breed with id {id} was not found");
            }

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();
        }
    }
}
