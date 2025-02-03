using ApiHeriberto.Models.Domain;

namespace ApiHeriberto.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
