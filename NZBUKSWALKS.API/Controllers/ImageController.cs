using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZBUKSWALKS.API.Models.Domain;
using NZBUKSWALKS.API.Models.DTO;
using NZBUKSWALKS.API.Repositories;

namespace NZBUKSWALKS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IimageRespository imageRespository;

        public ImageController(IimageRespository imageRespository)
        {
            this.imageRespository = imageRespository;
        }

        [HttpPost]

        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)

        {
            VaildateFileUpload(request);

            if (ModelState.IsValid)
            {

                // Convert image to domain model

                var imageDomainModel = new Image

                {
                    file = request.File,

                    fileExtension = Path.GetExtension(request.File.FileName),

                    fileSizeInByte = request.File.Length,

                    fileName = request.File.FileName,

                    fileDescription = request.fileDescription,

                    

                };

                //User repository to upload image

                await imageRespository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }

            return BadRequest(ModelState);

        }


        private void VaildateFileUpload(ImageUploadRequestDto request)
        {

            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))

            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 10485750)
            {
                ModelState.AddModelError("file", "File size more than 10MB , please upload a smaller size file");
            }
        }
    }
}
