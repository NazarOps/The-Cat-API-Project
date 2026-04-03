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

        public CatsController(ICatService catService)
        {
            _catService = catService;
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

            if (cat == null)
            {
                return NotFound(new { message = $"Cat with id {id} was not found" });
            }

            return Ok(cat);
        }

        [HttpPost] 
        public async Task<IActionResult> Create(CreateCatDTO createCatDto, IValidator<CreateCatDTO> validator)
        {
            var validationResult = await validator.ValidateAsync(createCatDto);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var createdCat = await _catService.CreateCatAsync(createCatDto);

            if(createdCat == null)
            {
                return BadRequest(new { message = "Breed does not exist" });
            }

            return CreatedAtAction(nameof(GetById), new { id = createdCat.Id }, createdCat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCatDTO updateCatDTO, IValidator<UpdateCatDTO> validator)
        {
            var validationResult = await validator.ValidateAsync(updateCatDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var updatedCat = await _catService.UpdateCatAsync(id, updateCatDTO);

            if(!updatedCat)
            {
                return BadRequest(new { message = "Cat or Breed not found" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _catService.DeleteCatAsync(id);

            if(!deleted)
            {
                return NotFound(new { message = $"Cat with id {id} was not found" });
            }

            return NoContent();
        }
    }
}
