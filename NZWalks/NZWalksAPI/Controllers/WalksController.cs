using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkReposiotry walksReposity;
        private readonly IMapper mapper;

        public WalksController(IWalkReposiotry walksReposity, IMapper mapper)
        {
            this.walksReposity = walksReposity;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksASync()
        {
            var walks = await walksReposity.GetAllAsync();

            var walksDTO = mapper.Map<IEnumerable<Models.DTOs.Walk>>(walks);

            return Ok(walksDTO);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName(nameof(GetWalkAsync))]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walksReposity.GetAsync(id);

            if (walk == null)
                return null;

            var walkDTO = mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequest addWalkRequest)
        {
            var walk = mapper.Map<Models.Domain.Walk>(addWalkRequest);

            await walksReposity.AddAsync(walk);

            var walkDTO = mapper.Map<Models.DTOs.Walk>(walk);

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await walksReposity.DeleteAsync(id);

            if (walk == null)
                return null;

            var walkDTO = mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(walkDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            var walk = mapper.Map<Models.Domain.Walk>(updateWalkRequest);

            walk = await walksReposity.UpdateAsync(id, walk);

            if (walk == null)
                return NotFound();

            var updatedWalk = mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(updatedWalk);

        }

    }
}
