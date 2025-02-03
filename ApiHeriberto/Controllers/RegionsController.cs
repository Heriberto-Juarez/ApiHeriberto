using ApiHeriberto.CustomActionFilters;
using ApiHeriberto.Models.Domain;
using ApiHeriberto.Models.DTO;
using ApiHeriberto.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiHeriberto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository repository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<ActionResult> GetById(Guid id)
        {

            var region = await repository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(region));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            logger.LogInformation("GetAllAsync requested");
            var regions = await repository.GetAllAsync();
            logger.LogInformation($"Finished: {JsonSerializer.Serialize(regions)}");
            return Ok(mapper.Map<List<RegionDto>>(regions));
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult> CreateAsync([FromBody] AddRegionDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newRegion = await repository.AddAsync(mapper.Map<Region>(dto));
            var createdDto = mapper.Map<RegionDto>(newRegion);


            return CreatedAtAction(nameof(GetById), new {id= createdDto.Id}, createdDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult> UpdateById(Guid id, [FromBody] UpdateRegionDto dto)
        {
            var region = await repository.UpdateByIdAsync(id, mapper.Map<Region>(dto));
            if (region == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(region));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var region = await this.repository.DeleteById(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
