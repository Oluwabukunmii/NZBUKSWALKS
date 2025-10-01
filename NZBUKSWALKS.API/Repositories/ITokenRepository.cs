using Microsoft.AspNetCore.Identity;

namespace NZBUKSWALKS.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);


    }
}
