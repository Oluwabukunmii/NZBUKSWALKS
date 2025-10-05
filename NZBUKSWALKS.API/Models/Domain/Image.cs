using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZBUKSWALKS.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }


        [NotMapped]  //to be excluded from the database mapping
        public IFormFile file { get; set; }

        public string fileName { get; set; }

        public string? fileDescription { get; set; }

        public string fileExtension { get; set; }

        public long fileSizeInByte { get; set; }

        public string filePath { get; set; }

    }
}
