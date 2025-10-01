using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZBUKSWALKS.API.CustomActionFilters;
using NZBUKSWALKS.API.Data;
using NZBUKSWALKS.API.Models.Domain;
using NZBUKSWALKS.API.Models.DTO;
using NZBUKSWALKS.API.Repositories;

namespace NZBUKSWALKS.API.Controllers
{   //https://localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase

    {
        private readonly NZBUKSWALKSDBCONTEXT dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZBUKSWALKSDBCONTEXT dbContext, IRegionRepository regionRepository ,
            
            IMapper mapper)
            
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //GET ALL REGIONS
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        [Authorize (Roles = "Reader , Writer")]

        public async Task <IActionResult> GetAll()
        {
            //GET DATA FROM DATABASE

            //  var regionsDomains = await dbContext.Regions.ToListAsync();//
            var regionsDomains = await regionRepository.GetAllAsync();



            //MAP DOMAIN MODELS TO DTOS

            /*  var regionsDto = new List<RegionDto>();
              foreach (var regionDomain in regionsDomains)
              {
                  regionsDto.Add(new RegionDto()

                  {
                      Id = regionDomain.Id,
                      Code = regionDomain.Code,
                      Name = regionDomain.Name,
                      RegionImageUrl = regionDomain.RegionImageUrl,
                  });
              }*/

            var regionDto = mapper.Map<List<RegionDto>>(regionsDomains);

            //RETURNS DTOS BACK TO THE CLIENT
            return Ok(regionDto);
        }

        //GET SINGLE REGION (GET REGION BY ID)
        // GET : https://localhost:portnumber/api/regions{id}//
        [HttpGet]

        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader , Writer")]


        public async Task <IActionResult> GetById([FromRoute] Guid id)
        {

            // var region = dbContext.Regions.Find(id);//
            //Get Region Domain Model from Database

            // var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);//

            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)

            {
                return NotFound();
            }
            //map regon domain model to region dto

           /* var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
           */

            // map domain modeltodtos with automapper

         var regionDto =   mapper.Map<RegionDto>(regionDomain);

            // return Dto back to client
            return Ok(regionDto);

        }

        //POST to craeate New region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]


        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto AddRegionRequestDto)
        {

                // Map  DTO to Domain Model

                /*   var regionDomainModel = new Region

                   {
                       Code = AddRegionRequestDto.Code,
                       Name = AddRegionRequestDto.Name,
                       RegionImageUrl = AddRegionRequestDto.RegionImageUrl,
                   };

                   //Use Domain Model to create region [in the database]

                   //  await dbContext.Regions.AddAsync(regionDomainModel);
                   // await dbContext.SaveChangesAsync();

                */
                var regionDomainModel = mapper.Map<Region>(AddRegionRequestDto);

                //Use Domain Model to create region [in the database] [contd]

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);


                // Map domain model back to DT0

                /*     var regionDto = new RegionDto

                     {
                         Id = regionDomainModel.Id,
                         Code = regionDomainModel.Code,
                         Name = regionDomainModel.Name,
                         RegionImageUrl= regionDomainModel.RegionImageUrl,

                     };
                */
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

            }

                 
        // UPDATE REGION

        // PUT:https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]


        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDto)

        {
           

                //Map Dto to domain model

                /*   var regionDomainModel = new Region
                   {
                       Code = updateRegionRequestDto.Code,
                       Name = updateRegionRequestDto.Name,
                       RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
                   };
                */

                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //Check if region exits//

                //  var regionDomainModel=  await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);



                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //  Map Dto to Domain Model

                regionDomainModel.Code = updateRegionRequestDto.Code;
                regionDomainModel.Name = updateRegionRequestDto.Name;
                regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

                await dbContext.SaveChangesAsync();

                //convert Domain model to DTO

                /*  var regionDto = new RegionDto

                  {
                      Id = regionDomainModel.Id,
                      Code = regionDomainModel.Code,
                      Name = regionDomainModel.Name,
                      RegionImageUrl = regionDomainModel.RegionImageUrl,

                  };*/
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);

            

        }

        //TO DELETE A REGION.
        //DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //  var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

          var  regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)

            {
                return NotFound();
            }
            // Delete region
            dbContext.Regions.Remove(regionDomainModel);
           await dbContext.SaveChangesAsync();


            //return deleted region back
            //Map Domain Model to DTO


/*           var regionDto = new RegionDto

            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,

            };
*/

             var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);


}


}



}


