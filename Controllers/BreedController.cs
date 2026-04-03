using Cat_API_Project.DTO;
using Cat_API_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedController : ControllerBase
    {
        private readonly IBreedService _breedService;

        public BreedController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var breeds = await _breedService.GetAllAsync();
            return Ok(breeds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var breed = await _breedService.GetByIdAsync(id);
            return Ok(breed);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBreedDTO createBreedDTO)
        {
            var createdBreed = await _breedService.CreateAsync(createBreedDTO);

            return CreatedAtAction(nameof(GetById), new { id = createdBreed.Id }, createdBreed);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBreedDTO updateBreedDTO)
        {
            var updated = await _breedService.UpdateAsync(id, updateBreedDTO);

            if(!updated)
            {
                return NotFound(new { message = $"Breed with id {id} was not found" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _breedService.DeleteAsync(id);

            if(!deleted)
            {
                return NotFound(new { message = $"Breed with id {id} was not found" });
            }

            return NoContent();
        }
    }
}
