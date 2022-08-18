using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyReposiotry service;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyReposiotry sevice, IMapper mapper)
        {
            this.service = sevice;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficulties = await service.GetAllAsync();

            var walkDifficultiesDTO = mapper.Map<IEnumerable<Models.DTOs.WalkDifficulty>>(walkDifficulties);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName(nameof(GetWalkDifficultyAsync))]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await service.GetAsync(id);

            if (walkDifficulty == null)
                return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficulty addWalkDifficulty)
        {
            var walkDifficulty = mapper.Map<Models.Domain.WalkDifficulty>(addWalkDifficulty);

            await service.AddAsync(walkDifficulty);

            var walkDifficultyDTO = mapper.Map<Models.Domain.WalkDifficulty>(walkDifficulty);

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficulty.Id }, walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsyc(Guid id)
        {
            var deletedWalkDifficulty = await service.DeleteAsync(id);

            if (deletedWalkDifficulty == null)
                return NotFound();

            var deletedWalkDifficultyDTO = mapper.Map<Models.DTOs.WalkDifficulty>(deletedWalkDifficulty);

            return Ok(deletedWalkDifficulty);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficulty = mapper.Map<Models.Domain.WalkDifficulty>(updateWalkDifficultyRequest);

            walkDifficulty = await service.UpdateAsync(id, walkDifficulty);

            if (walkDifficulty == null)
                return NotFound();

            var updatedWalkDifficultyDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficulty);

            return Ok(updatedWalkDifficultyDTO);
        }
    }
}
