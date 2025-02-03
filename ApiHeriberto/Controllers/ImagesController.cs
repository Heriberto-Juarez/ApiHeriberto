using ApiHeriberto.Models.Domain;
using ApiHeriberto.Models.DTO;
using ApiHeriberto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiHeriberto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Upload([FromForm] ImageUploadRequestDto dto)
        {

            ValidateFileUpload(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var image = new Image
            {
                File = dto.File,
                FileExtension = Path.GetExtension(dto.File.FileName),
                FileSizeInBytes = dto.File.Length,
                FileName = dto.File.FileName,
                FileDescription = dto.FileDescription,
            };

            await imageRepository.Upload(image);
            return Ok();
        }

        private void ValidateFileUpload(ImageUploadRequestDto dto)
        {
            var allowedExtensions = new string[] {
                ".jpg",
                ".jpeg",
                ".png",
            };

            if (!allowedExtensions.Contains(Path.GetExtension(dto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }


            if (dto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller file.");
            }

        }

    }
}
