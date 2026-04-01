using Cat_API_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Cat_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IBreedImportService _breedImportService;

        public ImportController(IBreedImportService breedImportService)
        {
            _breedImportService = breedImportService;
        }

        [HttpPost("breeds")]
        public async Task<IActionResult> ImportBreeds()
        {
            var importedCount = await _breedImportService.ImportBreedsAsync();

            return Ok(new
            {
                message = "Breeds imported successfully",
                importedCount = importedCount
            });
        }
    }
}
