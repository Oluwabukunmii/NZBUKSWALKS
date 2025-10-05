using NZBUKSWALKS.API.Data;
using NZBUKSWALKS.API.Models.Domain;

namespace NZBUKSWALKS.API.Repositories
{
    public class LocalImageRespository : IimageRespository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZBUKSWALKSDBCONTEXT dbcontext;

        public LocalImageRespository(IWebHostEnvironment webHostEnvironment , IHttpContextAccessor  httpContextAccessor , NZBUKSWALKSDBCONTEXT dbcontext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbcontext = dbcontext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Imagesfolder" ,$"{image.fileName}{image.fileExtension}");

            //upload image to lacal path

           using  var stream = new FileStream (localFilePath, FileMode.Create);
            await image.file.CopyToAsync (stream);

            //https://localhost:1234/imagesfolder/image.jpg

            var urlFilepath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Imagesfolder/{image.fileName}{image.fileExtension}";

            image.filePath = urlFilepath;

            // Add image to the images table

            await dbcontext.Images.AddAsync (image);

            await dbcontext.SaveChangesAsync ();

            return image;




        }
    }
}
