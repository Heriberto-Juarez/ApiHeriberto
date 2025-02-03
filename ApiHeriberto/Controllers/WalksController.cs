using ApiHeriberto.CustomActionFilters;
using ApiHeriberto.Models.Domain;
using ApiHeriberto.Models.DTO;
using ApiHeriberto.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHeriberto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRespository repository;
        private readonly IMapper mapper;

        public WalksController(IWalkRespository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult> CreateWalk([FromBody] AddWalkRequestDto dto)
        {
            var walk = mapper.Map<Walk>(dto);
            await this.repository.Create(walk);
            return StatusCode(StatusCodes.Status201Created, mapper.Map<WalkDto>(walk));
        }

        [HttpGet]
        public async Task<ActionResult> ListAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending=true,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=10)
        {
            var walks = await this.repository.ListAll(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var walk = await this.repository.GetById(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<ActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto dto)
        {
            var updatedWalk = await this.repository.UpdateById(id, mapper.Map<Walk>(dto));
            if(updatedWalk == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetById), new { id = updatedWalk.Id }, mapper.Map<WalkDto>(updatedWalk));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var deletedWalk = await this.repository.DeleteById(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalk));
        }

    }
}
