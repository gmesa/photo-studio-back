using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Application.Services;
using PhotoStudio.ServicesDTO;

namespace PhotoStudio.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeManager sizeManager;

        public SizeController(ISizeManager sizeManager)
        {
            this.sizeManager = sizeManager;

        }

        [HttpGet, Route("size")]
        [ProducesResponseType(typeof(List<SizeDTO>), 200)]
        public async Task<IActionResult> GetSizes()
        {
            var result = await sizeManager.GetSizes();
            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpGet, Route("size/{id}", Name = "GetSizeById")]
        [ProducesResponseType(typeof(SizeDTO), 200)]
        public async Task<IActionResult> GetSizeById(int id)
        {
            var result = await sizeManager.GetSizeById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost, Route("size")]
        [ProducesResponseType(typeof(SizeDTO), 201)]
        public async Task<IActionResult> AddSize([FromBody] SizeDTO size)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await sizeManager.AddSize(size);

            if (result != null)
                return CreatedAtAction(nameof(GetSizeById), new { id = result.Id }, result);

            return BadRequest();

        }

        [HttpPut, Route("size/{id}")]
        [ProducesResponseType(typeof(SizeDTO), 200)]
        public async Task<IActionResult> UpdateSize([FromBody] SizeDTO size, int id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await sizeManager.UpdateSize(size, id);

            if (result != null)
                return Ok(result);

            return BadRequest();

        }

        [HttpDelete, Route("size/{id}")]
        [ProducesResponseType(typeof(SizeDTO), 200)]
        public async Task<IActionResult> DeleteSize(int id)
        {
            await sizeManager.DeleteSize(id);

            return Ok();

        }
    }
}
