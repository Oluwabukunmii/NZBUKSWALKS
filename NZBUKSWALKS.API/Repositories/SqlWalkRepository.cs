using Microsoft.EntityFrameworkCore;
using NZBUKSWALKS.API.Data;
using NZBUKSWALKS.API.Models.Domain;

namespace NZBUKSWALKS.API.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NZBUKSWALKSDBCONTEXT dbContext;

        public SqlWalkRepository(NZBUKSWALKSDBCONTEXT dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk Walk)
        {

         await   dbContext.Walks.AddAsync(Walk);
          await  dbContext.SaveChangesAsync();
            return Walk;
        }

       
        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk Walk)
        {
          var existingWalk =   await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)

            {
                return null;
            }

            existingWalk.Name = Walk.Name;
            existingWalk.Description = Walk.Description;
            existingWalk.LengthInKm = Walk.LengthInKm;
            existingWalk.WalkImageUrl = Walk.WalkImageUrl;
            existingWalk.Difficultyid = Walk.Difficultyid;
            existingWalk.Regionid = Walk.Regionid;

            await dbContext.SaveChangesAsync();

            return (existingWalk);
        }


        public async Task<Walk?> DeleteAsync(Guid id)
        {
          var existingWalk =  await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)

            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;



        }

    }
}
