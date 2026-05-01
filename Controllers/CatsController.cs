using Cat_API_Project.DTO;
using Cat_API_Project.Services.Interfaces;
using Cat_API_Project.Models;
using Cat_API_Project.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Cat_API_Project.Data;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ICatService _catService;
        private readonly IValidator<CreateCatDTO> _createValidator;
        private readonly IValidator<UpdateCatDTO> _updateValidator;
        private readonly AppDbContext _context;

        public CatsController(ICatService catService, IValidator<CreateCatDTO> createValidator, IValidator<UpdateCatDTO> updateValidator, AppDbContext context)
        {
            _catService = catService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CatQueryParametersDTO queryParametersDTO) // read from query string in url
        {
            var cats = await _catService.GetAllCatsAsync(queryParametersDTO);
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _catService.GetCatByIdAsync(id);
            return Ok(cat);
        }

        //endpoint for authorized users with JWT token
        [Authorize]
        [HttpPost("create-a-cat")]
        public async Task<IActionResult> CreateMyCat([FromBody] CreateUserCatDTO createUserCatDTO)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //gets accountid from JWT token

            if(accountIdClaim == null)
            {
                return Unauthorized();
            }

            var accountId = Convert.ToInt32(accountIdClaim);

            var Cat = await _catService.CreateUserCatAsync(createUserCatDTO, accountId);

            return CreatedAtAction(nameof(CreateMyCat), new { id = Cat.Id }, Cat);
        }

        [Authorize]
        [HttpGet("my-cats")]
        public async Task<IActionResult> GetMyCats()
        {
            //checks if user has permission to execute the request, finds account id
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(accountIdClaim == null)
            {
                return Unauthorized();
            }

            var accountId = Convert.ToInt32(accountIdClaim);

            var getMyCats = await _catService.GetUserCatsAsync(accountId);

            return Ok(getMyCats);


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
