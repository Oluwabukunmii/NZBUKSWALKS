using Microsoft.EntityFrameworkCore;
using NZBUKSWALKS.API.Data;
using NZBUKSWALKS.API.Models.Domain;

namespace NZBUKSWALKS.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZBUKSWALKSDBCONTEXT dbcontext;

        public SqlRegionRepository(NZBUKSWALKSDBCONTEXT dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbcontext.Regions.AddAsync(region);
            await dbcontext.SaveChangesAsync();
            return region;
        }




        public async Task<List<Region>> GetAllAsync()
        {
            return await dbcontext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {

                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbcontext.SaveChangesAsync();

            return existingRegion;
        }

        async Task<Region?> IRegionRepository.DeleteAsync(Guid id)
        {
            var existingregion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingregion == null)
            {

                return null;
            }

            dbcontext.Regions.Remove(existingregion);
            await dbcontext.SaveChangesAsync();
            return existingregion;

        }

    }
}