using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZBUKSWALKS.API.CustomActionFilters;
using NZBUKSWALKS.API.Models.Domain;
using NZBUKSWALKS.API.Models.DTO;
using NZBUKSWALKS.API.Repositories;

namespace NZBUKSWALKS.API.Controllers
{
    //api//walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository WalkRepository;


        public WalksController(IMapper mapper, IWalkRepository WalkRepository)
        {
            this.mapper = mapper;
            this.WalkRepository = WalkRepository;

        }


        //CREATE WALKS
        // POST : /api/walks

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto AddWalkRequestDto)
        {


            //Add Dto to domain Model

            var walkDomainModel = mapper.Map<Walk>(AddWalkRequestDto);

            await WalkRepository.CreateAsync(walkDomainModel);


            //Map domain model to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));




        }

        //GET ALL WALKS
        //GET : /API/WALKS

        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var walksDomainModel = await WalkRepository.GetAllAsync();

            //Map domain model to Dto


            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));


        }

        //GET SINGLE Walk (GET WALK BY ID)
        // GET : https://localhost:portnumber/api/walks{id}//

        [HttpGet]

        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainModel = await WalkRepository.GetByIdAsync(id);

            if (walksDomainModel == null)

            {
                return NotFound();
            }
            //Map domain model to Dto


            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        // PUT:https://localhost:portnumber/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]



        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)

        {

            //Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            var theWalkDomainModel = await WalkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map  Domain Model back to Dto

            return Ok(mapper.Map<WalkDto>(theWalkDomainModel));



        }

        //TO DELETE A WALK.
        //DELETE: https://localhost:portnumber/api/walk/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var walkDomainModel = await WalkRepository.DeleteAsync(id);

            if (walkDomainModel == null)

            {
                return NotFound();
            }

            //Map  Domain Model back to Dto


            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }
    }
}