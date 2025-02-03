using ApiHeriberto.Data;
using ApiHeriberto.Models.Domain;
using ApiHeriberto.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiHeriberto.Repositories
{
    public class SQLDififcultyRepository : IDifficultyRepository
    {
        private readonly AppDbContext dbContext;

        public SQLDififcultyRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Difficulty>> GetAll()
        {
            return await this.dbContext.Difficulties.ToListAsync();
        }
    }
}
