using ApiHeriberto.Data;
using ApiHeriberto.Models.Domain;
using System.Data;

namespace ApiHeriberto.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor,
            AppDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {

            if (image?.File == null || string.IsNullOrWhiteSpace(image.FileName) || string.IsNullOrWhiteSpace(image.FileExtension))
            {
                throw new ArgumentException("Invalid file data");
            }
            var safeFileName = Path.GetFileNameWithoutExtension(image.FileName);
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{safeFileName}{image.FileExtension}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var request = httpContextAccessor.HttpContext.Request;
            var urlFilePath = $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{safeFileName}{image.FileExtension}";
            image.FilePath = urlFilePath;
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
