using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTOs.Region>();
            CreateMap<Models.DTOs.AddRegionRequest, Models.Domain.Region>();
            CreateMap<Models.DTOs.UpdateRegionRequest, Models.Domain.Region>();
        }
    }
}
