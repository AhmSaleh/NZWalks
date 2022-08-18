using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkDifficulityReposiotry : IWalkDifficultyReposiotry
    {
        private readonly NZWalkContext context;

        public WalkDifficulityReposiotry(NZWalkContext context)
        {
            this.context = context;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();

            await context.AddAsync(walkDifficulty);
            await context.SaveChangesAsync();

            return walkDifficulty;
        }
        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficuty = await context.WalkDifficulties.FirstOrDefaultAsync(w => w.Id == id);

            if (walkDifficuty == null)
                return null;

            context.Remove(walkDifficuty);
            await context.SaveChangesAsync();

            return walkDifficuty;
        }
        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await context.WalkDifficulties.ToListAsync();
        }
        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await context.WalkDifficulties.FirstOrDefaultAsync(w => w.Id == id);

        }
        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await GetAsync(id);

            existingWalkDifficulty.Code = walkDifficulty.Code;

            await context.SaveChangesAsync();

            return existingWalkDifficulty;
        }
    }
}
