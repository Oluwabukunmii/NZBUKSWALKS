namespace NZBUKSWALKS.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }   
        public string Name { get; set; }    

        public string Description { get; set; }

        public double LengthInKm {  get; set; }

        public string? WalkImageUrl  { get; set; }
//relationships//
        public Guid Difficultyid { get; set; }
        public Guid Regionid { get; set; }  

        //Navigation properties
        public Difficulty Difficulty {  get; set; }
        public Region Region { get; set; }


    }
}
