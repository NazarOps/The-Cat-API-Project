using Cat_API_Project.DTO;
using Cat_API_Project.Services.Interfaces;
using Cat_API_Project.Models;
using Cat_API_Project.Controllers;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/get-all-cats")]
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

        [HttpPost("/create-a-cat")] 
        public async Task<IActionResult> Create(CreateCatDTO createCatDto)
        {
            var createdCat = await _catService.CreateCatAsync(createCatDto);

            if(createdCat == null)
            {
                return BadRequest(new { message = "Breed does not exist" });
            }

            return CreatedAtAction(nameof(GetById), new { id = createdCat.Id }, createdCat);
        }

        [HttpPut("/update-a-cat")]
        public async Task<IActionResult> Update(int id, UpdateCatDTO updateCatDTO)
        {
            var updatedCat = await _catService.UpdateCatAsync(id, updateCatDTO);

            if(!updatedCat)
            {
                return BadRequest(new { message = "Cat or Breed not found" });
            }

            return NoContent();
        }

        [HttpDelete("/delete-a-cat")]
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
