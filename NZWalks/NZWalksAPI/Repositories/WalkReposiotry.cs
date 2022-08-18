using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkReposiotry : IWalkReposiotry
    {
        private readonly NZWalkContext nZWalksDbContext;

        public WalkReposiotry(NZWalkContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await
                nZWalksDbContext.Walks
                .Include(w => w.WalkDifficulty)
                .Include(w => w.Region)
                .ToListAsync();
        }
        public async Task<Walk> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Walks
                .Include(w => w.WalkDifficulty)
                .Include(w => w.Region)
                .FirstOrDefaultAsync(w => w.Id == id);
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();

            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await GetAsync(id);

            if (walk == null)
                return null;

            nZWalksDbContext.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks
                .Include(w => w.WalkDifficulty)
                .Include(w => w.Region)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (existingWalk == null)
                return null;

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
