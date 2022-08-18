using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class WalkDifficultyProfile : Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.DTOs.WalkDifficulty>()
                .ReverseMap();


            CreateMap<Models.Domain.WalkDifficulty, Models.DTOs.AddWalkDifficulty>()
                .ReverseMap();


            CreateMap<Models.Domain.WalkDifficulty, Models.DTOs.UpdateWalkDifficultyRequest>()
                .ReverseMap();
        }
    }
}
