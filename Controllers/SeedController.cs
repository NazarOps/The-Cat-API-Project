using Cat_API_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly ISeedService _seedService;

        public SeedController(ISeedService seedService)
        {
            _seedService = seedService;
        }

        [HttpPost("cats/{count}")]
        public async Task<IActionResult> seedCats(int count)
        {
            var seededCount = await _seedService.SeedCatsAsync(count);

            if(seededCount == 0)
            {
                return BadRequest(new { message = "No breeds found. Import breeds first" });
            }

            return Ok(new
            {
                message = "Cats seeded successfully",
                seededCount
            });
        }
    }
}
