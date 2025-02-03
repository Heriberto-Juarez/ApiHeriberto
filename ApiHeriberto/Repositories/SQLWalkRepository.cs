using ApiHeriberto.Data;
using ApiHeriberto.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiHeriberto.Repositories
{
    public class SQLWalkRepository : IWalkRespository
    {
        private readonly AppDbContext dbContext;

        public SQLWalkRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> Create(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteById(Guid id)
        {
            var walkInDb = await this.dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkInDb == null)
            {
                return null;
            }
            this.dbContext.Remove(walkInDb);
            await this.dbContext.SaveChangesAsync();
            return walkInDb;
        }

        public async Task<Walk?> GetById(Guid id)
        {
            return await this.dbContext.Walks.Include(x=>x.Region).Include(x=>x.Difficulty).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Walk>> ListAll(
            string? filterOn=null, 
            string? filterQuery=null, 
            string? sortBy=null, 
            bool? isAscending=true,
            int pageNumber=1,
            int pageSize=10
        )
        {

            var walks = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending==true)
                    {
                        walks = walks.OrderBy(x => x.Name);
                    }
                    else
                    {
                        walks = walks.OrderByDescending(x => x.Name);
                    }
                }
            }

            var skipResults = (pageNumber - 1) * pageNumber;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> UpdateById(Guid id, Walk walk)
        {
            var walkInDb = await this.dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkInDb == null)
            {
                return null;
            }
            walkInDb.Name = walk.Name;
            walkInDb.Description = walk.Description;
            walkInDb.LengthInKm = walk.LengthInKm;
            walkInDb.WalkImageUrl = walk.WalkImageUrl;
            walkInDb.DifficultyId = walk.DifficultyId;
            walkInDb.RegionId = walk.RegionId;
            walkInDb.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return walkInDb;
        }
    }
}
