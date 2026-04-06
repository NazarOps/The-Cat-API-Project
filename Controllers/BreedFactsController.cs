using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Cat_API_Project.DTO;
using Cat_API_Project.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedFactsController : ControllerBase
    {
        private readonly IBreedFactService _breedFactService;
        private readonly IValidator<CreateBreedFactDTO> _createValidator;
        private readonly IValidator<UpdateBreedFactDTO> _updateValidator;

        public BreedFactsController(IBreedFactService breedFactService, IValidator<CreateBreedFactDTO> createValidator, IValidator<UpdateBreedFactDTO> updateValidator)
        {
            _breedFactService = breedFactService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var breedFacts = await _breedFactService.GetAllAsync();
            return Ok(breedFacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var breedFact = await _breedFactService.GetByIdAsync(id);
            return Ok(breedFact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBreedFactDTO createBreedFactDTO)
        {
            var validationResult = await _createValidator.ValidateAsync(createBreedFactDTO);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var createdBreedFact = await _breedFactService.CreateAsync(createBreedFactDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdBreedFact.Id }, createdBreedFact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBreedFactDTO updateBreedFactDTO)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateBreedFactDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            await _breedFactService.UpdateAsync(id, updateBreedFactDTO);

            return NoContent();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(int id)
        {
            await _breedFactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
