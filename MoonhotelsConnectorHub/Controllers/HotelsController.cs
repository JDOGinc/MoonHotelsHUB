using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonhotelsConnectorHub.Application.UseCase;
using MoonhotelsConnectorHub.Domain.Dto;

namespace MoonhotelsConnectorHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly SearchHotelsUseCase _searchHotelsUseCase;

        public HotelsController(SearchHotelsUseCase searchHotelsUseCase)
        {
            _searchHotelsUseCase = searchHotelsUseCase;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchHotels([FromBody] HubSearchRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                var response = await _searchHotelsUseCase.PerformSearchAsync(request);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
