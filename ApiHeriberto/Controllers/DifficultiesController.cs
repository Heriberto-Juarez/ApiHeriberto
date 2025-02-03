using ApiHeriberto.Models.DTO;
using ApiHeriberto.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHeriberto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class DifficultiesController : ControllerBase
    {
        private readonly IDifficultyRepository repository;
        private readonly IMapper mapper;

        public DifficultiesController(IDifficultyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var difficulties = await this.repository.GetAll();
            return Ok(mapper.Map<List<DifficultyDto>>(difficulties));
        }

    }
}
