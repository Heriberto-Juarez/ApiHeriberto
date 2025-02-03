using ApiHeriberto.Models.Domain;

namespace ApiHeriberto.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAll();
    }
}
