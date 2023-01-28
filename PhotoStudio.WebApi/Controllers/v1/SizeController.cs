using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Application.Services;
using PhotoStudio.ServicesDTO;

namespace PhotoStudio.WebApi.Controllers.v1
{
    /// <summary>
    /// The controller for Size
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeManager sizeManager;

        /// <summary>
        /// Initialized a new instance for <see cref="SizeController"/>
        /// </summary>
        /// <param name="sizeManager"></param>
        public SizeController(ISizeManager sizeManager)
        {
            this.sizeManager = sizeManager;

        }
        /// <summary>
        /// Get all sizes
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [ProducesResponseType(typeof(List<SizeDTO>), 200)]
        public async Task<IActionResult> GetSizes()
        {
            var result = await sizeManager.GetSizes();
            if (result != null)
                return Ok(result);

            return BadRequest();
        }
        /// <summary>
        /// Get size by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}", Name = "GetSizeById")]
        [ProducesResponseType(typeof(SizeDTO), 200)]
        public async Task<IActionResult> GetSizeById(int id)
        {
            var result = await sizeManager.GetSizeById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        ///Add size 
        /// </summary>
        /// <param name="size">Size data</param>
        /// <returns></returns>
        [HttpPost, Route("")]
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

        /// <summary>
        /// Update size
        /// </summary>
        /// <param name="size">Data size to update</param>
        /// <param name="id">Id to update</param>
        /// <returns></returns>
        [HttpPut, Route("{id:int}")]
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

        /// <summary>
        /// Delete size
        /// </summary>
        /// <param name="id">Id to delete</param>
        /// <returns></returns>
        [HttpDelete, Route("{id:int}")]
        [ProducesResponseType(typeof(SizeDTO), 200)]
        public async Task<IActionResult> DeleteSize(int id)
        {
            await sizeManager.DeleteSize(id);

            return Ok();

        }
    }
}
