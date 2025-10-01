using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZBUKSWALKS.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions <NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "c84763d4-2b0a-40f8-ab2b-7a9fdd346d9";
            var writerRoleId = "27aae5d2-ec91-49be-9ab9-095d66216cd0";

            var roles = new List<IdentityRole>
            {


                new IdentityRole
                {

                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId ,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),

                },

                new IdentityRole
                {

                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId ,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),

                }


            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
