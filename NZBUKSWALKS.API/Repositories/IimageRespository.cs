using NZBUKSWALKS.API.Models.Domain;

namespace NZBUKSWALKS.API.Repositories
{
    public interface IimageRespository
    {
        Task<Image> Upload(Image image);  
    }
}
