using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStudio.Application.Services;
using PhotoStudio.ServicesDTO;

namespace PhotoStudio.WebApi.Controllers.v1
{
    /// <summary>
    /// The PhotoBook controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PhotoBookController : ControllerBase
    {
        private readonly IPhotoBookManager photoBookManager;

        /// <summary>
        /// Initializes a new instance for <see cref="PhotoBookController"/>
        /// </summary>
        /// <param name="photoBookManager"></param>
        public PhotoBookController(IPhotoBookManager photoBookManager)
        {
            this.photoBookManager = photoBookManager;
        }

        /// <summary>
        /// Get all photobooks
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [ProducesResponseType(typeof(List<PhotoBookDTO>), 200)]
        public async Task<IActionResult> GetPhotoBooks()
        {
            var photoBooks = await photoBookManager.GetPhotoBooks();

            if (photoBooks != null)
                return Ok(photoBooks);

            return NotFound();
        }

        /// <summary>
        /// Get photo book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(PhotoBookDTO), 200)]
        public async Task<IActionResult> GetPhotoBookById(int id)
        {
            var photoBooks = await photoBookManager.GetPhotoBookById(id);

            if (photoBooks != null)
                return Ok(photoBooks);

            return NotFound();
        }

        /// <summary>
        /// Add photo book
        /// </summary>
        /// <param name="photoBookDTO">The photo book data</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [ProducesResponseType(typeof(PhotoBookDTO), 201)]
        public async Task<IActionResult> AddPhotoBook([FromBody] PhotoBookDTO photoBookDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await photoBookManager.AddPhotoBook(photoBookDTO);

            if (result != null)
                return Ok(result);

            return BadRequest();

        }

    }
}
