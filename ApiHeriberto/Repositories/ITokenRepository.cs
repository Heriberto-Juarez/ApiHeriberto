using Microsoft.AspNetCore.Identity;

namespace ApiHeriberto.Repositories
{
    public interface ITokenRepository
    {

        string CreateJwtToken(IdentityUser user, List<string> roles);


    }
}
