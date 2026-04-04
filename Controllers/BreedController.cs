using Cat_API_Project.DTO;
using Cat_API_Project.Services;
using Cat_API_Project.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedController : ControllerBase
    {
        private readonly IBreedService _breedService;
        private readonly IValidator<CreateBreedDTO> _createValidator;
        private readonly IValidator<UpdateBreedDTO> _updateValidator;

        public BreedController(IBreedService breedService, IValidator<CreateBreedDTO> createValidator, IValidator<UpdateBreedDTO> updateValidator)
        {
            _breedService = breedService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
        public async Task<IActionResult> Create([FromBody] CreateBreedDTO createBreedDTO)
        {
            var validationResult = await _createValidator.ValidateAsync(createBreedDTO);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var createdBreed = await _breedService.CreateAsync(createBreedDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdBreed.Id }, createdBreed);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBreedDTO updateBreedDTO)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateBreedDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            await _breedService.UpdateBreedAsync(id, updateBreedDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _breedService.DeleteBreedAsync(id);
            return NoContent();
        }
    }
}
