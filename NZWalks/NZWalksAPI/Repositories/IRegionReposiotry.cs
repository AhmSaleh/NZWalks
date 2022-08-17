using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IRegionReposiotry
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
