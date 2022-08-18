using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTOs.Walk>()
                .ReverseMap();

            CreateMap<Models.Domain.Walk, Models.DTOs.AddWalkRequest>()
                .ReverseMap();

            CreateMap<Models.Domain.Walk, Models.DTOs.UpdateWalkRequest>()
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTOs.WalkDifficulty>()
                .ReverseMap();

        }
    }
}
