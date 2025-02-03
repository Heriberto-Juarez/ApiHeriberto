using ApiHeriberto.Data;
using ApiHeriberto.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiHeriberto.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly AppDbContext dbContext;

        public SQLRegionRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region?> AddAsync(Region region)
        {
            await this.dbContext.Regions.AddAsync(region);
            await this.dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteById(Guid id)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Region?> UpdateByIdAsync(Guid id, Region region)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            existingRegion.Code = region.Code;
            dbContext.Update(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
