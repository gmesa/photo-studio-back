using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoStudio.Application.Services;
using PhotoStudio.Application.Validator;
using PhotoStudio.Domain.Entities;
using PhotoStudio.Infrastructure.Commons.Configurations;
using PhotoStudio.Infrastructure.Commons.CustomExceptions;
using PhotoStudio.Infrastructure.Data.DBContext;
using PhotoStudio.ServicesDTO;

namespace PhotoStudio.WebApi.Controllers.v1
{
    /// <summary>
    /// The material controller
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialManager materialManager;

        /// <summary>
        /// Initilizes new instance for <see cref="MaterialController"/>
        /// </summary>
        /// <param name="materialManager">The manager for controller</param>
        public MaterialController(IMaterialManager materialManager)
        {
            this.materialManager = materialManager;
        }
        /// <summary>
        /// Get all materials.
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet, Route("")]
        [ProducesResponseType(typeof(List<MaterialDTO>), 200)]
        public async Task<IActionResult> GetMaterials()
        {
            var result = await materialManager.GetMaterials();
            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        /// <summary>
        /// Get material by id
        /// </summary>
        /// <param name="id">The material id</param>
        /// <returns></returns>
        [HttpGet, Route("{id:int}", Name = "GetMaterialById")]
        [ProducesResponseType(typeof(MaterialDTO), 200)]
        public async Task<IActionResult> GetMaterialById(int id)
        {
            var result = await materialManager.GetMaterialById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Add material
        /// </summary>
        /// <param name="material">The material data</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [ProducesResponseType(typeof(MaterialDTO), 201)]
        public async Task<IActionResult> AddMaterial([FromBody] MaterialDTO material)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await materialManager.AddMaterial(material);

            if (result != null)
                return CreatedAtAction(nameof(GetMaterialById), new { id = result.Id }, result);
            // return Created(Url.Link(nameof(GetMaterialById), new { id = result.Id }), result);

            return BadRequest();

        }

        /// <summary>
        /// Update material with specific id
        /// </summary>
        /// <param name="material">update data</param>
        /// <param name="id">material id </param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        [ProducesResponseType(typeof(MaterialDTO), 200)]
        public async Task<IActionResult> UpdateMaterial([FromBody] MaterialDTO material, int id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await materialManager.UpdateMaterial(material, id);

            if (result != null)
                return Ok(result);

            return NotFound();

        }

        /// <summary>
        /// Delete material with the specified id
        /// </summary>
        /// <param name="id">The material id to delete</param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(MaterialDTO), 200)]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            await materialManager.DeleteMaterial(id);
            return Ok();
        }

        [HttpGet, Route("{name}", Name = "GetMaterialsWithName")]
        [ProducesResponseType(typeof(List<MaterialDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMaterialsWithName(string name)
        {
            var result = await materialManager.GetMaterialByTextName(name);

            if(result == null)
                return NotFound();
            else 
                return Ok(result);
        }
        
    }
}
