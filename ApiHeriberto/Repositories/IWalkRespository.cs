using ApiHeriberto.Models.Domain;

namespace ApiHeriberto.Repositories
{
    public interface IWalkRespository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> ListAll(string? filterOn = null,
            string? filterQuery = null, string? sortBy = null, bool? isAscending = true,
            int pageNumber = 1, int pageSize = 10
            );
        Task<Walk?> GetById(Guid id);
        Task<Walk?> UpdateById(Guid id, Walk walk);

        Task<Walk?> DeleteById(Guid id);

    }
}
