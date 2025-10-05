namespace NZBUKSWALKS.API.Models.DTO
{
    public class ImageUploadRequestDto
    {

        public IFormFile File { get; set; }

        public string Filename { get; set; }

        public string? fileDescription { get; set; }
    }
}
