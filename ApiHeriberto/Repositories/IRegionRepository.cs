using ApiHeriberto.Models.Domain;

namespace ApiHeriberto.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region?> AddAsync(Region region);

        Task<Region?> UpdateByIdAsync(Guid id, Region region);

        Task<Region?> DeleteById(Guid id);

    }
}
