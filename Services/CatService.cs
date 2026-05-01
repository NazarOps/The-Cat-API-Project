using Cat_API_Project.DTO;
using Cat_API_Project.Data;
using Cat_API_Project.Models;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Cat_API_Project.Exceptions;

namespace Cat_API_Project.Services
{
    public class CatService : ICatService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CatService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<PagedResultDTO<CatDTO>> GetAllCatsAsync(CatQueryParametersDTO queryParametersDTO)
        {
            //return await _context.Cats
            //    .Include(c => c.Breed) // include breed that the cat belongs to
            //    .Select(c => new CatDTO
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        Description = c.Description,
            //        BreedId = c.BreedId,
            //        BreedName = c.Breed.BreedName // if we didn't include breed then this would be null
            //    })
            //    .ToListAsync();  

            var query = _context.Cats
                .Include(c => c.Breed)
                .AsQueryable(); // build query before using it to search in database

            //filtering
            if (queryParametersDTO.BreedId.HasValue)
            {
                query = query.Where(c => c.BreedId == queryParametersDTO.BreedId.Value); // if user sends breed.id then only filter after the breed
            }

            //sorting
            if(!string.IsNullOrWhiteSpace(queryParametersDTO.SortBy))
            {
                var sortBy = queryParametersDTO.SortBy.ToLower();
                var sortOrder = queryParametersDTO.SortOrder?.ToLower() == "desc" ? "desc" : "asc";

                query = (sortBy, sortOrder) switch
                {
                    ("name", "asc") => query.OrderBy(c => c.Name),
                    ("name", "desc") => query.OrderByDescending(c => c.Name),

                    ("description", "asc") => query.OrderBy(c => c.Description),
                    ("description", "desc") => query.OrderByDescending(c => c.Description),

                    ("breedname", "asc") => query.OrderBy(c => c.Breed.BreedName),
                    ("breedname", "desc") => query.OrderByDescending(c => c.Breed.BreedName),

                    _ => query.OrderBy(c => c.Id)
                };
            }

            else
            {
                query = query.OrderBy(c => c.Id);
            }

            //pagination
            var pageNumber = queryParametersDTO.PageNumber < 1 ? 1 : queryParametersDTO.PageNumber;
            var pageSize = queryParametersDTO.PageSize < 1 ? 10 : queryParametersDTO.PageSize;

            if(pageSize > 50)
            {
                pageSize = 50;
            }

            var totalCount = await query.CountAsync();

            var cats = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            //get all cats first then use automapper

            var catDTOs = _mapper.Map<List<CatDTO>>(cats);

            return new PagedResultDTO<CatDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = catDTOs
            };
        }

        public async Task<CatDTO> GetCatByIdAsync(int id) // get one cat, if it does not exist return null 404
        {
            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(cat == null)
            {
                throw new NotFoundException($"Cat with id {id} was not found");
            }

            //return new CatDTO
            //{
            //    Id = cat.Id,
            //    Name = cat.Name,
            //    Description = cat.Description,
            //    BreedId = cat.BreedId,
            //    BreedName = cat.Breed.BreedName
            //};

            return _mapper.Map<CatDTO>(cat);
        }

        public async Task<CatDTO> CreateCatAsync(CreateCatDTO createCatDTO)
        {
            var breedExists = await _context.Breeds
                .AnyAsync(b => b.Id == createCatDTO.BreedId); // checks if the breed exists that the user is trying to connect to

            if(!breedExists) // if breed does not exist, return null and do not create object
            {
                throw new NotFoundException($"Breed was not found");
            }

            //var cat = new Cat // if breed exists, initialize an object
            //{
            //    Name = createCatDTO.Name,
            //    Description = createCatDTO.Description,
            //    BreedId = createCatDTO.BreedId
            //};

            var cat = _mapper.Map<Cat>(createCatDTO); // use automapper instead of manually assigning object above

            await _context.Cats.AddAsync(cat); // save object
            await _context.SaveChangesAsync();

            var createdCat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(c => c.Id == cat.Id);

            if(createdCat == null)
            {
                throw new Exception("Cat could not be created");
            }

            //return new CatDTO
            //{
            //    Id = createdCat.Id,
            //    Name = createdCat.Name,
            //    Description = createdCat.Description,
            //    BreedId = createdCat.BreedId,
            //    BreedName = createdCat.Breed.BreedName
            //};

            return _mapper.Map<CatDTO>(createdCat);
        }

        public async Task<Cat> CreateUserCatAsync(CreateUserCatDTO createUserCatDTO, int accountId)
        {
            //var cat = new Cat
            //{
            //    Name = dto.Name,
            //    Description = dto.Description,
            //    BreedId = dto.BreedId,
            //    ImageUrl = dto.ImageUrl,
            //    AccountId = accountId
            //};

            var cat = _mapper.Map<Cat>(createUserCatDTO);

            cat.AccountId = accountId;
            
            await _context.Cats.AddAsync(cat);
            await _context.SaveChangesAsync();

            return cat;
        }

        public async Task<List<UserCatDTO>> GetUserCatsAsync(int accountId) //get user's cats after auth successful
        {
            var userCats = await _context.Cats
                .Where(c => c.AccountId == accountId)
                .ToListAsync();

            return _mapper.Map<List<UserCatDTO>>(userCats);
        }

        public async Task<UserCatDTO> UpdateUserCatAsync(int catId, UpdateUserCatDTO updateUserCatDTO, int accountId) //update user's cat after auth success
        {
            var userCat = await _context.Cats
                .FirstOrDefaultAsync(c => c.Id == catId);

            if(userCat == null)
            {
                throw new NotFoundException("Cat was not found.");
            }

            if(userCat.AccountId != accountId)
            {
                throw new UnauthorizedException("You do not own this cat.");
            }

            _mapper.Map(updateUserCatDTO, userCat);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserCatDTO>(userCat);

        }

        public async Task UpdateCatAsync(int id, UpdateCatDTO updateCatDTO) // checks if cat and breed exists, if it does then update all the fields
        {
            var cat = await _context.Cats.FindAsync(id);

            if(cat == null)
            {
                throw new NotFoundException($"Cat with id {id} was not found");
            }

            var breedExists = await _context.Breeds.AnyAsync(b => b.Id == updateCatDTO.BreedId);

            if(!breedExists)
            {
                throw new NotFoundException($"Breed with id {id} was not found");
            }

            _mapper.Map(updateCatDTO, cat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserCatAsync(int catId, int accountId)
        {
            var userCat = await _context.Cats
                .FirstOrDefaultAsync(c => c.Id == catId);

            if(userCat == null)
            {
                throw new NotFoundException("Cat was not found.");
            }

            if(userCat.AccountId != accountId)
            {
                throw new UnauthorizedException("You do not own this cat.");
            }

            _context.Cats.Remove(userCat);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCatAsync(int id) // find cat by id, if it exists delete if its null return false does not exist
        {
            var cat = await _context.Cats.FindAsync(id);

            if(cat == null)
            {
                throw new NotFoundException($"Cat with id {id} was not found");
            }

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();
        }
    }
}
