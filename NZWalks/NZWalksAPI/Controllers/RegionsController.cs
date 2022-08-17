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

        public RegionsController(IRegionReposiotry regionReposiotry, IMapper mapper)
        {
            this.regionReposiotry = regionReposiotry;
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionReposiotry.GetAllAsync();

            var regionsDTO = Mapper.Map<List<Models.DTOs.Region>>(regions);

            return Ok(regionsDTO);
        }
    }
}
