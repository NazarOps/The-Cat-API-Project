using Cat_API_Project.DTO;
using Cat_API_Project.Services.Interfaces;
using Cat_API_Project.Models;
using Cat_API_Project.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ICatService _catService;
        private readonly IValidator<CreateCatDTO> _createValidator;
        private readonly IValidator<UpdateCatDTO> _updateValidator;

        public CatsController(ICatService catService, IValidator<CreateCatDTO> createValidator, IValidator<UpdateCatDTO> updateValidator)
        {
            _catService = catService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cats = await _catService.GetAllCatsAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _catService.GetCatByIdAsync(id);
            return Ok(cat);
        }

        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateCatDTO createCatDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createCatDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var createdCat = await _catService.CreateCatAsync(createCatDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCat.Id }, createdCat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCatDTO updateCatDTO)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateCatDTO);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            await _catService.UpdateCatAsync(id, updateCatDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _catService.DeleteCatAsync(id);
            return NoContent();
        }
    }
}
