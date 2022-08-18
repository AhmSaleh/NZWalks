using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionReposiotry regionReposiotry;
        private readonly IMapper mapper;

        public RegionsController(IRegionReposiotry regionReposiotry, IMapper mapper)
        {
            this.regionReposiotry = regionReposiotry;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionReposiotry.GetAllAsync();

            var regionsDTO = mapper.Map<List<Models.DTOs.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName(nameof(GetRegionAsync))]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionReposiotry.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTOs.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTOs.AddRegionRequest addRegionRequest)
        {
            var region = mapper.Map<Models.Domain.Region>(addRegionRequest);

            await regionReposiotry.AddAsync(region);

            var regionDTO = mapper.Map<Models.DTOs.Region>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {

            var region = await regionReposiotry.DeleteAsync(id);

            if (region == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTOs.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, [FromBody] Models.DTOs.UpdateRegionRequest updateRegionRequest)
        {
            var regionDomain = mapper.Map<Models.Domain.Region>(updateRegionRequest);
            regionDomain = await regionReposiotry.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
                return NotFound();

            var regionDTO = mapper.Map<Models.DTOs.Region>(regionDomain);

            return Ok(regionDTO);

        }
    }
}
